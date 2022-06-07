using YANAN.Mail.Core.ThreadTask;
using YANAN.Mail.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace YANAN.Mail.Core
{
    public class SmtpSenderHost
    {
        public static StatusBags StatusBags { get; } = new StatusBags();

        public static TaskPool SenderPool = null;
        private static SmtpSenderLoaderBase loader;
        public static void Start(SmtpSenderLoaderBase loader)
        {
            SmtpSenderHost.loader = loader;
            MailBee.Global.LicenseKey = ConstConfig.MailBeeLicenseKey;

            SenderPool = TaskPool.Create("Sender", Const.SendThreadCount);

            Thread thread = new Thread(new ThreadStart(ThreadSendTimer))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private static void ThreadSendTimer()
        {
            while (true)
            {
                try
                {
                    Dictionary<string, List<string>> mailMainIds = loader.LoadAllSendTasks();

                    foreach (string companyCode in mailMainIds.Keys)
                    {
                        List<SmtpSenderBase> tasks = loader.LoadSendTasks(companyCode, mailMainIds[companyCode].ToArray());
                        foreach (SmtpSenderBase task in tasks)
                        {
                            SenderPool.AddToEnd(task);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteError(ex);
                }


                Thread.Sleep(Const.SendTimer);
            }
        }

        public static void SendImmediately(string companyCode, string[] mailIds)
        {
            try
            {
                List<string> needLoad = new List<string>();
                foreach (string mailId in mailIds)
                {
                    string key = CreateKey(companyCode, mailId);
                    if (!SenderPool.TaskExists(key))
                    {
                        needLoad.Add(mailId);
                    }
                    SmtpSenderHost.StatusBags.RegisterKey(key);
                }


                List<SmtpSenderBase> tasks = loader.LoadSendTasks(companyCode, needLoad.ToArray());
                foreach (SmtpSenderBase task in tasks)
                {
                    SenderPool.AddToEnd(task);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex);
                throw;
            }
        }

        public static string CreateKey(string companyCode, string mailId)
        {
            return companyCode + "_" + mailId;
        }
    }
}
