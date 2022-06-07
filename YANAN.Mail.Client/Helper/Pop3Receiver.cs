using System;
using System.Linq;
using System.Collections.Generic;

namespace YANAN.Mail.Client
{
    using Titan;
    using YANAN.Mail.Utilities.Enums;
    using MailBee.Mime;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Services;
    using YANAN.Mail.Core;

    public class Pop3Receiver : Pop3ReceiverBase
    {
        /// <summary>
        /// 邮件附件存储根目录
        /// </summary>
        public string MailAttachFolder { get; set; }
        /// <summary>
        /// 站点根目录
        /// </summary>
        public string SiteFolder { get; set; }
        /// <summary>
        /// 用户(操作员)编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string OCode { get; set; }
        /// <summary>
        /// 邮箱ID编号
        /// </summary>
        public string MailBoxId { get; set; }
        /// <summary>
        /// 服务器端保留天数
        /// </summary>
        public int KeepDays { get; set; }
        /// <summary>
        /// 默认文件夹ID编号
        /// </summary>
        public string DefaultFolderId { get; set; }


        public override void SaveLatest(MailFolder folder, int savedTotalCount, long savedTotalSize)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailService.UpdateMailCountAndSize(db, OCode, UserId, MailBoxId, folder.MailFolderId, folder.MailType == (int)MailFolderEnum.InBox, savedTotalSize, savedTotalCount);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    OnWorkError(ex);
                    throw ex;
                }
            }
        }

        public override void SaveDownloadedMessage(MailMessage mailMessage, string messageUid)
        {
            MailMain mailMain = MailService.CreateMailMain(OCode, mailMessage, messageUid, MailBoxId, UserId, MailAttachFolder, UserName);
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailService.InsertMailFromReceive(db, mailMain, mailMain.MailBody, mailMain.MailAttachs, DefaultFolderId);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    OnWorkError(ex);
                    throw ex;
                }
            }
            ////更新全文索引
            //IndexHost.UpdateIndex(OCode, UserId);
        }
        public override void SaveErrorMessage(string messageUid)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                #region 保存MessageID
                MailBoxMessage mailBoxMessage = new MailBoxMessage
                {
                    MailBoxId = MailBoxId,
                    MessageId = messageUid,
                    MailTime = DateTime.Now,
                    InsertType = 2,
                    InsertTime = DateTime.Now,
                    InsertErrorNum = 1,
                    DeleteType = 0,
                    DeleteTime = DateTime.Now
                };
                if (!db.Exists(mailBoxMessage))
                {
                    db.Insert(mailBoxMessage);
                }
                else
                {
                    db.Select(mailBoxMessage);
                    mailBoxMessage.InsertErrorNum = mailBoxMessage.InsertErrorNum ?? 0 + 1;
                    db.Update(mailBoxMessage, MailBoxMessage_.InsertType, MailBoxMessage_.InsertTime);
                }
                #endregion
            }
        }
        public override void SaveRejectedMessage(string messageUid, DateTime mailTime)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                #region 保存MessageID
                MailBoxMessage mailBoxMessage = new MailBoxMessage
                {
                    MailBoxId = MailBoxId,
                    MessageId = messageUid,
                    MailTime = mailTime,
                    InsertType = 1,
                    InsertTime = DateTime.Now,
                    DeleteType = 0,
                    DeleteTime = DateTime.Now
                };
                if (!db.Exists(mailBoxMessage))
                {
                    db.Insert(mailBoxMessage);
                }
                else
                {
                    db.Update(mailBoxMessage, MailBoxMessage_.InsertType, MailBoxMessage_.InsertTime);
                }
                #endregion
            }
        }

        public override void SaveDeleteMessages(List<string> deleteMessageUids)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    deleteMessageUids.ForEach(msgId =>
                    {
                        MailBoxMessage mailBoxMessage = new MailBoxMessage
                        {
                            MailBoxId = MailBoxId,
                            MessageId = msgId,
                            DeleteType = 1,
                            DeleteTime = DateTime.Now
                        };
                        db.Update(mailBoxMessage, MailBoxMessage_.DeleteType, MailBoxMessage_.DeleteTime);
                    });
                    db.Commit();
                }
                catch
                {
                    db.Rollback();
                }
            }
        }

        public override HashSet<string> LoadDeleteMessageUids()
        {
            HashSet<string> list = new HashSet<string>();
            if (KeepDays >= 36500) { return list; }//100年永久保留,跳过数据查询
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                QueryExpression q = new QueryExpression
                {
                    EntityType = typeof(MailBoxMessage)
                };
                q.Selects.Add(MailBoxMessage_.MessageId);
                q.Wheres.Add(MailBoxMessage_.MailBoxId.TEqual(MailBoxId));
                q.Wheres.Add(MailBoxMessage_.DeleteType.TEqual(0));
                q.Wheres.Add(MailBoxMessage_.InsertType.TEqual(1));
                q.Wheres.Add(MailBoxMessage_.MailTime.TLessThan(DateTime.Now.AddDays(-KeepDays)));
                q.PageIndex = 1;
                q.PageSize = 100;//为了提高性能，每次只处理一部分
                MailBoxMessages mailBoxMessages = new MailBoxMessages();
                db.SelectCollection(mailBoxMessages.Items, q);
                foreach (MailBoxMessage mailBoxMessage in mailBoxMessages.Items)
                {
                    list.Add(mailBoxMessage.MessageId);
                }
            }
            return list;
        }
    }
}
