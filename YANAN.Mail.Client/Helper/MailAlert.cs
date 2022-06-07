using YANAN.Mail.Core;
using YANAN.Mail.Entity;
using YANAN.Mail.Services;
using System.Collections.Generic;
using Titan;

namespace YANAN.Mail.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class MailAlert
    {
        /// <summary>
        /// 获取邮箱的收件协议类型 1=pop3,2=imap
        /// </summary>
        /// <param name="ocode"></param>
        /// <param name="mailBoxId"></param>
        /// <returns></returns>
        public int GetMailBoxProtocolType(string ocode, string mailBoxId)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    MailBox mailBox = new MailBox { MailBoxId = mailBoxId };
                    db.Select(mailBox, MailBox_.ProtocolTypeId, MailBox_.OCode);
                    if (mailBox == null || ocode != mailBox.OCode) return -1;
                    return mailBox.ProtocolTypeId;
                }
                catch
                {
                    return -1;
                }
            }
        }

        #region 收取相关

        public void MailBoxInserted(string ocode, string mailBoxId)
        {
            int protocolType = GetMailBoxProtocolType(ocode, mailBoxId);
            if (protocolType == 2)
            {
                ImapReceiverHost.LoadMailBox(ocode, mailBoxId, true);
            }
            else if (protocolType == 1)
            {
                Pop3ReceiverHost.LoadMailBox(ocode, mailBoxId.ToString(), true);
            }
        }

        public void MailBoxUpdated(string ocode, string mailBoxId)
        {
            int protocolType = GetMailBoxProtocolType(ocode, mailBoxId);
            if (protocolType == 2)
            {
                ImapReceiverHost.LoadMailBox(ocode, mailBoxId, true);
            }
            else if (protocolType == 1)
            {
                Pop3ReceiverHost.LoadMailBox(ocode, mailBoxId, true);
            }
        }

        public void MailBoxDeleted(string ocode, string mailBoxId)
        {
            int protocolType = GetMailBoxProtocolType(ocode, mailBoxId);
            if (protocolType == 2)
            {
                ImapReceiverHost.DeleteMailBox(ocode, mailBoxId);
            }
            else if (protocolType == 1)
            {
                Pop3ReceiverHost.DeleteMailBox(ocode, mailBoxId);
            }
        }
        /// <summary>
        /// 黑名单/拒收列表有更改需重新加载邮箱数据,待改进(不需要全部邮箱重新加载，只需更改当前邮箱数据)
        /// </summary>
        /// <param name="ocode"></param>
        /// <param name="mailBoxId"></param>
        public void MailJudgeChange(string ocode, string mailBoxId)
        {
            int protocolType = GetMailBoxProtocolType(ocode, mailBoxId);
            if (protocolType == 2)
            {
                ImapReceiverHost.LoadMailBox(ocode, mailBoxId, true);
            }
            else if (protocolType == 1)
            {
                Pop3ReceiverHost.LoadMailBox(ocode, mailBoxId, true);
            }
        }
        public void ReceiveMail(string ocode, string[] mailBoxIds)
        {
            if (string.IsNullOrWhiteSpace(ocode)) return;
            if (mailBoxIds == null || mailBoxIds.Length < 1) return;
            foreach (string mailBoxId in mailBoxIds)
            {
                if (string.IsNullOrWhiteSpace(mailBoxId)) continue;
                int protocolType = GetMailBoxProtocolType(ocode, mailBoxId);
                string key;
                if (protocolType == 2)
                {
                    key = ImapReceiverHost.CreateKey(ocode, mailBoxId);
                    ImapReceiverHost.LoadMailBox(ocode, mailBoxId, true);
                    ImapReceiverHost.StartImmediately(ocode, mailBoxId);
                    ImapReceiverHost.StatusBags.RegisterKey(key);
                }
                else if (protocolType == 1)
                {
                    key = Pop3ReceiverHost.CreateKey(ocode, mailBoxId);
                    Pop3ReceiverHost.LoadMailBox(ocode, mailBoxId, false);
                    Pop3ReceiverHost.StartImmediately(ocode, mailBoxId);
                    Pop3ReceiverHost.StatusBags.RegisterKey(key);
                }
            }
        }

        public Dictionary<string, List<MailServerStatus>> GetMailBoxReceiveStatus(string companyCode, string[] mailBoxIds)
        {
            Dictionary<string, List<MailServerStatus>> dic = new Dictionary<string, List<MailServerStatus>>();
            if (string.IsNullOrWhiteSpace(companyCode)) return dic;
            if (mailBoxIds == null || mailBoxIds.Length < 1) return dic;
            foreach (string mailBoxId in mailBoxIds)
            {
                if (!dic.ContainsKey(mailBoxId))
                {
                    List<MailServerStatus> list = new List<MailServerStatus>();
                    string key = ImapReceiverHost.CreateKey(companyCode, mailBoxId);
                    if (ImapReceiverHost.ReceiverTasks.ContainsKey(key))
                    {
                        list = ImapReceiverHost.StatusBags.Dequeue(key);
                    }
                    else
                    {
                        key = Pop3ReceiverHost.CreateKey(companyCode, mailBoxId);
                        list = Pop3ReceiverHost.StatusBags.Dequeue(key);
                    }
                    dic.Add(mailBoxId, list);
                }
            }
            return dic;
        }

        #endregion 收取相关

        #region 发送相关
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="companyCode">公司编号</param>
        /// <param name="mailMainIds">邮件ID编号</param>
        public void SendMail(string companyCode, string[] mailMainIds)
        {
            if (mailMainIds == null || mailMainIds.Length < 1) return;
            List<string> list = new List<string>();
            foreach (string mailMainId in mailMainIds)
            {
                list.Add(mailMainId.ToString());
            }
            SmtpSenderHost.SendImmediately(companyCode, list.ToArray());
        }
        /// <summary>
        /// 获取邮件发送状态
        /// </summary>
        /// <param name="companyCode">公司编号</param>
        /// <param name="mailMainIds">邮件ID数组</param>
        /// <returns></returns>
        public Dictionary<string, List<MailServerStatus>> GetMailSendStatus(string companyCode, string[] mailMainIds)
        {
            Dictionary<string, List<MailServerStatus>> dic = new Dictionary<string, List<MailServerStatus>>();
            if (mailMainIds == null || mailMainIds.Length < 1) return dic;
            foreach (string mailMainId in mailMainIds)
            {
                if (!dic.ContainsKey(mailMainId))
                {
                    List<MailServerStatus> list = new List<MailServerStatus>();
                    string key = SmtpSenderHost.CreateKey(companyCode, mailMainId.ToString());
                    list = SmtpSenderHost.StatusBags.Dequeue(key);
                    dic.Add(mailMainId, list);
                }
            }
            return dic;
        }

        #endregion 发送相关
    }
}
