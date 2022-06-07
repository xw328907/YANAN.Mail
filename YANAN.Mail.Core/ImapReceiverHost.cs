using System;
using System.Collections.Generic;
using System.Threading;

namespace YANAN.Mail.Core
{
    using YANAN.Mail.Core.ThreadTask;

    public static class ImapReceiverHost
    {
        public static StatusBags StatusBags { get; } = new StatusBags();
        private static readonly object lockReceiverTasks = new object();
        public static Dictionary<string, ImapReceiverBase> ReceiverTasks = new Dictionary<string, ImapReceiverBase>();
        private static ImapReceiverLoaderBase loader;

        public static TaskPool ReceiverPool = null;

        public static void Start(ImapReceiverLoaderBase loader)
        {
            ImapReceiverHost.loader = loader;
            Dictionary<string, List<string>> mailBoxIds = loader.LoadAllMailBoxIds();
            foreach (string companyCode in mailBoxIds.Keys)
            {
                foreach (string mailBoxId in mailBoxIds[companyCode])
                {
                    LoadMailBox(companyCode, mailBoxId, true);
                }
            }
            Thread threadIdle = new Thread(new ThreadStart(ThreadReceiveIdle)) { IsBackground = true };
            threadIdle.Start();
            Thread thread = new Thread(new ThreadStart(ThreadReceiveTimer)) { IsBackground = true };
            thread.Start();
        }

        public static void Stop()
        {
            foreach (ImapReceiverBase receiver in ReceiverTasks.Values)
            {
                if (receiver.imap != null && receiver.IsIdle)
                {
                    if (receiver.imap.IsConnected && !receiver.imap.IsAborted)
                        receiver.imap.Disconnect();
                    receiver.imap.Dispose();
                }
            }
        }
        /// <summary>
        ///  加载运行支持idle模式的IMAP协议邮箱
        /// </summary>
        private static void ThreadReceiveIdle()
        {
            foreach (ImapReceiverBase receiver in ReceiverTasks.Values)
            {
                if (!receiver.IsIdle) continue;
                if (receiver.NextWorkTime < DateTime.Now) receiver.NextWorkTime = DateTime.Now;
                receiver.DoWork();
            }
        }
        /// <summary>
        ///  加载运行不支持idle模式的IMAP协议邮箱
        /// </summary>
        private static void ThreadReceiveTimer()
        {
            if (ReceiverTasks.Count > 0)
            {
                while (true)
                {
                    foreach (ImapReceiverBase receiver in ReceiverTasks.Values)
                    {
                        if (receiver.IsIdle) continue;
                        CreateImapPool();
                        if (receiver.NextWorkTime <= DateTime.Now)
                        {
                            receiver.NextWorkTime = DateTime.Now.AddMinutes(receiver.TimerMinutes);
                            ReceiverPool.AddToEnd(receiver);
                        }
                    }
                    Thread.Sleep(Const.ReceiveTimer);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ocode"></param>
        /// <param name="mailBoxId"></param>
        /// <param name="forceReload">强制从数据库读取,只有在平时点击“收取”的时候才传false</param>
        public static void LoadMailBox(string ocode, string mailBoxId, bool forceReload)
        {
            string key = CreateKey(ocode, mailBoxId);
            if (!forceReload && ReceiverTasks.ContainsKey(key)) return;
            lock (lockReceiverTasks)
            {
                //强制加载
                ImapReceiverBase task = loader.LoadMailBox(ocode, mailBoxId);
                if (task == null) return;
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
            if (ReceiverTasks[key].IsIdle)
            {
                AbortReceiveTask(companyCode, mailBoxId);
                ReceiverTasks[key].NextWorkTime = DateTime.Now;//.AddMinutes(-ReceiverTasks[key].TimerMinutes)
                ReceiverTasks[key].DoWork();
            }
            else
            {
                CreateImapPool();
                ReceiverTasks[key].NextWorkTime = DateTime.Now;//ReceiverTasks[key].NextWorkTime.AddMinutes(ReceiverTasks[key].TimerMinutes);
                ReceiverPool.AddToBegin(ReceiverTasks[key]);
            }
        }
        /// <summary>
        /// 创建收取程序池(如不存在)
        /// </summary>
        private static void CreateImapPool()
        {
            if (ReceiverPool == null) ReceiverPool = TaskPool.Create("ImapReceiver", Const.ReceiveThreadCount);
        }
        /// <summary>
        /// 终止协议
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="mailBoxId"></param>
        private static void AbortReceiveTask(string companyCode, string mailBoxId)
        {
            string key = CreateKey(companyCode, mailBoxId);
            if (ReceiverTasks.ContainsKey(key))
            {
                var receiver = ReceiverTasks[key];
                if (receiver.imap != null && receiver.IsIdle)
                {
                    if (receiver.imap.IsConnected)
                        receiver.imap.Disconnect();
                    if (!receiver.imap.IsAborted)
                        receiver.imap.Abort();
                    receiver.imap.Dispose();
                }
            }
        }
        public static void DeleteMailBox(string companyCode, string mailBoxId)
        {
            lock (lockReceiverTasks)
            {
                string key = CreateKey(companyCode, mailBoxId);
                if (ReceiverTasks.ContainsKey(key))
                {
                    if (ReceiverTasks[key].IsIdle)
                    {
                        AbortReceiveTask(companyCode, mailBoxId);
                    }
                    ReceiverTasks.Remove(key);
                }
            }
        }

        public static string CreateKey(string companyCode, string mailBoxId)
        {
            return companyCode + "_" + mailBoxId;
        }
    }

}
