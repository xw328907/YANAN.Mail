using System;
using System.Collections.Generic;
using System.Linq;

namespace YANAN.Mail.Client
{
    using MailBee.ImapMail;
    using MailBee.Mime;
    using YANAN.Mail.Core;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Services;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using YANAN.Mail.Utilities.Extensions;
    using Titan;

    public class ImapReceiver : ImapReceiverBase
    {
        /// <summary>
        /// 邮件附件存储根目录
        /// </summary>
        public string MailAttachFolder { get; set; }
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
        /// 邮箱文件夹邮件下载完成后执行
        /// </summary>
        /// <param name="savedTotalCount"></param>
        /// <param name="savedTotalSize"></param>
        public override void SaveCompleted(int savedTotalCount, long savedTotalSize)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    SaveMailUpdateFolder(db, savedTotalSize, savedTotalCount);
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
        /// <summary>
        /// 更新邮件所属邮箱和文件夹的数量和大小信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailSize"></param>
        /// <param name="mailCount"></param>
        private void SaveMailUpdateFolder(IDbSession db, long mailSize, int mailCount = 1)
        {
            MailBox mailBox = new MailBox { MailBoxId = MailBoxId };
            db.Select(mailBox, MailBox_.MailCount, MailBox_.MailSize, MailBox_.MailBoxId);
            MailFolder mailFolder = new MailFolder { MailFolderId = CurrentMailFolder.MailFolderId };
            var folderFields = new List<PropertyExpression> { MailFolder_.MailCount, MailFolder_.MailFolderId, MailFolder_.MailType, MailFolder_.UnreadCount };
            db.Select(mailFolder, folderFields);
            folderFields.Remove(MailFolder_.MailType);
            mailFolder.MailCount += mailCount;
            if (mailFolder.MailType == (int)MailFolderEnum.InBox)
            {
                mailFolder.UnreadCount += mailCount;
            }
            else
            {
                if (folderFields.Contains(MailFolder_.UnreadCount))
                    folderFields.Remove(MailFolder_.UnreadCount);
            }
            if (LastestMailTime.HasValue)
            {
                folderFields.Add(MailFolder_.LastMailTime);
                mailFolder.LastMailTime = LastestMailTime.Value;
            }
            mailBox.MailCount += mailCount;
            mailBox.MailSize += mailSize;
            db.Update(mailBox, MailBox_.MailCount, MailBox_.MailSize);
            db.Update(mailFolder, folderFields);
        }
        /// <summary>
        /// 下载邮局文件夹
        /// </summary>
        /// <param name="folderCollection"></param>
        public override void SaveMailFolder(FolderCollection folderCollection)
        {
            IList<MailFolder> folders = new List<MailFolder>();
            if (MailFolderList == null) MailFolderList = new List<MailFolder>();
            string folderSplitString = "";
            int maxSorting = MailFolderList.Count > 0 ? MailFolderList.Max(x => x.Sorting) : 6;
            if (maxSorting < 6) maxSorting = 6;
            foreach (Folder folder in folderCollection)
            {
                if (MailFolderList.Count > 0 && MailFolderList.Any(x => x.ServerFullFolderName == folder.Name)) continue;
                if (string.IsNullOrWhiteSpace(folderSplitString))
                    folderSplitString = folder.Delimiter;
                MailFolder mailFolder = new MailFolder
                {
                    CreateTime = DateTime.Now,
                    CreateUID = UserId,
                    FolderName = folder.ShortName,
                    ServerFullFolderName = folder.Name,
                    Depth = folder.NestingLevel + 1,
                    MailBoxId = MailBoxId,
                    MailCount = 0,
                    MailFolderId = UtilityHelper.GetGuid(),
                    OCode = OCode,
                    OwnerUID = UserId,
                    ParentId = "",
                    LastMailTime = ReceiveDefaultTime,
                    Sorting = 1,
                    SourceId = MailBoxId,
                    SourceTable = MailFolderSourceTableEnum.MailBox.ToString(),
                    UnreadCount = 0
                };
                switch (folder.Flags)
                {
                    case FolderFlags.Inbox:
                        mailFolder.MailType = (int)MailFolderEnum.InBox;
                        mailFolder.Sorting = 1;
                        mailFolder.FolderName = MailFolderEnum.InBox.GetDescription();
                        break;
                    case FolderFlags.Sent:
                        mailFolder.MailType = (int)MailFolderEnum.OutBox;
                        mailFolder.Sorting = 2;
                        break;
                    case FolderFlags.Drafts:
                        mailFolder.MailType = (int)MailFolderEnum.DraftBox;
                        mailFolder.Sorting = 3;
                        break;
                    case FolderFlags.Spam:
                        mailFolder.MailType = (int)MailFolderEnum.TrashBox;
                        mailFolder.Sorting = 4;
                        break;
                    case FolderFlags.Trash:
                        mailFolder.MailType = (int)MailFolderEnum.Deleted;
                        mailFolder.Sorting = 5;
                        break;
                    case FolderFlags.None:
                        mailFolder.MailType = (int)MailFolderEnum.Customize;
                        mailFolder.Sorting = maxSorting + mailFolder.Depth;
                        break;
                }
                if (mailFolder.ServerFullFolderName.ToUpper() == MailFolderEnum.InBox.ToString().ToUpper() && mailFolder.FolderName != MailFolderEnum.InBox.GetDescription())
                {
                    mailFolder.MailType = (int)MailFolderEnum.InBox;
                    mailFolder.FolderName = MailFolderEnum.InBox.GetDescription();//兼容部分邮箱收件箱Flags显示None
                    mailFolder.Sorting = 1;
                }
                folders.Add(mailFolder);
                maxSorting++;
            }
            if (folders.Count > 0)
            {
                foreach (MailFolder folder in folders)
                {
                    if (folder.Depth == 1) continue;//跳过一级文件夹
                    string parentFolderName = folder.ServerFullFolderName;
                    int len = parentFolderName.Length - (folderSplitString + folder.FolderName).Length;
                    if (len < 1) continue;
                    parentFolderName = parentFolderName.Substring(0, len);
                    //先判断上级文件夹是否已存在
                    var parentFolder = MailFolderList.FirstOrDefault(x => x.ServerFullFolderName == parentFolderName && x.Depth == folder.Depth - 1);
                    if (parentFolder != null)
                    { folder.ParentId = parentFolder.MailFolderId; }
                    else
                    {
                        parentFolder = folders.FirstOrDefault(x => x.ServerFullFolderName == parentFolderName && x.Depth == folder.Depth - 1);
                        if (parentFolder != null) { folder.ParentId = parentFolder.MailFolderId; }
                    }
                }
                MailFolderList.AddRange(folders);
                using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
                {
                    try
                    {
                        db.BeginTransaction();
                        foreach (var folder in folders)
                        {
                            db.Insert(folder);
                        }
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
        }
        /// <summary>
        /// 保存邮件
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="messageUid"></param>
        public override void SaveDownloadedMessage(MailMessage mailMessage, string messageUid)
        {
            MailMain mailMain = MailService.CreateMailMain(OCode, mailMessage, messageUid, MailBoxId, UserId, MailAttachFolder, EmailAddress);
            if (!LastestMailTime.HasValue || LastestMailTime.Value < mailMain.MailTime)
                LastestMailTime = mailMain.MailTime;
            mailMain.Viewed = CurrentMailFolder.MailType != (int)MailFolderEnum.InBox;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailService.InsertMailFromReceive(db, mailMain, mailMain.MailBody, mailMain.MailAttachs, CurrentMailFolder.MailFolderId);
                    SaveMailUpdateFolder(db, mailMessage.SizeOnServer);
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
        /// <summary>
        /// 保存下载错误邮件UID
        /// </summary>
        /// <param name="messageUid"></param>
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
                    db.Update(mailBoxMessage, MailBoxMessage_.InsertType, MailBoxMessage_.InsertTime, MailBoxMessage_.InsertErrorNum);
                }
                #endregion
            }
        }
        /// <summary>
        /// 保存拒收邮件UID记录
        /// </summary>
        /// <param name="messageUid"></param>
        /// <param name="mailTime"></param>
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
                    InsertType = 3,
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

        /// <summary>
        /// 检查邮件是否已存在本地,本地发送出去的,如存在返回true则不应继续保存当前邮件
        /// </summary>
        /// <param name="messageUID">邮件MessageUid</param>
        /// <param name="msgUid">邮件在邮局中唯一UID</param>
        public override bool CheckMailFromSystem(string messageUID, string msgUid)
        {
            if (string.IsNullOrWhiteSpace(messageUID)) return false;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                QueryExpression query = new QueryExpression { EntityType = typeof(MailMain) };
                query.Selects.Add(MailMain_.MailMainId);
                query.Selects.Add(MailMain_.MailTime);
                query.Selects.Add(MailMain_.MailBoxId);
                query.Wheres.Add(MailMain_.MessageId.TEqual(messageUID));
                query.Wheres.Add(MailMain_.MailBoxId.TEqual(MailBoxId));
                query.Wheres.Add(MailMain_.OwnerUID.TEqual(UserId));
                var mailMain = db.SelectOne<MailMain>(query);
                if (mailMain != null && !string.IsNullOrWhiteSpace(mailMain.MailMainId))
                {
                    mailMain.MessageId = msgUid;
                    db.Update(mailMain, MailMain_.MessageId);
                    MailBoxMessage mailBoxMessage = new MailBoxMessage
                    {
                        MailBoxId = mailMain.MailBoxId,
                        MessageId = mailMain.MessageId,
                        MailTime = mailMain.MailTime,
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
                    return true;
                }
            }
            return false;
        }
    }
}
