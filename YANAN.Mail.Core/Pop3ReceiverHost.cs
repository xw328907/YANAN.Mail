using YANAN.Mail.Core.ThreadTask;
using YANAN.Mail.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
namespace YANAN.Mail.Core
{
    public static class Pop3ReceiverHost
    {
        public static StatusBags StatusBags { get; } = new StatusBags();

        private static List<string> toDeleteTasks = new List<string>(); //需要从队列中删除的邮局
        private static readonly object lockReceiverTasks = new object();
        public static Dictionary<string, Pop3ReceiverBase> ReceiverTasks = new Dictionary<string, Pop3ReceiverBase>();
        public static TaskPool ReceiverPool = null;
        private static Pop3ReceiverLoaderBase loader;

        public static void Start(Pop3ReceiverLoaderBase loader)
        {
            Pop3ReceiverHost.loader = loader;
            MailBee.Global.LicenseKey = ConstConfig.MailBeeLicenseKey;

            ReceiverPool = TaskPool.Create("Receiver", Const.ReceiveThreadCount);

            Thread thread = new Thread(new ThreadStart(ThreadReceiveTimer)) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        ///运行pop3协议邮箱收取服务
        /// </summary>
        private static void ThreadReceiveTimer()
        {
            //加载所有的mailBox 
            Dictionary<string, List<string>> mailBoxIds = loader.LoadAllMailBoxIds();

            foreach (string companyCode in mailBoxIds.Keys)
            {
                foreach (string mailBoxId in mailBoxIds[companyCode])
                {
                    LoadMailBox(companyCode, mailBoxId, true);
                }
            }

            while (true)
            {
                //删除邮局任务
                lock (lockReceiverTasks)
                {
                    foreach (string key in toDeleteTasks)
                    {
                        if (ReceiverTasks.ContainsKey(key))
                        {
                            ReceiverTasks.Remove(key);
                        }
                    }
                    toDeleteTasks = new List<string>();
                }
                foreach (Pop3ReceiverBase receiver in ReceiverTasks.Values)
                {
                    if (receiver.NextWorkTime <= DateTime.Now)
                    {
                        receiver.NextWorkTime = DateTime.Now.AddMinutes(receiver.TimerMinutes);
                        ReceiverPool.AddToEnd(receiver);
                    }
                }
                Thread.Sleep(Const.ReceiveTimer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="mailBoxId"></param>
        /// <param name="forceReload">强制从数据库读取,只有在平时点击“收取”的时候才传false</param>
        public static void LoadMailBox(string companyCode, string mailBoxId, bool forceReload)
        {

            string key = CreateKey(companyCode, mailBoxId);
            if (!forceReload && ReceiverTasks.ContainsKey(key)) return;


            lock (lockReceiverTasks)
            {
                //强制加载
                Pop3ReceiverBase task = loader.LoadMailBox(companyCode, mailBoxId);
                task.Key = key;
                if (!ReceiverTasks.ContainsKey(key))
                {
                    ReceiverTasks.Add(key, task);
                }
                else
                {
                    ReceiverTasks[key] = task;
                }
            }
        }

        public static void StartImmediately(string companyCode, string mailBoxId)
        {
            string key = CreateKey(companyCode, mailBoxId);
            if (!ReceiverTasks.ContainsKey(key)) return;
            ReceiverTasks[key].NextWorkTime = ReceiverTasks[key].NextWorkTime.AddMinutes(ReceiverTasks[key].TimerMinutes);
            ReceiverPool.AddToBegin(ReceiverTasks[key]);
        }

        public static void DeleteMailBox(string companyCode, string mailBoxId)
        {
            lock (lockReceiverTasks)
            {
                toDeleteTasks.Add(CreateKey(companyCode, mailBoxId));
            }
        }

        public static string CreateKey(string companyCode, string mailBoxId)
        {
            return companyCode + "_" + mailBoxId;
        }
    }
}
