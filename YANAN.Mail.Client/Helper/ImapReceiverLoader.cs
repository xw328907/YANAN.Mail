using System;
using System.Collections.Generic;
using System.Linq;

namespace YANAN.Mail.Client
{
    using MailBee.ImapMail;
    using YANAN.Mail.Core;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Services;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using Titan;

    public class ImapReceiverLoader : ImapReceiverLoaderBase
    {
        /// <summary>
        /// 加载邮箱信息
        /// </summary>
        /// <param name="ocode">公司编号</param>
        /// <param name="mailBoxId">邮箱ID</param>
        /// <returns></returns>
        public override ImapReceiverBase LoadMailBox(string ocode, string mailBoxId)
        {
            ImapReceiver mailReceiver = null;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                QueryExpression query;
                MailBox mailBox = new MailBox
                {
                    MailBoxId = mailBoxId
                };
                db.Select(mailBox);
                if (mailBox.OCode != ocode)
                {
                    return mailReceiver;
                }
                mailReceiver = new ImapReceiver
                {
                    MailAttachFolder = UtilityHelper.MailAttachBaseDirectory,
                    OCode = ocode,
                    MailBoxId = mailBox.MailBoxId,
                    ReceiveDefaultTime = mailBox.ReceiveBeginTime,
                    UserId = mailBox.OwnerUID,
                    TimerMinutes = mailBox.ReceiveTimer >= 1 ? mailBox.ReceiveTimer : 1,
                    KeepDays = mailBox.KeepDays,
                    MailServerAddress = mailBox.PopServer,
                    MailServerPort = mailBox.PopPort,
                    EmailAddress = mailBox.MailAddress,
                    EmailPassword = EncryptHelper.DecodeBase64(mailBox.MailPassword)
                };

                #region imap协议验证是否支持idle模式

                if (mailBox.ProtocolTypeId == 2)
                {
                    try
                    {
                        MailBee.Global.LicenseKey = ConstConfig.MailBeeLicenseKey;
                        Imap imap = new Imap();
                        imap.Connect(mailReceiver.MailServerAddress, mailReceiver.MailServerPort);
                        mailReceiver.IsIdle = imap.GetExtension("IDLE") != null;
                    }
                    catch (Exception)
                    {
                        mailReceiver.IsIdle = false;
                    }
                }

                #endregion imap协议验证是否支持idle模式

                #region 读取文件夹

                query = new QueryExpression
                {
                    EntityType = typeof(MailFolder)
                };
                query.Wheres.Add(MailFolder_.SourceId.TEqual(mailBoxId.ToString()));
                query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
                query.Selects.Add(MailFolder_.MailFolderId);
                query.Selects.Add(MailFolder_.MailType);
                query.Selects.Add(MailFolder_.MailCount);
                query.Selects.Add(MailFolder_.FolderName);
                query.Selects.Add(MailFolder_.ServerFullFolderName);
                query.Selects.Add(MailFolder_.Sorting);
                query.Selects.Add(MailFolder_.ParentId);
                query.Selects.Add(MailFolder_.Depth);
                query.Selects.Add(MailFolder_.LastMailTime);
                MailFolders folders = new MailFolders();
                db.SelectCollection(folders.Items, query);
                if (folders != null && folders.Items.Count > 0)
                {
                    mailReceiver.MailFolderList = folders.Items;
                }

                #endregion 读取文件夹


                #region 读取MessageID
                MailBoxMessages mailBoxMessages;
                HashSet<string> latestMessageIDs;
                //查询出已收取成功的邮件UID
                latestMessageIDs = new HashSet<string>();
                query = new QueryExpression
                {
                    EntityType = typeof(MailBoxMessage)
                };
                query.Selects.Add(MailBoxMessage_.MessageId);
                query.Wheres.Add(MailBoxMessage_.MailBoxId.TEqual(mailBoxId));
                query.Wheres.Add(MailBoxMessage_.InsertType.TNotEqual(2));//加载除错误外的
                query.PageIndex = 1;
                query.PageSize = int.MaxValue;
                query.OrderBys.Add(MailBoxMessage_.MailTime.PropertyName, OrderType.Desc);
                mailBoxMessages = new MailBoxMessages();
                db.SelectCollection(mailBoxMessages.Items, query);
                if (mailBoxMessages != null && mailBoxMessages.Items.Count > 0)
                {
                    foreach (MailBoxMessage mailBoxMessage in mailBoxMessages.Items)
                    {
                        latestMessageIDs.Add(mailBoxMessage.MessageId);
                    }
                }
                mailReceiver.LatestSavedMessageUids = latestMessageIDs;
                //查询收取失败的邮件UID
                latestMessageIDs = new HashSet<string>();
                query = new QueryExpression
                {
                    EntityType = typeof(MailBoxMessage)
                };
                query.Selects.Add(MailBoxMessage_.MessageId);
                query.Wheres.Add(MailBoxMessage_.MailBoxId.TEqual(mailBoxId));
                query.Wheres.Add(MailBoxMessage_.InsertType.TEqual(2));
                query.Wheres.Add(MailBoxMessage_.DeleteErrorNum.TLessThan(3));
                query.PageIndex = 1;
                query.PageSize = int.MaxValue;
                query.OrderBys.Add(MailBoxMessage_.MailTime.PropertyName, OrderType.Desc);
                mailBoxMessages = new MailBoxMessages();
                db.SelectCollection(mailBoxMessages.Items, query);
                if (mailBoxMessages != null && mailBoxMessages.Items.Count > 0)
                {
                    foreach (MailBoxMessage mailBoxMessage in mailBoxMessages.Items)
                    {
                        mailReceiver.LatestSavedErrorMessageUids.Add(mailBoxMessage.MessageId, mailBoxMessage.InsertErrorNum ?? 0);
                    }
                }

                #endregion 读取MessageID

                #region 加载拒收

                query = new QueryExpression
                {
                    EntityType = typeof(MailJudge)
                };
                query.Selects.Add(MailJudge_.ALL);
                query.Wheres.Add(MailJudge_.CreateUID.TEqual(mailBox.OwnerUID));
                query.Wheres.Add(MailJudge_.OperateType.TEqual(2));
                MailJudges mailJudges = new MailJudges();
                db.SelectCollection(mailJudges.Items, query);
                mailReceiver.Blacklist = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                if (mailJudges != null && mailJudges.Items.Count > 0)
                {
                    mailJudges.Items.ForEach(item =>
                    {
                        if (!string.IsNullOrEmpty(item.MailAddress))
                        {
                            mailReceiver.Blacklist.Add(item.MailAddress);
                        }
                    });
                }

                #endregion 加载拒收
            }
            return mailReceiver;
        }

        /// <summary>
        ///  加载所有IMAP协议的邮箱ID
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, List<string>> LoadAllMailBoxIds()
        {
            Dictionary<string, List<string>> mailBoxIds = new Dictionary<string, List<string>>();
            MailBoxs mailBoxs = new MailBoxs();
            QueryExpression q;
            string ocode = CurrentUserInfo.GetLoginedUserInfo().OCode;
            List<string> list = new List<string>();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                mailBoxs = new MailBoxs();
                q = new QueryExpression { EntityType = typeof(MailBox) };
                q.Wheres.Add(MailBox_.Deleted.TEqual(false));
                q.Wheres.Add(MailBox_.ProtocolTypeId.TEqual(2));
                q.Wheres.Add(MailBox_.ReceiveTimer.TLessThan(0));
                q.Wheres.Add(MailBox_.OCode.TEqual(ocode));
                q.Selects.Add(MailBox_.MailBoxId);
                q.PageIndex = 1;
                q.PageSize = int.MaxValue;
                db.SelectCollection(mailBoxs.Items, q);
                list = mailBoxs.Items.Select(x => x.MailBoxId).ToList();
                db.Close();
            }
            if (!mailBoxIds.ContainsKey(ocode))
                mailBoxIds.Add(ocode, list);
            return mailBoxIds;
        }
    }
}
