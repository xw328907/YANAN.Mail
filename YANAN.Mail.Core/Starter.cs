using YANAN.Mail.Core.WindowsService;
using System;
using System.ServiceModel;

namespace YANAN.Mail.Core
{
    public static class Starter
    {
        public static void Start(Pop3ReceiverLoaderBase receiverLoader, ImapReceiverLoaderBase imapReceiverLoader, SmtpSenderLoaderBase senderLoader, string[] args)
        {
            Starter.receiverLoader = receiverLoader;
            Starter.imapReceiver = imapReceiverLoader;
            Starter.senderLoader = senderLoader;
            RunStart(args);
            //ServiceStarter.Start(RunStart, RunStop, serviceStartInfo, args);
        }
        private static Pop3ReceiverLoaderBase receiverLoader;
        private static ImapReceiverLoaderBase imapReceiver;
        private static SmtpSenderLoaderBase senderLoader;


        public static void RunStart(string[] args)
        {
            MailBee.Global.LicenseKey= Utilities.ConstConfig.MailBeeLicenseKey;
            SmtpSenderHost.Start(senderLoader);
            Pop3ReceiverHost.Start(receiverLoader);
            ImapReceiverHost.Start(imapReceiver);
        }
        public static void RunStop()
        {
            ImapReceiverHost.Stop();
        }
    }
}
