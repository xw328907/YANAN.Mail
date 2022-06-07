using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YANAN.Mail.Services
{
    using MailBee.Mime;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using YANAN.Mail.Utilities.Extensions;
    using Titan;

    public class MailService : IServices.IMailService
    {
        IServices.IMailFilterService _mailFilterServices = new MailFilterService();

        #region 邮件

        public EntityList<MailMain> GetListMailMain(LoginedUserInfo loginInfo, MailSearchDto searchDto)
        {
            if (searchDto == null)
            {
                return new EntityList<MailMain>();
            }
            ConditionExpressionCollection conditions;
            QueryExpression query = new QueryExpression { EntityType = typeof(MailMainFolder) };
            query.OrderBys.Add(MailMainFolder_.MailMain.IsTop.PropertyName, OrderType.Desc);
            query.OrderBys.Add(MailMainFolder_.MailMain.MailTime.PropertyName, OrderType.Desc);
            query.Selects.Add(MailMainFolder_.MailMain.ALL);
            query.Selects.Add(MailMainFolder_.MailFolderId);
            query.Selects.Add(MailMainFolder_.MailFolder.MailType);
            query.Wheres.Add(MailMainFolder_.MailMain.OCode.TEqual(loginInfo.OCode));
            query.Wheres.Add(MailMainFolder_.MailMain.OwnerUID.TEqual(loginInfo.UserId));
            query.PageIndex = 1;
            query.PageSize = int.MaxValue;
            query.ReturnMatchedCount = true;
            query.Wheres.Add(MailMainFolder_.MailMain.Deleted.TEqual(searchDto.Deleted != true ? false : true));
            if (searchDto.UnRead == true)
                query.Wheres.Add(MailMainFolder_.MailMain.Viewed.TEqual(false));
            if (!string.IsNullOrWhiteSpace(searchDto.MailBoxId))
                query.Wheres.Add(MailMainFolder_.MailMain.MailBoxId.TEqual(searchDto.MailBoxId));
            if (!string.IsNullOrWhiteSpace(searchDto.MailFolderId))
                query.Wheres.Add(MailMainFolder_.MailFolderId.TEqual(searchDto.MailFolderId));
            if (!string.IsNullOrWhiteSpace(searchDto.MailFolderSourceTableCode))
            {
                query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(searchDto.MailFolderSourceTableCode));
            }
            else { query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString())); }
            if (!string.IsNullOrWhiteSpace(searchDto.MailLabelId))
            {
                query.Wheres.Add(MailMainFolder_.MailFolder.SourceId.TEqual(searchDto.MailLabelId));
                query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(MailFolderSourceTableEnum.MailLabel.ToString()));
            }
            if (searchDto.IsLabelMail == true)
            {
                conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
                conditions.Add(MailMainFolder_.MailMain.LabelInfo.TNotEqual(null));
                conditions.Add(MailMainFolder_.MailMain.LabelInfo.TNotEqual(string.Empty));
                query.Wheres.Add(conditions);
            }
            if (searchDto.IsTopMail == true)
            {
                query.Wheres.Add(MailMainFolder_.MailMain.IsTop.TEqual(true));
            }
            if (!string.IsNullOrWhiteSpace(searchDto.Keyword))
            {
                conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
                conditions.Add(MailMainFolder_.MailMain.Subject.PropertyName, ConditionOperator.Like, searchDto.Keyword);
                conditions.Add(MailMainFolder_.MailMain.Receiver.PropertyName, ConditionOperator.Like, searchDto.Keyword);
                conditions.Add(MailMainFolder_.MailMain.Sender.PropertyName, ConditionOperator.Like, searchDto.Keyword);
                conditions.Add(MailMainFolder_.MailMain.Cc.PropertyName, ConditionOperator.Like, searchDto.Keyword);
                query.Wheres.Add(conditions);
            }
            conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
            if (!string.IsNullOrWhiteSpace(searchDto.Receiver))
            {
                conditions.Add(MailMainFolder_.MailMain.Receiver.PropertyName, ConditionOperator.Like, searchDto.Receiver);
            }
            if (!string.IsNullOrWhiteSpace(searchDto.Sender))
            {
                conditions.Add(MailMainFolder_.MailMain.Sender.PropertyName, ConditionOperator.Like, searchDto.Sender);
            }
            if (conditions.Count > 0) query.Wheres.Add(conditions);
            MailMainFolders entities = new MailMainFolders();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                entities.TotalCount = db.SelectCollection(entities.Items, query);
                EntityList<MailMain> list = new EntityList<MailMain>
                {
                    TotalCount = entities.TotalCount
                };
                if (entities.TotalCount > 0)
                {
                    entities.Items.ForEach(item =>
                    {
                        var dto = item.MailMain;
                        dto.MailFolderId = item.MailFolderId;
                        dto.MailFolderType = item.MailFolder.MailType;
                        list.Items.Add(dto);
                    });
                }
                return list;
            }
        }

        public MailMain GetMailMain(string mailId)
        {
            MailMain mailMain = new MailMain { MailMainId = mailId };
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                bool flag = db.Select(mailMain, MailMain_.ALL, MailMain_.MailBody.ALL);
                MailAttachs attachs = new MailAttachs();
                QueryExpression query = new QueryExpression { EntityType = typeof(MailAttach) };
                query.Wheres.Add(MailAttach_.MailMainId.TEqual(mailId));
                query.Wheres.Add(MailAttach_.ContentID.TEqual(string.Empty));////不读取邮局内嵌附件(常为图片)
                query.Selects.Add(MailAttach_.ALL);
                db.SelectCollection(attachs.Items, query);
                mailMain.MailAttachs = attachs.Items;
            }
            return mailMain;
        }

        /// <summary>
        /// 邮件删除
        /// </summary>
        /// <param name="mailIds"></param>
        /// <returns></returns>
        public ResponseResult RemoveMail(LoginedUserInfo loginInfo, string[] mailIds)
        {
            ResponseResult result = new ResponseResult();
            if (mailIds == null || mailIds.Length < 1)
            {
                return UtilityHelper.ReturnParameterNull();
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                result = RemoveMail(db, mailIds, loginInfo.OCode);
                result.Data = mailIds;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailIds"></param>
        /// <param name="ocode"></param>
        /// <param name="deleted"></param>
        /// <returns></returns>
        private static ResponseResult RemoveMail(IDbSession db, string[] mailIds, string ocode, bool deleted = false)
        {
            ResponseResult result = new ResponseResult();
            QueryExpression query;
            foreach (var id in mailIds)
            {
                if (string.IsNullOrWhiteSpace(id)) continue;
                query = new QueryExpression { EntityType = typeof(MailMainFolder) };
                query.Selects.Add(MailMainFolder_.MailFolderId);
                query.Selects.Add(MailMainFolder_.MailMainId);
                query.Selects.Add(MailMainFolder_.MailFolder.UnreadCount);
                query.Selects.Add(MailMainFolder_.MailFolder.MailCount);
                query.Selects.Add(MailMainFolder_.MailMain.MailBoxId);
                query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
                query.Wheres.Add(MailMainFolder_.MailMainId.TEqual(id));
                var mailMainFolder = db.SelectOne<MailMainFolder>(query);
                if (mailMainFolder == null)
                {
                    continue;
                }
                query = new QueryExpression { EntityType = typeof(MailFolder) };
                query.Selects.Add(MailFolder_.MailFolderId);
                query.Wheres.Add(MailFolder_.MailBoxId.TEqual(mailMainFolder.MailMain.MailBoxId));
                query.Wheres.Add(MailFolder_.MailType.TEqual((int)MailFolderEnum.Deleted));
                var trashMailFolder = db.SelectOne<MailFolder>(query);//取出已删除文件夹ID
                                                                      //如果当前邮件已在已删除箱则执行彻底删除  
                if (!deleted)//如果未强制彻底删除则再判断是否已再删除箱
                    deleted = mailMainFolder.MailFolderId == trashMailFolder.MailFolderId;
                try
                {
                    db.BeginTransaction();
                    MailMain mail = new MailMain { MailMainId = mailMainFolder.MailMainId };
                    bool flag = db.Select(mail);
                    if (!flag)
                    {
                        throw new Exception("邮件不存在或已删除");
                    }
                    SQLiteCommand command = new SQLiteCommand();
                    string sql = string.Empty;
                    if (mail.Deleted != true)
                    {
                        if (mail.OCode != ocode)
                        {
                            throw new Exception("非法操作");
                        }
                        sql += "UPDATE MailMain SET Deleted=1 WHERE OCode=@OCode AND MailMainId=@MailMainId;";
                        sql += "UPDATE MailFolder SET MailCount=CASE WHEN MailCount>0 THEN MailCount-1 ELSE 0 END " + (mail.Viewed == false ? ",UnreadCount=CASE WHEN UnreadCount>0 THEN UnreadCount-1 ELSE 0 END" : "") + " WHERE OCode=@OCode AND MailFolderId IN(SELECT MailFolderId FROM MailMainFolder WHERE MailMainId=@MailMainId);";
                        command.Parameters.Add("@OCode", DbType.String).Value = mail.OCode;
                        command.Parameters.Add("@MailMainId", DbType.String).Value = mail.MailMainId;
                    }
                    if (deleted == true)
                    {
                        trashMailFolder.MailFolderId = string.Empty;
                        sql += "UPDATE MailBox SET MailCount=CASE WHEN MailCount>0 THEN MailCount-1 ELSE 0 END,MailSize=CASE WHEN MailSize>@MailSize THEN MailSize-@MailSize ELSE 0 END WHERE OCode=@OCode AND MailBoxId=@MailBoxId;";
                        command.Parameters.Add("@OCode", DbType.String).Value = mail.OCode;
                        command.Parameters.Add("@MailBoxId", DbType.String).Value = mail.MailBoxId;
                        command.Parameters.Add("@MailSize", DbType.Int32).Value = mail.MailSize;
                    }
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;
                        db.ExecuteNonQuery(command);
                    }

                    //将邮件移入已删除/彻底删除
                    MoveMailMainToFolder(db, mailMainFolder.MailFolderId, trashMailFolder.MailFolderId, mailMainFolder.MailMainId);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    result.Message = ex.Message;
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                }
            }
            return result;
        }
        /// <summary>
        /// 设置邮件为已读/未读状态
        /// </summary>
        /// <param name="mailIds">邮件ID编号数组</param>
        /// <param name="read">阅读状态,true =设为已读（默认值）；false=设为未读</param>
        /// <returns></returns>
        public ResponseResult SetMailReadStatus(string[] mailIds, bool read = true)
        {
            ResponseResult result = new ResponseResult();
            if (mailIds == null || mailIds.Length < 1)
            {
                result.Message = "不存在待处理邮件";
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.Code = ResponseCodeEnum.ERROR.ToString();
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    int num = SetMailReadStatus(db, read, mailIds);
                    db.Commit();
                    result.Data = num;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                }
            }
            return result;
        }
        /// <summary>
        /// 设置邮箱文件夹全部邮件已读
        /// </summary>
        /// <param name="mailFolderId">邮件文件夹ID编号</param>
        /// <returns></returns>
        public ResponseResult SetMailFolderRead(LoginedUserInfo userInfo, string mailFolderId)
        {
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrWhiteSpace(mailFolderId))
            {
                result.Message = "不存在待处理邮件";
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.Code = ResponseCodeEnum.ERROR.ToString();
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    SQLiteCommand command = new SQLiteCommand();
                    if (mailFolderId != MailFolderCustomIdEnum.all_noread_mail.ToString())
                    {
                        string sql = "update MailFolder set UnreadCount=0 where MailFolderId =@MailFolderId;";
                        sql += "update MailMain set Viewed=1 from MailMain left join MailMainFolder mmf on MailMain.MailMainId=mmf.MailMainId where MailMain.Viewed=0 and mmf.MailFolderId=@MailFolderId;";
                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("@MailFolderId", DbType.String).Value = mailFolderId;
                        db.ExecuteNonQuery(command);
                    }
                    else
                    {
                        string sql = "update MailFolder set UnreadCount=0 where OwnerUID =@OwnerUID;";
                        sql += "update MailMain set Viewed=1 where Viewed=0 and OwnerUID=@OwnerUID;";
                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("@OwnerUID", DbType.String).Value = userInfo.UserId;
                        db.ExecuteNonQuery(command);
                    }
                    command.Dispose();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                }
            }
            return result;
        }
        /// <summary>
        /// 邮件发送(常规发送/转发/回复)
        /// </summary>
        /// <param name="mailMain"></param>
        /// <returns></returns>
        public ResponseResult MailSend(LoginedUserInfo loginInfo, MailMain mailMain)
        {
            ResponseResult result = new ResponseResult();
            if (mailMain == null)
            {
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.GetDescription();
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
            }
            if (string.IsNullOrWhiteSpace(mailMain.Receiver))
            {
                result.Message = "收件人不能为空";
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailMainSave(loginInfo, db, mailMain, mailMain.MailBody, new MailAttachs { Items = mailMain.MailAttachs, TotalCount = mailMain.MailAttachs.Count });

                    #region 处理转发/回复次数

                    if ((mailMain.FromTypeId == 1 || mailMain.FromTypeId == 2) && !string.IsNullOrWhiteSpace(mailMain.FromId))
                    {
                        MailMain fromMainMain = new MailMain
                        {
                            MailMainId = mailMain.FromId
                        };
                        List<PropertyExpression> fields = new List<PropertyExpression>();
                        if (mailMain.FromTypeId == 1)
                        {
                            fields.Add(MailMain_.ReplyCount);
                        }
                        else if (mailMain.FromTypeId == 2)
                        {
                            fields.Add(MailMain_.ForwardCount);
                        }
                        db.Select(fromMainMain, fields);
                        if (mailMain.FromTypeId == 1)
                        {
                            fromMainMain.ReplyCount += 1;
                        }
                        else if (mailMain.FromTypeId == 2)
                        {
                            fromMainMain.ForwardCount += 1;
                        }
                        db.Update(fromMainMain, fields);
                    }

                    #endregion 处理转发/回复次数

                    if (mailMain.MailState == MailStateEnum.DRAFT.ToString() || mailMain.MailState == MailStateEnum.SEND_FAIL.ToString() || mailMain.MailState == MailStateEnum.SEND_FAIL_END.ToString())
                    {
                        mailMain.MailState = MailStateEnum.WAIT_SEND.ToString();
                        db.Update(mailMain, MailMain_.MailState);
                    }
                    db.Commit();
                    result.Data = mailMain.MailMainId;
                    result.Message = "邮件发送中";
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// 邮件保存,存草稿
        /// </summary>
        /// <param name="mailMain"></param>
        /// <returns></returns>
        public ResponseResult MailSave(LoginedUserInfo loginInfo, MailMain mailMain)
        {
            ResponseResult result = new ResponseResult();
            if (mailMain == null)
            {
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.GetDescription();
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailMainSave(loginInfo, db, mailMain, mailMain.MailBody, new MailAttachs { Items = mailMain.MailAttachs, TotalCount = mailMain.MailAttachs.Count });
                    db.Commit();
                    result.Data = mailMain.MailMainId;
                    result.Message = "保存成功";
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    //throw ex;
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// 邮件信息保存
        /// </summary>
        /// <param name="db">数据库连接</param>
        /// <param name="mailMain">邮件主信息对象</param>
        /// <param name="mailBody">邮件正文对象</param>
        /// <param name="mailAttachs">邮件附件对象集合</param>
        private void MailMainSave(LoginedUserInfo loginInfo, IDbSession db, MailMain mailMain, MailBody mailBody, MailAttachs mailAttachs)
        {
            bool isNew = string.IsNullOrWhiteSpace(mailMain.MailMainId);
            if (mailBody.BodyHtml == null) { mailBody.BodyHtml = string.Empty; }
            if (mailBody.BodyText == null) { mailBody.BodyText = string.Empty; }
            if (string.IsNullOrWhiteSpace(mailBody.BodyText)) mailBody.BodyText = UtilityHelper.Nohtml(mailBody.BodyHtml);
            mailMain.MailSize = mailBody.BodyHtml.Length;//大致计算
            if (!string.IsNullOrWhiteSpace(mailMain.MailBoxId))
            {
                MailBox mailBox = new MailBox { MailBoxId = mailMain.MailBoxId };//读取邮箱信息赋值于发件人 
                bool flag = db.Select(mailBox);
                if (!flag || mailBox == null)
                {
                    throw new Exception("发件邮箱不存在");
                }
                mailMain.Sender = UtilityHelper.GetMailAddressText(mailBox.MailAddress, mailBox.NickName);
            }
            if (mailMain.IsReadReply)
            { mailMain.Reply = mailMain.Sender; }
            else
            { mailMain.Reply = string.Empty; }
            #region 初始默认值赋值 主要用于新增
            if (mailMain.Receiver == null) mailMain.Receiver = string.Empty;
            if (mailMain.Sender == null) mailMain.Sender = string.Empty;
            if (mailMain.MailBoxId == null) mailMain.MailBoxId = string.Empty;
            if (mailMain.Subject == null) mailMain.Subject = string.Empty;
            if (mailMain.LabelInfo == null) mailMain.LabelInfo = string.Empty;
            mailMain.IsSync = false;
            mailMain.IsArchived = false;
            mailMain.AttachCount = mailAttachs.Items.Count;
            mailMain.CreateTime = DateTime.Now;
            mailMain.CreateUID = loginInfo.UserId;
            mailMain.OCode = loginInfo.OCode;
            mailMain.Deleted = false;
            mailMain.ForwardCount = 0;
            mailMain.MailTime = mailMain.CreateTime;
            mailMain.IsComplete = true;
            mailMain.IsTop = false;
            mailMain.IsMemo = false;
            if (!mailMain.IsTimer.HasValue || !mailMain.TimerSendTime.HasValue)
                mailMain.IsTimer = false;
            mailMain.MailState = MailStateEnum.DRAFT.ToString();
            mailMain.MailTime = DateTime.Now;
            mailMain.MailType = (int)MailTypeEnum.OutBox;
            if (string.IsNullOrWhiteSpace(mailMain.MessageId))
                mailMain.MessageId = "mail-" + UtilityHelper.GetGuid();
            mailMain.OwnerUID = mailMain.CreateUID;
            mailMain.Deleted = false;
            mailMain.ForwardCount = 0;
            mailMain.ReplyCount = 0;
            mailMain.Viewed = true;
            mailMain.MailState = MailStateEnum.DRAFT.ToString();
            #endregion 初始默认值赋值
            if (mailAttachs != null && mailAttachs.Items.Count > 0)
            {
                mailAttachs.Items.ForEach(attach =>
                {
                    mailMain.MailSize += attach.ActualSize ?? 0;
                });
            }
            QueryExpression query;
            if (!isNew)
            {
                MailMain oldMailMain = new MailMain
                {
                    MailMainId = mailMain.MailMainId
                };
                bool flag = db.Select(oldMailMain, MailMain_.MailState, MailMain_.MailBoxId);
                if (flag)//当前ID的邮件存在
                {
                    #region 邮件修改保存
                    if (oldMailMain != null && oldMailMain.MailState != MailStateEnum.DRAFT.ToString())
                    {
                        //规避前端已点击发送，继续自动保存
                        return;
                    }
                    List<PropertyExpression> updates = new List<PropertyExpression> {
                  MailMain_.AttachCount,MailMain_.Cc,MailMain_.Bcc,MailMain_.FromId,MailMain_.FromTypeId,MailMain_.Importance,MailMain_.IsGroup
                  ,MailMain_.Reply,MailMain_.IsTimer,MailMain_.IsTracking,MailMain_.MailBoxId,MailMain_.MailSize,MailMain_.MailTime
                  ,MailMain_.Receiver,MailMain_.Sender,MailMain_.Subject,MailMain_.TimerSendTime
                    };

                    db.Update(mailMain, updates);
                    mailBody.MailMainId = mailMain.MailMainId;
                    db.Update(mailBody);

                    #region 切换发送邮箱
                    if (oldMailMain.MailBoxId != mailMain.MailBoxId)
                    {
                        //先找出旧的文件夹 
                        query = new QueryExpression() { EntityType = typeof(MailMainFolder) };
                        query.Wheres.Add(MailMainFolder_.MailFolder.SourceId.TEqual(oldMailMain.MailBoxId));
                        query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
                        query.Wheres.Add(MailMainFolder_.MailMainId.TEqual(oldMailMain.MailMainId));
                        query.Selects.Add(MailMainFolder_.ALL);
                        query.Selects.Add(MailMainFolder_.MailFolder.MailType);
                        MailMainFolder oldMailMainFolder = db.SelectOne<MailMainFolder>(query);
                        //找出新的文件夹
                        MailMainFolder newMailMainFolder = new MailMainFolder();//邮件草稿箱
                        query = new QueryExpression() { EntityType = typeof(MailFolder) };
                        query.Wheres.Add(MailFolder_.SourceId.TEqual(mailMain.MailBoxId));
                        query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
                        query.Wheres.Add(MailFolder_.MailType.TEqual((int)MailFolderEnum.DraftBox));
                        query.Selects.Add(MailFolder_.MailFolderId);
                        var mailFolder = db.SelectOne<MailFolder>(query);
                        if (mailFolder == null)
                        {
                            throw new Exception("读取邮箱草稿箱失败");
                        }
                        //先删除再插入
                        db.Delete(oldMailMainFolder);
                        db.Insert(newMailMainFolder);
                    }
                    #endregion  切换发送邮箱

                    #endregion 邮件修改保存
                }
                else { NewMailSave(db, mailMain, mailBody); }
            }
            else
            {
                NewMailSave(db, mailMain, mailBody);
            }

            #region 附件
            if (mailAttachs != null && mailAttachs.Items != null)
            {
                void insertAttach(MailAttach attach)
                {
                    attach.MailMainId = mailMain.MailMainId;
                    if (attach.ContentID == null) attach.ContentID = string.Empty;
                    attach.CreateTime = DateTime.Now;
                    attach.CreateUID = loginInfo.UserId;
                    attach.OCode = loginInfo.OCode;
                    db.Insert(attach);
                }
                foreach (MailAttach attach in mailAttachs.Items)
                {
                    if (attach.MailAttachId < 1)
                    {
                        insertAttach(attach);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(attach.MailMainId) && attach.MailMainId != mailMain.MailMainId)
                        {//说明该附件为引用,需重新插入
                            insertAttach(attach);
                        }
                        else if (string.IsNullOrWhiteSpace(attach.MailMainId))
                        {
                            attach.MailMainId = mailMain.MailMainId;
                            db.Update(attach, MailAttach_.MailMainId);
                        }
                    }
                }
            }
            #endregion 附件

            #region 分别发送/收件抄送独立保存

            MailGroups mailGroups = new MailGroups();
            query = new QueryExpression() { EntityType = typeof(MailGroup) };
            query.Selects.Add(MailGroup_.ReceiveTypeCode);
            query.Selects.Add(MailGroup_.ReceiveAddress);
            query.Selects.Add(MailGroup_.MailMainId);
            query.Selects.Add(MailGroup_.MailGroupId);
            query.Wheres.Add(MailGroup_.MailMainId.TEqual(mailMain.MailMainId));
            query.PageSize = int.MaxValue;
            query.PageIndex = 1;
            db.SelectCollection(mailGroups.Items, query);
            var listGroup = mailGroups.Items;
            void SaveMailGroup(Dictionary<string, string> keys, int receiveTypeCode)
            {
                foreach (var dic in keys)
                {
                    var group = listGroup.FirstOrDefault(x => x.ReceiveAddress == dic.Key && x.ReceiveTypeCode == receiveTypeCode);
                    if (group != null && group.MailGroupId > 0)
                    {
                        group.ReceiveAddress = dic.Key;
                        group.ReceiveName = dic.Value;
                        db.Update(group, MailGroup_.ReceiveAddress, MailGroup_.ReceiveName);
                        listGroup.Remove(group);
                    }
                    else
                    {
                        group = new MailGroup
                        {
                            MailMainId = mailMain.MailMainId,
                            IsReceive = null,
                            IsSend = null,
                            ReceiveAddress = dic.Key,
                            ReceiveName = dic.Value,
                            ReceiveTypeCode = (int)MailGroupReceiveTypeEnum.Receiver,
                            Sorting = listGroup.Count + 1,
                            SendTime = mailMain.MailTime
                        };
                        db.Insert(group);
                    }
                }
            }
            var dicReceiver = UtilityHelper.GetMailReceiverGroup(mailMain.Receiver);
            SaveMailGroup(dicReceiver, (int)MailGroupReceiveTypeEnum.Receiver);
            if (!mailMain.IsGroup)
            {
                dicReceiver = UtilityHelper.GetMailReceiverGroup(mailMain.Cc);
                SaveMailGroup(dicReceiver, (int)MailGroupReceiveTypeEnum.Cc);
                dicReceiver = UtilityHelper.GetMailReceiverGroup(mailMain.Bcc);
                SaveMailGroup(dicReceiver, (int)MailGroupReceiveTypeEnum.Bcc);
            }
            if (listGroup.Count > 0)
            {
                ConditionExpression cs = new ConditionExpression(MailGroup_.MailGroupId.PropertyName, ConditionOperator.In, string.Join(",", listGroup.Select(x => x.MailGroupId)));
                db.BatchDelete<MailGroup>(cs);
            }

            #endregion 分别发送/收件抄送独立保存
        }
        /// <summary>
        /// 新写邮件
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailMain"></param>
        /// <param name="mailBody"></param>
        private void NewMailSave(IDbSession db, MailMain mailMain, MailBody mailBody)
        {
            mailMain.MailMainId = UtilityHelper.GetGuid();
            MailMainFolder mailMainFolder = new MailMainFolder();//邮件草稿箱
            QueryExpression query = new QueryExpression() { EntityType = typeof(MailFolder) };
            query.Wheres.Add(MailFolder_.SourceId.TEqual(mailMain.MailBoxId));
            query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
            query.Wheres.Add(MailFolder_.MailType.TEqual((int)MailFolderEnum.DraftBox));
            query.Selects.Add(MailFolder_.MailFolderId);
            var mailFolder = db.SelectOne<MailFolder>(query);
            if (mailFolder == null)
            {
                throw new Exception("请先收取同步邮箱文件夹");
            }
            //存入草稿箱
            mailMainFolder.MailFolderId = mailFolder.MailFolderId;
            mailMainFolder.MailMainId = mailMain.MailMainId;
            db.Insert(mailMainFolder);
            //邮件保存
            db.Insert(mailMain);
            mailBody.MailMainId = mailMain.MailMainId;
            db.Insert(mailBody);
        }

        /// <summary>
        /// 移动邮件至文件夹,逻辑如下：
        /// 1、参数不能为空/null
        /// 2、收件不能移入发件箱/草稿箱
        /// 3、发件/草稿不能移入收件箱
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <param name="toMailFolderId">移入的文件夹ID</param>
        /// <returns></returns>
        public ResponseResult MoveMailToFolder(string mailId, string toMailFolderId)
        {
            if (string.IsNullOrWhiteSpace(mailId) || string.IsNullOrWhiteSpace(toMailFolderId))
            {
                return UtilityHelper.ReturnParameterNull();
            }
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {

                bool flag = false;
                MailFolder mailFolder = new MailFolder { MailFolderId = toMailFolderId };
                flag = db.Select(mailFolder);
                if (!flag)
                {
                    result.Message = "目标文件夹不存在或已删除";
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                    return result;
                }
                QueryExpression query = new QueryExpression { EntityType = typeof(MailMainFolder) };
                query.Selects.Add(MailMainFolder_.MailFolderId);
                query.Selects.Add(MailMainFolder_.MailMainId);
                query.Selects.Add(MailMainFolder_.MailMain.MailType);
                query.Selects.Add(MailMainFolder_.MailFolder.MailType);
                query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(mailFolder.SourceTable));
                query.Wheres.Add(MailMainFolder_.MailMainId.TEqual(mailId));
                var mailMainFolder = db.SelectOne<MailMainFolder>(query);
                if (mailMainFolder == null)
                {
                    result.Message = "邮件不存在或已删除";
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                    return result;
                }
                try
                {
                    db.BeginTransaction();
                    MoveMailMainToFolder(db, mailMainFolder.MailFolderId, mailFolder.MailFolderId, mailMainFolder.MailMainId);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                }
            }
            return result;
        }
        /// <summary>
        /// 设置邮件置顶/取消置顶
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="isTop"></param>
        public ResponseResult SetMailTopStatus(bool isTop = true, params string[] mailIds)
        {
            if (mailIds == null || mailIds.Length < 1) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            var ids = "'" + string.Join("','", mailIds) + "'";
            MailMain mailMain = new MailMain
            {
                IsTop = isTop
            };
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                db.BatchUpdate(mailMain, new ConditionExpression(MailMain_.MailMainId.PropertyName, ConditionOperator.In, ids), MailMain_.IsTop);
                result.Data = mailIds;
            }
            return result;
        }
        /// <summary>
        /// 设置邮件已读/未读
        /// </summary>
        public static int SetMailReadStatus(IDbSession db, bool read = true, params string[] mailIds)
        {
            var ids = "'" + string.Join("','", mailIds) + "'";
            MailMain mailMain = new MailMain
            {
                Viewed = read
            };
            var updateWheres = new ConditionExpressionCollection
                    {
                        MailMain_.MailMainId.TIn(ids),
                        MailMain_.Viewed.TNotEqual(read)
                    };
            int num = db.BatchUpdate(mailMain, updateWheres, MailMain_.Viewed);//更新邮件已读状态
            if (num > 0)
            {//更新对应邮件所在文件夹未读数,只更新已成功数
                SQLiteCommand command = new SQLiteCommand();
                string paramIds = "";
                int i = 0;
                foreach (var id in mailIds)
                {
                    paramIds += "@mailId" + i + ",";
                    command.Parameters.Add("@mailId" + i, DbType.String).Value = id;
                    i++;
                }
                string sql = "update MailFolder set UnreadCount=UnreadCount" + (read ? "-" : "+") + num + " where MailFolderId in(select MailFolder.MailFolderId from MailFolder left join MailMainFolder mmf on MailFolder.MailFolderId=mmf.MailFolderId left join MailMain m on m.MailMainId=mmf.MailMainId where mmf.MailMainId in({0}) and m.Viewed=@read);";
                sql += "update MailFolder set UnreadCount=0 where MailFolderId in(select MailFolder.MailFolderId from MailFolder left join MailMainFolder mmf on MailFolder.MailFolderId=mmf.MailFolderId left join MailMain m on m.MailMainId=mmf.MailMainId where mmf.MailMainId in({0}) and m.Viewed=@read) and MailFolder.UnreadCount<0;";
                command.CommandText = string.Format(sql, paramIds.Trim(','));
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@read", DbType.Boolean).Value = read;
                command.Parameters.Add("@mailNum", DbType.Boolean).Value = num;
                db.ExecuteNonQuery(command);
                command.Dispose();
            }
            return num;
        }

        /// <summary>
        /// 邮件以附件发送(生成邮件附件)
        /// </summary>
        /// <param name="mailId">要作为附件发送的邮件编号ID</param>
        /// <returns></returns>
        public MailMain ComposeMailAsAttach(LoginedUserInfo loginInfo, string mailId)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailMain mailMain = new MailMain { MailMainId = mailId };
                bool flag = db.Select(mailMain, MailMain_.ALL.Union(MailMain_.MailBody.ALL));
                if (!flag)
                {
                    return mailMain;
                }
                string relativePath = string.Format(@"\Eml\{0}\MailBox_{1}\", loginInfo.OCode, mailMain.MailBoxId);
                string acutalFileName = mailMain.MailMainId;//+ ".eml";
                string fullPath = UtilityHelper.MailAttachBaseDirectory + relativePath;
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                string fullFilePath = fullPath + acutalFileName;
                QueryExpression query;
                MailAttach mailAttach = new MailAttach();
                if (!File.Exists(fullFilePath))//如果当前邮件未生成物理文件则生成eml文件
                {
                    query = new QueryExpression { EntityType = typeof(MailAttach) };
                    query.Selects.Add(MailAttach_.ALL);
                    query.Wheres.Add(MailAttach_.MailMainId.TEqual(mailId));
                    MailAttachs attachs = new MailAttachs();
                    db.SelectCollection(attachs.Items, query);
                    FileInfo fi = new FileInfo(fullFilePath);
                    if (!fi.Directory.Exists) fi.Directory.Create();
                    var mailMsg = ConvertToMailMessage(mailMain, mailMain.MailBody, attachs.Items);
                    mailMsg.SaveMessage(fullFilePath);
                }
                else
                {
                    query = new QueryExpression { EntityType = typeof(MailAttach) };
                    query.Selects.Add(MailAttach_.ALL);
                    query.Wheres.Add(MailAttach_.CreateUID.TEqual(loginInfo.UserId));
                    query.Wheres.Add(MailAttach_.FilesPath.TEqual(fullFilePath));
                    query.Wheres.Add(MailAttach_.MailMainId.TEqual(string.Empty));
                    mailAttach = db.SelectOne<MailAttach>(query);
                }
                if (mailAttach == null || mailAttach.MailAttachId < 1)
                {
                    long size = 0;
                    using (FileStream fileStream = File.Open(fullFilePath, FileMode.Open))
                    {
                        size = fileStream.Length;
                    }
                    var name = mailMain.Subject.Length <= 220 ? mailMain.Subject : mailMain.Subject.Substring(0, 220);
                    name = name.IndexOf(".eml") < 0 ? (name + ".eml") : name;
                    mailAttach = new MailAttach
                    {
                        MailMainId = string.Empty,
                        ContentID = string.Empty,
                        FilesName = name,
                        FilesType = "eml",
                        FilesPath = fullFilePath,
                        FilesSize = UtilityHelper.ConvertFileSize(size),
                        ActualSize = (int)size,
                        CreateUID = loginInfo.UserId,
                        OCode = loginInfo.OCode,
                        CreateTime = DateTime.Now
                    };
                    db.Insert(mailAttach);
                }

                mailMain = new MailMain
                {
                    MailMainId = mailId,
                    MailAttachs = new List<MailAttach> { mailAttach },
                    MailBody = new MailBody(),
                    Subject = mailAttach.FilesName
                };
                return mailMain;
            }
        }

        #endregion 邮件

        #region 邮箱签名/模板

        /// <summary>
        /// 获取当前用户的邮箱签名列表，max = 999
        /// </summary>
        /// <param name="mailBoxId">邮箱ID编号,可空</param>
        /// <param name="signType">类型：1=邮箱签名,默认值；2=模板</param>
        /// <returns></returns>
        public IList<MailSignature> GetListSelfMailSign(LoginedUserInfo loginInfo, string mailBoxId = "", int signType = 1)
        {
            IList<MailSignature> signs = new List<MailSignature>();
            QueryExpression query = new QueryExpression { EntityType = typeof(MailSignature) };
            query.Selects.Add(MailSignature_.MailSignatureId);
            query.Selects.Add(MailSignature_.MailBoxId);
            query.Selects.Add(MailSignature_.IsDefault);
            query.Selects.Add(MailSignature_.SignatureName);
            query.Selects.Add(MailSignature_.SignatureContent);
            query.Wheres.Add(MailSignature_.OwnerUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailSignature_.OCode.TEqual(loginInfo.OCode));
            if (!string.IsNullOrWhiteSpace(mailBoxId))
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
                conditions.Add(MailSignature_.MailBoxId.TEqual(mailBoxId));
                conditions.Add(MailSignature_.MailBoxId.TEqual(string.Empty));
                query.Wheres.Add(conditions);
            }
            else
            {
                query.Wheres.Add(MailSignature_.MailBoxId.TEqual(string.Empty));
            }
            query.Wheres.Add(MailSignature_.SignType.TEqual(signType));
            query.OrderBys.Add(MailSignature_.IsDefault.PropertyName, OrderType.Desc);
            if (!string.IsNullOrWhiteSpace(mailBoxId))
                query.OrderBys.Add(MailSignature_.MailBoxId.PropertyName, OrderType.Desc);
            query.PageIndex = 1;
            query.PageSize = 999;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailSignatures entities = new MailSignatures();
                entities.TotalCount = db.SelectCollection(entities.Items, query);
                signs = entities.Items;
            }
            return signs;
        }
        /// <summary>
        /// 保存邮箱签名/模板，注意：参数MailSignDto.SignType必须赋值,否则保存后无法取值
        /// </summary>
        /// <param name="signDto"></param>
        /// <returns></returns>
        public ResponseResult SaveMailSign(LoginedUserInfo loginInfo, MailSignature signature)
        {
            if (signature == null || string.IsNullOrWhiteSpace(signature.SignatureName))
            {
                return UtilityHelper.ReturnParameterNull();
            }
            if (signature.MailBoxId == null) signature.MailBoxId = string.Empty;
            ResponseResult result = new ResponseResult();
            ConditionExpressionCollection conditions;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.And };
                conditions.Add(MailSignature_.SignType.TEqual(signature.SignType));
                conditions.Add(MailSignature_.SignatureName.TEqual(signature.SignatureName));
                if (signature.MailSignatureId > 0) { conditions.Add(MailSignature_.MailSignatureId.TNotEqual(signature.MailSignatureId)); }
                if (db.Exists<MailSignature>(conditions))
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = (signature.SignType == 1 ? "签名标题" : "模板名称") + "已存在";
                    return result;
                }
                if (signature.MailSignatureId < 1)//新增
                {
                    signature.CreateTime = DateTime.Now;
                    signature.CreateUID = loginInfo.UserId;
                    signature.OwnerUID = signature.CreateUID;
                    signature.OCode = loginInfo.OCode;
                    if (!signature.IsDefault.HasValue) signature.IsDefault = false;
                    db.Insert(signature);
                }
                else//修改
                {
                    MailSignature oldSign = new MailSignature { MailSignatureId = signature.MailSignatureId };
                    bool flag = db.Select(oldSign, MailSignature_.MailSignatureId, MailSignature_.SignatureName);
                    if (!flag)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = "邮箱" + (signature.SignType == 1 ? "签名" : "模板") + "不存在或已删除";
                        result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                        return result;
                    }
                    db.Update(signature, MailSignature_.MailBoxId, MailSignature_.SignatureName, MailSignature_.SignatureContent, MailSignature_.IsDefault);
                }
                if (signature.IsDefault == true)
                {
                    MailSignature updateSign = new MailSignature { IsDefault = false };
                    conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.And };
                    conditions.Add(MailSignature_.OwnerUID.TEqual(loginInfo.UserId));
                    conditions.Add(MailSignature_.OCode.TEqual(loginInfo.OCode));
                    conditions.Add(MailSignature_.MailBoxId.TEqual(signature.MailBoxId));
                    conditions.Add(MailSignature_.MailSignatureId.TNotEqual(signature.MailSignatureId));
                    db.BatchUpdate(updateSign, conditions, MailSignature_.IsDefault);
                }
                result.Data = signature;
            }
            return result;
        }
        /// <summary>
        /// 邮件签名删除
        /// </summary>
        /// <param name="signId"></param>
        /// <returns></returns>
        public ResponseResult RemoveMailSign(LoginedUserInfo loginInfo, int signId)
        {
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailSignature signature = new MailSignature { MailSignatureId = signId };
                bool flag = db.Select(signature);
                db.Delete(signature);
                result.Data = signId;
            }
            return result;
        }


        #endregion 邮箱签名/模板

        #region 邮箱联系人

        /// <summary>
        /// 获取当前用户的邮箱联系人列表
        /// </summary>
        /// <param name="query">查询条件,可不传</param>
        /// <returns></returns>
        public EntityList<MailContact> GetListSelfMailContact(LoginedUserInfo loginInfo, QueryParameter parameter)
        {
            EntityList<MailContact> contacts = new EntityList<MailContact>();
            QueryExpression query = QueryParameterConvertor.ToQueryExpression(parameter);
            query.EntityType = typeof(MailContact);
            if (query.Selects.Count < 1)
                query.Selects.Add(MailContact_.ALL);
            if (!string.IsNullOrWhiteSpace(parameter.KeyWords))
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
                conditions.Add(MailContact_.EMail.TLike(parameter.KeyWords));
                conditions.Add(MailContact_.ContactName.TLike(parameter.KeyWords));
                conditions.Add(MailContact_.ContactPinyin.TLike(parameter.KeyWords));
                query.Wheres.Add(conditions);
            }
            query.Wheres.Add(MailContact_.OwnerUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailContact_.OCode.TEqual(loginInfo.OCode));
            if (query.OrderBys.Count < 1)
            {
                query.OrderBys.Add(MailContact_.LastContactTime.PropertyName, OrderType.Desc);
                query.OrderBys.Add(MailContact_.ContactPinyin.PropertyName, OrderType.Asc);
            }
            if (query.PageIndex.HasValue && query.PageSize.HasValue)
                query.ReturnMatchedCount = true;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailContacts mailContacts = new MailContacts();
                mailContacts.TotalCount = db.SelectCollection(mailContacts.Items, query);
                contacts.TotalCount = mailContacts.TotalCount;
                contacts.Items = mailContacts.Items;
            }
            return contacts;
        }
        /// <summary>
        /// 获取邮件联系人
        /// </summary>
        /// <param name="contactId">邮件联系人ID</param>
        /// <returns></returns>
        public MailContact GetMailContact(int contactId)
        {
            MailContact mailContact = new MailContact();
            if (contactId > 0)
            {
                using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
                {
                    mailContact.MailContactId = contactId;
                    db.Select(mailContact);
                }
            }
            return mailContact;
        }
        /// <summary>
        /// 保存邮箱联系人
        /// </summary>
        /// <param name="mailContact">联系人数据传输对象</param>
        /// <returns></returns>
        public ResponseResult SaveMailContact(LoginedUserInfo loginInfo, MailContact mailContact)
        {
            if (mailContact == null || string.IsNullOrWhiteSpace(mailContact.EMail)) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                if (string.IsNullOrWhiteSpace(mailContact.ContactName))
                    mailContact.ContactName = mailContact.EMail.Split('@')[0];
                if (string.IsNullOrWhiteSpace(mailContact.ContactPinyin))
                    mailContact.ContactPinyin = PinYinHelper.GetPinyin(mailContact.ContactName);
                if (mailContact.MailContactId < 1)
                {
                    mailContact.CreateTime = DateTime.Now;
                    mailContact.CreateUID = loginInfo.UserId;
                    mailContact.OwnerUID = loginInfo.UserId;
                    mailContact.OCode = loginInfo.OCode;
                    db.Insert(mailContact);
                }
                else
                {
                    MailContact oldContact = new MailContact { MailContactId = mailContact.MailContactId };
                    bool flag = db.Select(oldContact);
                    if (!flag)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = ResponseErrorCodeEnum.ERROR_NOT_EXIST.GetDescription();
                        result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                        return result;
                    }
                    if (oldContact.OCode != loginInfo.OCode && oldContact.OwnerUID != loginInfo.UserId)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                        result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                        return result;
                    }
                    List<PropertyExpression> updates = new List<PropertyExpression> { MailContact_.Address, MailContact_.Area, MailContact_.CompanyName
                        , MailContact_.ContactName, MailContact_.ContactPinyin, MailContact_.CountryId, MailContact_.EMail, MailContact_.Facebook, MailContact_.Fax
                        , MailContact_.Memo, MailContact_.Mobile, MailContact_.Post, MailContact_.Postalcode, MailContact_.QQ, MailContact_.Skype, MailContact_.Tel
                        , MailContact_.Twitter };
                    db.Update(mailContact, updates);
                }
                result.Data = mailContact;
            }
            return result;
        }
        /// <summary>
        /// 删除邮件联系人，控制逻辑如下：
        /// 1、只能删除自己的
        /// </summary>
        /// <param name="ids">联系人编号ID数组</param>
        /// <returns></returns>
        public ResponseResult RemoveMailContact(LoginedUserInfo loginInfo, int[] ids)
        {
            if (ids.Length < 1) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    ConditionRelation = ConditionRelation.And
                };
                conditions.Add(MailContact_.MailContactId.TIn(string.Join(",", ids)));
                conditions.Add(MailContact_.OCode.TEqual(loginInfo.OCode));
                conditions.Add(MailContact_.OwnerUID.TEqual(loginInfo.UserId));
                db.BatchDelete<MailContact>(conditions);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ListItem> GetComposeMailContactsList(LoginedUserInfo loginInfo, string keyword)
        {
            List<ListItem> list = new List<ListItem>();
            QueryExpression query = new QueryExpression { EntityType = typeof(MailContact) };
            query.Selects.Add(MailContact_.EMail);
            query.Selects.Add(MailContact_.ContactName);
            ConditionExpressionCollection conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
            conditions.Add(MailContact_.ContactName.TLike(keyword));
            conditions.Add(MailContact_.EMail.TLike(keyword));
            query.Wheres.Add(conditions);
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailContacts mailContacts = new MailContacts();
                db.SelectCollection(mailContacts.Items, query);
                if (mailContacts.Items.Count > 0)
                {
                    mailContacts.Items.ForEach(item =>
                    {
                        if (!list.Any(x => x.Value == item.EMail))
                            list.Add(new ListItem { Text = UtilityHelper.GetMailAddressText(item.EMail, item.ContactName), Value = item.EMail });
                    });
                }
                query = new QueryExpression { EntityType = typeof(MailGroup) };
                query.Selects.Add(MailGroup_.ReceiveAddress);
                query.Selects.Add(MailGroup_.ReceiveName);
                conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
                conditions.Add(MailGroup_.ReceiveAddress.TLike(keyword));
                conditions.Add(MailGroup_.ReceiveName.TLike(keyword));
                query.Wheres.Add(conditions);
                MailGroups mailGroups = new MailGroups();
                db.SelectCollection(mailGroups.Items, query);
                if (mailGroups.Items.Count > 0)
                {
                    mailGroups.Items.ForEach(item =>
                    {
                        if (!list.Any(x => x.Value == item.ReceiveAddress))
                            list.Add(new ListItem { Text = UtilityHelper.GetMailAddressText(item.ReceiveAddress, item.ReceiveName), Value = item.ReceiveAddress });
                    });
                }
            }
            return list;
        }

        #endregion 邮箱联系人

        #region 黑名单/拒收

        /// <summary>
        /// 获取当前用户的邮箱拒收/黑名单列表
        /// </summary>
        /// <param name="query">查询条件,可不传</param>
        /// <returns></returns>
        public EntityList<MailJudge> GetListSelfMailJudge(LoginedUserInfo loginInfo, QueryParameter parameter)
        {
            EntityList<MailJudge> contacts = new EntityList<MailJudge>();
            QueryExpression query = QueryParameterConvertor.ToQueryExpression(parameter);
            query.EntityType = typeof(MailJudge);
            if (query.Selects.Count < 1)
                query.Selects.Add(MailJudge_.ALL);
            if (!query.Selects.Contains(new OutputExpression(MailJudge_.MailBox.MailAddress.PropertyName)))
                query.Selects.Add(MailJudge_.MailBox.MailAddress);
            if (!string.IsNullOrWhiteSpace(parameter.KeyWords))
            {
                query.Wheres.Add(MailJudge_.MailAddress.TLike(parameter.KeyWords));
            }
            query.Wheres.Add(MailJudge_.CreateUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailJudge_.OCode.TEqual(loginInfo.OCode));
            if (query.OrderBys.Count < 1)
            {
                query.OrderBys.Add(MailJudge_.CreateTime.PropertyName, OrderType.Desc);
            }
            if (query.PageIndex.HasValue && query.PageSize.HasValue)
                query.ReturnMatchedCount = true;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                contacts.TotalCount = db.SelectCollection(contacts.Items, query);
            }
            return contacts;
        }
        /// <summary>
        /// 保存邮箱拒收/黑名单，逻辑如下：
        /// 1、只能修改自己的
        /// </summary>
        /// <param name="entity">数据传输对象</param>
        /// <returns></returns>
        public ResponseResult SaveMailJudge(LoginedUserInfo loginInfo, MailJudge entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.MailAddress)) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                if (entity.MailJudgeId < 1)
                {
                    ConditionExpressionCollection conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.And };
                    conditions.Add(MailJudge_.OCode.TEqual(loginInfo.OCode));
                    conditions.Add(MailJudge_.CreateUID.TEqual(loginInfo.UserId));
                    conditions.Add(MailJudge_.MailAddress.TEqual(entity.MailAddress));
                    if (!string.IsNullOrWhiteSpace(entity.MailBoxId))
                        conditions.Add(MailJudge_.MailBoxId.TEqual(entity.MailBoxId));
                    if (!db.Exists<MailJudge>(conditions))
                    {
                        if (entity.MessageID == null) entity.MessageID = string.Empty;
                        if (entity.OperateType < 1) entity.OperateType = 2;
                        entity.CreateTime = DateTime.Now;
                        entity.CreateUID = loginInfo.UserId;
                        entity.OCode = loginInfo.OCode;
                        db.Insert(entity);
                    }
                }
                else
                {
                    MailJudge oldEntity = new MailJudge { MailJudgeId = entity.MailJudgeId };
                    bool flag = db.Select(oldEntity);
                    if (!flag)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = ResponseErrorCodeEnum.ERROR_NOT_EXIST.GetDescription();
                        result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                        return result;
                    }
                    if (oldEntity.OCode != loginInfo.OCode && oldEntity.CreateUID != loginInfo.UserId)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                        result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                        return result;
                    }
                    List<PropertyExpression> updates = new List<PropertyExpression> { MailJudge_.MailAddress, MailJudge_.Memo };
                    db.Update(entity, updates);
                }
                result.Data = entity;
            }

            return result;
        }
        /// <summary>
        /// 删除邮件黑名单/拒收，控制逻辑如下：
        /// 1、只能删除自己的
        /// </summary>
        /// <param name="ids">黑名单编号ID数组</param>
        /// <returns></returns>
        public ResponseResult RemoveMailJudge(LoginedUserInfo loginInfo, int[] ids)
        {
            if (ids.Length < 1) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            List<string> mailBoxs = new List<string>();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    ConditionRelation = ConditionRelation.And
                };
                conditions.Add(MailJudge_.MailJudgeId.TIn(string.Join(",", ids)));
                conditions.Add(MailJudge_.OCode.TEqual(loginInfo.OCode));
                conditions.Add(MailJudge_.CreateUID.TEqual(loginInfo.UserId));
                QueryExpression query = new QueryExpression() { EntityType = typeof(MailJudge) };
                query.Wheres.Add(conditions);
                query.Selects.Add(MailJudge_.MailBoxId);
                MailJudges judges = new MailJudges();
                db.SelectCollection(judges.Items, query);
                if (judges != null && judges.Items.Count > 0)
                    mailBoxs = judges.Items.Select(x => x.MailBoxId).ToList();
                db.BatchDelete<MailJudge>(conditions);
                result.Data = mailBoxs;
            }
            return result;
        }

        #endregion 黑名单/拒收

        #region 邮件标签

        /// <summary>
        /// 获取当前用户的邮件标签列表
        /// </summary>
        /// <param name="query">查询条件,可不传</param>
        /// <returns></returns>
        public EntityList<MailLabel> GetListSelfMailLabel(LoginedUserInfo loginInfo, QueryParameter parameter)
        {
            EntityList<MailLabel> contacts = new EntityList<MailLabel>();
            QueryExpression query = QueryParameterConvertor.ToQueryExpression(parameter);
            query.EntityType = typeof(MailLabel);
            if (query.Selects.Count < 1)
                query.Selects.Add(MailLabel_.ALL);
            if (!string.IsNullOrWhiteSpace(parameter.KeyWords))
            {
                query.Wheres.Add(MailLabel_.MailLabelName.TLike(parameter.KeyWords));
            }
            query.Wheres.Add(MailLabel_.CreateUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailLabel_.OCode.TEqual(loginInfo.OCode));
            if (query.OrderBys.Count < 1)
            {
                query.OrderBys.Add(MailLabel_.CreateTime.PropertyName, OrderType.Asc);
            }
            if (query.PageIndex.HasValue && query.PageSize.HasValue)
                query.ReturnMatchedCount = true;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                contacts.TotalCount = db.SelectCollection(contacts.Items, query);
            }
            return contacts;
        }
        /// <summary>
        /// 保存邮件标签，逻辑如下：
        /// 1、不能保存相同邮件名称和颜色的标签
        /// 2、只能修改自己的邮件标签
        /// </summary>
        /// <param name="entity">邮件标签数据传输对象</param>
        /// <returns></returns>
        public ResponseResult SaveMailLabel(LoginedUserInfo loginInfo, MailLabel entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.MailLabelName)) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection() { ConditionRelation = ConditionRelation.And };
                conditions.And(MailLabel_.OCode.TEqual(loginInfo.OCode));
                conditions.And(MailLabel_.CreateUID.TEqual(loginInfo.UserId));
                conditions.And(MailLabel_.MailLabelName.TEqual(entity.MailLabelName));
                conditions.And(MailLabel_.Color.TEqual(entity.Color));
                if (!string.IsNullOrWhiteSpace(entity.MailLabelId))
                    conditions.And(MailLabel_.MailLabelId.TNotEqual(entity.MailLabelId));
                if (db.Exists<MailLabel>(conditions))
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "已存在相同名称和颜色的标签";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(entity.MailLabelId))
                {
                    conditions = new ConditionExpressionCollection() { ConditionRelation = ConditionRelation.And };
                    conditions.And(MailLabel_.OCode.TEqual(loginInfo.OCode));
                    conditions.And(MailLabel_.CreateUID.TEqual(loginInfo.UserId));
                    int num = db.SelectCount<MailLabel>(conditions);
                    if (num < int.MaxValue) num = num + 1;
                    entity.MailLabelId = UtilityHelper.GetGuid();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateUID = loginInfo.UserId;
                    entity.OCode = loginInfo.OCode;
                    db.Insert(entity);
                    //增加邮件标签后同步增加相应的文件夹记录
                    MailFolder mailFolder = new MailFolder
                    {
                        CreateTime = entity.CreateTime,
                        CreateUID = entity.CreateUID,
                        Depth = 1,
                        FolderName = entity.MailLabelName,
                        MailBoxId = string.Empty,
                        MailFolderId = UtilityHelper.GetGuid(),
                        MailType = (int)MailFolderEnum.Customize,
                        OCode = entity.OCode,
                        OwnerUID = entity.CreateUID,
                        ParentId = string.Empty,
                        Sorting = num,
                        SourceId = entity.MailLabelId,
                        SourceTable = MailFolderSourceTableEnum.MailLabel.ToString(),
                        UnreadCount = 0
                    };
                    db.Insert(mailFolder);
                }
                else
                {
                    MailLabel oldEntity = new MailLabel { MailLabelId = entity.MailLabelId };
                    bool flag = db.Select(oldEntity);
                    if (!flag)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = ResponseErrorCodeEnum.ERROR_NOT_EXIST.GetDescription();
                        result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                        return result;
                    }
                    if (oldEntity.OCode != loginInfo.OCode && oldEntity.CreateUID != loginInfo.UserId)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                        result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                        return result;
                    }
                    List<PropertyExpression> updates = new List<PropertyExpression> { MailLabel_.MailLabelName, MailLabel_.Color, MailLabel_.Memo };
                    db.Update(entity, updates);
                    //标签名称有修改，需同步修改邮箱文件夹MailFolder_.FolderName和过滤器中的 Text
                    if (oldEntity.MailLabelName != entity.MailLabelName)
                    {
                        conditions = new ConditionExpressionCollection() { ConditionRelation = ConditionRelation.And };
                        conditions.And(MailFolder_.OCode.TEqual(entity.OCode));
                        conditions.And(MailFolder_.OwnerUID.TEqual(entity.CreateUID));
                        conditions.And(MailFolder_.SourceId.TEqual(entity.MailLabelId));
                        conditions.And(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailLabel.ToString()));
                        MailFolder mailFolder = new MailFolder { FolderName = entity.MailLabelName };
                        db.BatchUpdate(mailFolder, conditions, MailFolder_.FolderName);
                    }
                    if (oldEntity.MailLabelName != entity.MailLabelName || oldEntity.Color != entity.Color)
                    {
                        _mailFilterServices.UpdateTagToFilter(loginInfo, db, entity);
                    }
                }
                result.Data = entity;
            }
            return result;
        }
        /// <summary>
        /// 删除邮件标签，控制逻辑如下：
        /// 1、判断是否已被过滤器使用,如已使用不可删除
        /// 2、只能删除自己的
        /// </summary>
        /// <param name="ids">邮件标签编号ID数组</param>
        /// <returns></returns>
        public ResponseResult RemoveMailLabel(LoginedUserInfo loginInfo, string[] ids)
        {
            if (ids.Length < 1) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    string inIds = "'" + string.Join("','", ids) + "'";
                    //判断过滤器是否使用该标签,如使用则不能删除,需先修改/删除对应过滤器
                    if (_mailFilterServices.CheckMailLabelUsedInFilter(loginInfo, db, ids))
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.Message = "标签已在过滤器中使用,不能删除";
                        return result;
                    }
                    db.BeginTransaction();
                    ConditionExpressionCollection conditions = new ConditionExpressionCollection
                    {
                        ConditionRelation = ConditionRelation.And
                    };
                    conditions.Add(MailLabel_.MailLabelId.TIn(inIds));
                    conditions.Add(MailLabel_.OCode.TEqual(loginInfo.OCode));
                    conditions.Add(MailLabel_.CreateUID.TEqual(loginInfo.UserId));
                    db.BatchDelete<MailLabel>(conditions);
                    //删除MailFolder和MailMailFolder记录
                    MailFolders mailFolders = new MailFolders();
                    QueryExpression query = new QueryExpression() { EntityType = typeof(MailFolder) };
                    query.Selects.Add(MailFolder_.MailFolderId);
                    query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailLabel.ToString()));
                    query.Wheres.Add(MailFolder_.SourceId.TIn(inIds));
                    query.Wheres.Add(MailFolder_.OCode.TEqual(loginInfo.OCode));
                    query.Wheres.Add(MailFolder_.CreateUID.TEqual(loginInfo.UserId));
                    query.PageSize = int.MaxValue;
                    db.SelectCollection(mailFolders.Items, query);
                    if (mailFolders != null && mailFolders.Items.Count > 0)
                    {
                        var folderIds = "'" + string.Join("','", mailFolders.Items.Select(x => x.MailFolderId)) + "'";
                        #region 删除邮件主表中的标签信息
                        MailMainFolders mainFolders = new MailMainFolders();
                        query = new QueryExpression() { EntityType = typeof(MailMainFolder) };
                        query.Selects.Add(MailMainFolder_.MailMain.MailMainId);
                        query.Selects.Add(MailMainFolder_.MailMain.LabelInfo);
                        query.Wheres.Add(MailMainFolder_.MailFolderId.TIn(folderIds));
                        db.SelectCollection(mainFolders.Items, query);
                        if (mainFolders != null && mainFolders.Items.Count > 0)
                        {
                            mainFolders.Items.ForEach(item =>
                            {
                                var labelInfo = JsonSerializationHelper.JsonDeserialize<List<MailLabel>>(item.MailMain.LabelInfo);
                                if (labelInfo != null)
                                {
                                    foreach (string labelId in ids)
                                    {
                                        var index = labelInfo.FindIndex(x => x.MailLabelId == labelId);
                                        if (index > -1)
                                        {
                                            labelInfo.RemoveAt(index);
                                        }
                                    }
                                }
                                if (labelInfo != null && labelInfo.Count > 0)
                                    item.MailMain.LabelInfo = JsonSerializationHelper.JsonSerialize(labelInfo);
                                else
                                    item.MailMain.LabelInfo = string.Empty;
                                db.Update(item.MailMain, MailMain_.LabelInfo);
                            });
                            #endregion 删除邮件主表中的标签信息
                        }
                        db.BatchDelete<MailMainFolder>(MailMainFolder_.MailFolderId.TIn(folderIds));
                        db.BatchDelete<MailFolder>(MailFolder_.MailFolderId.TIn(folderIds));
                    }
                    db.Commit();
                    result.Data = ids;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    result.Message = ex.Message;
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                }
            }
            return result;
        }
        /// <summary>
        /// 邮件打标签
        /// </summary>
        /// <param name="db">数据库连接池</param>
        /// <param name="ocode">组织编号(代码)</param>
        /// <param name="uid">用户编号</param>
        /// <param name="mailMainIds">邮件ID数组</param>
        /// <param name="labelId">标签ID</param>
        public static void MailAddLabel(IDbSession db, string ocode, string uid, string[] mailMainIds, string labelId)
        {
            if (mailMainIds.Length < 1) return;
            MailLabel mailLabel = new MailLabel() { MailLabelId = labelId };
            bool flag = db.Select(mailLabel);
            if (!flag || mailLabel == null || mailLabel.OCode != ocode) return;
            var query = new QueryExpression { EntityType = typeof(MailFolder) };
            query.Selects.Add(MailFolder_.MailFolderId);
            query.Wheres.Add(MailFolder_.SourceId.TEqual(mailLabel.MailLabelId.ToString()));
            query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailLabel.ToString()));
            query.Wheres.Add(MailFolder_.CreateUID.TEqual(uid));
            query.Wheres.Add(MailFolder_.OCode.TEqual(ocode));
            MailFolder mailFolder = db.SelectOne<MailFolder>(query);
            if (mailFolder == null) return;

            foreach (var mailId in mailMainIds)
            {
                MailMain mailMain = new MailMain { MailMainId = mailId };
                db.Select(mailMain, MailMain_.MailMainId, MailMain_.LabelInfo);
                List<MailLabel> labelInfo = JsonSerializationHelper.JsonDeserialize<List<MailLabel>>(mailMain.LabelInfo);
                if (labelInfo == null) labelInfo = new List<MailLabel>();
                var index = labelInfo.FindIndex(x => x.MailLabelId == mailLabel.MailLabelId);
                var labelDto = mailLabel;
                labelDto.Memo = null;
                if (index < 0)//当前标签未添加(不存在)
                {
                    labelInfo.Add(labelDto);
                }
                else
                {
                    labelInfo[index] = labelDto;
                }
                mailMain.LabelInfo = JsonSerializationHelper.JsonSerialize(labelInfo);
                db.Update(mailMain, MailMain_.LabelInfo);
                MailMainFolder mainFolder = new MailMainFolder
                {
                    MailFolderId = mailFolder.MailFolderId,
                    MailMainId = mailId
                };
                if (!db.Exists(mainFolder))
                    db.Insert(mainFolder);
            }
        }
        /// <summary>
        /// 邮件打标签
        /// </summary>
        /// <param name="mailIds">邮件ID数组</param>
        /// <param name="labelId">标签ID</param>
        /// <returns></returns>
        public ResponseResult MailAddLabel(LoginedUserInfo loginInfo, string[] mailIds, string labelId)
        {
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailAddLabel(db, loginInfo.OCode, loginInfo.UserId, mailIds, labelId);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 删除邮件标签
        /// </summary>
        /// <param name="db">数据库连接池</param>
        /// <param name="ocode">组织编号(代码)</param>
        /// <param name="uid">用户编号</param>
        /// <param name="mailMainIds">邮件ID数组</param>
        /// <param name="labelId">标签ID</param>
        public static void MailRemoveLabel(IDbSession db, string ocode, string uid, string[] mailMainIds, string labelId)
        {
            if (mailMainIds.Length < 1) return;
            string mailIds = "'" + string.Join("','", mailMainIds) + "'";
            QueryExpression query;
            query = new QueryExpression { EntityType = typeof(MailMainFolder) };
            query.Selects.Add(MailMainFolder_.MailFolderId);
            query.Selects.Add(MailMainFolder_.MailMain.MailMainId);
            query.Selects.Add(MailMainFolder_.MailMain.LabelInfo);
            query.Wheres.Add(MailMainFolder_.MailMainId.TIn(mailIds));
            if (!string.IsNullOrWhiteSpace(labelId))
            {
                query.Wheres.Add(MailMainFolder_.MailFolder.SourceId.TEqual(labelId));
            }
            query.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(MailFolderSourceTableEnum.MailLabel.ToString()));
            query.Wheres.Add(MailMainFolder_.MailFolder.CreateUID.TEqual(uid));
            query.Wheres.Add(MailMainFolder_.MailFolder.OCode.TEqual(ocode));
            MailMainFolders mailMainFolders = new MailMainFolders();
            db.SelectCollection(mailMainFolders.Items, query);
            if (mailMainFolders == null) mailMainFolders = new MailMainFolders();
            if (string.IsNullOrWhiteSpace(labelId))//清除邮件标签
            {
                mailMainFolders.Items.ForEach(item =>
                {
                    MailMainFolder mainFolder = new MailMainFolder
                    {
                        MailFolderId = item.MailFolderId,
                        MailMainId = item.MailMain.MailMainId
                    };
                    db.Delete(mainFolder);
                });
                ConditionExpression conditions = new ConditionExpression(MailMain_.MailMainId.PropertyName, ConditionOperator.In, mailIds);
                MailMain mailMain = new MailMain { LabelInfo = string.Empty };
                db.BatchUpdate(mailMain, conditions, MailMain_.LabelInfo);
            }
            else//删除邮件某一标签
            {
                mailMainFolders.Items.ForEach(item =>
                {
                    var labelInfo = JsonSerializationHelper.JsonDeserialize<List<MailLabel>>(item.MailMain.LabelInfo);
                    if (labelInfo != null)
                    {
                        var index = labelInfo.FindIndex(x => x.MailLabelId == labelId);
                        if (index > -1)
                        {
                            labelInfo.RemoveAt(index);
                        }
                    }
                    if (labelInfo != null && labelInfo.Count > 0)
                        item.MailMain.LabelInfo = JsonSerializationHelper.JsonSerialize(labelInfo);
                    else
                        item.MailMain.LabelInfo = string.Empty;
                    db.Update(item.MailMain, MailMain_.LabelInfo);
                    MailMainFolder mainFolder = new MailMainFolder
                    {
                        MailFolderId = item.MailFolderId,
                        MailMainId = item.MailMain.MailMainId
                    };
                    db.Delete(mainFolder);
                });
            }
        }
        /// <summary>
        /// 删除邮件标签
        /// </summary>
        /// <param name="mailIds">邮件ID数组</param>
        /// <param name="labelId">标签ID</param>
        /// <returns></returns>
        public ResponseResult MailRemoveLabel(LoginedUserInfo loginInfo, string[] mailIds, string labelId)
        {
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailRemoveLabel(db, loginInfo.OCode, loginInfo.UserId, mailIds, labelId);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        #endregion 邮件标签

        #region 客户联系人
        /// <summary>
        /// 当前登录用户的客户联系人列表(主要用于客户邮件归档)
        /// </summary>
        public static List<CustomerContacts> CustomerContactsList = new List<CustomerContacts>();
        /// <summary>
        /// 获取客户联系人列表(当前企业当前用户下所有)
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public void SyncCustomerContacts(LoginedUserInfo loginInfo)
        {
            try
            {
                string url = "https://email.msncp.com/Mail/GetCustomerList";
                var responseResult = HttpHelper.HttpGet(url, string.Format("uid={0}&ocode={1}&sign={2}", loginInfo.UserId, loginInfo.OCode
                    , EncryptHelper.MD5(loginInfo.OCode + loginInfo.UserId)));
                if (!string.IsNullOrWhiteSpace(responseResult))
                {
                    var result = JsonSerializationHelper.JsonDeserialize<ResponseResult>(responseResult);
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString() && result.Data != null)
                    {
                        RemoveCustomerContacts(loginInfo, new string[] { }, true);
                        var contactList = JsonSerializationHelper.JsonDeserialize<List<CustomerContacts>>(result.Data.ToString());//result.Data as List<CustomerContacts>;
                        if (contactList != null)
                        {
                            foreach (var contacts in contactList)
                            {
                                SaveCustomerContacts(loginInfo, contacts);
                            }
                            CustomerContactsList = contactList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex);
            }

        }

        /// <summary>
        /// 获取当前用户的客户联系人列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<CustomerContacts> GetCustomerContactsList(LoginedUserInfo loginInfo)
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                GetCustomerContactsList(db, loginInfo.UserId, loginInfo.OCode);
                return CustomerContactsList;
            }
        }
        /// <summary>
        /// 获取客户联系人列表(当前企业当前用户下所有)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userId"></param>
        /// <param name="ocode"></param>
        /// <param name="reload">是否重新从数据库读取；true：读取</param>
        /// <returns></returns>
        public static void GetCustomerContactsList(IDbSession db, string userId, string ocode, bool reload = false)
        {
            if (CustomerContactsList.Count < 1 || reload)
            {
                List<CustomerContacts> contactsList = new List<CustomerContacts>();
                QueryExpression query = new QueryExpression
                {
                    EntityType = typeof(CustomerContacts)
                };
                query.Selects.Add(CustomerContacts_.ContactsId);
                query.Selects.Add(CustomerContacts_.ClientId);
                query.Selects.Add(CustomerContacts_.ClientName);
                query.Selects.Add(CustomerContacts_.ClientNo);
                query.Selects.Add(CustomerContacts_.OwnerUID);
                query.Selects.Add(CustomerContacts_.OCode);
                query.Selects.Add(CustomerContacts_.Linkman);
                query.Selects.Add(CustomerContacts_.Email);
                query.Wheres.Add(CustomerContacts_.OwnerUID.TEqual(userId));
                query.Wheres.Add(CustomerContacts_.OCode.TEqual(ocode));
                query.PageSize = int.MaxValue;
                CustomerContactss mailContacts = new CustomerContactss();
                mailContacts.TotalCount = db.SelectCollection(mailContacts.Items, query);
                CustomerContactsList = mailContacts.Items;
            }
        }
        public ResponseResult SaveCustomerContacts(LoginedUserInfo loginInfo, CustomerContacts contacts)
        {
            if (contacts == null) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrWhiteSpace(contacts.ClientNo) || string.IsNullOrWhiteSpace(contacts.ClientId))
            {
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.Message = "客户代码为空";
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                if (string.IsNullOrWhiteSpace(contacts.ContactsId))
                {
                    contacts.ContactsId = UtilityHelper.GetGuid();
                    contacts.CreateTime = DateTime.Now;
                    contacts.CreateUID = loginInfo.UserId;
                    contacts.OwnerUID = loginInfo.UserId;
                    contacts.OCode = loginInfo.OCode;
                    db.Insert(contacts);
                }
                else
                {
                    contacts.CreateTime = DateTime.Now;
                    List<PropertyExpression> fields = new List<PropertyExpression> {
                        CustomerContacts_.ClientId, CustomerContacts_.ClientName, CustomerContacts_.ClientNo, CustomerContacts_.Linkman
                        , CustomerContacts_.Email,CustomerContacts_.CreateTime };
                    db.Update(contacts, fields);
                }
                result.Data = contacts;
            }
            return result;
        }
        /// <summary>
        /// 删除客户联系人，控制逻辑如下：
        /// 1、只能删除自己的
        /// </summary>
        /// <param name="ids">联系人编号ID数组</param>
        /// <returns></returns>
        public ResponseResult RemoveCustomerContacts(LoginedUserInfo loginInfo, string[] ids, bool removeAll = false)
        {
            if (ids.Length < 1 && !removeAll) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    ConditionRelation = ConditionRelation.And
                };
                if (ids.Length > 0)
                    conditions.Add(CustomerContacts_.ContactsId.TIn(string.Join(",", ids)));
                conditions.Add(CustomerContacts_.OCode.TEqual(loginInfo.OCode));
                conditions.Add(CustomerContacts_.OwnerUID.TEqual(loginInfo.UserId));
                db.BatchDelete<CustomerContacts>(conditions);
            }
            return result;
        }
        /// <summary>
        /// 客户邮件归档
        /// </summary>
        /// <param name="loginInfo">登录用户信息</param>
        /// <param name="customerContactsList">需为同一客户的联系人信息</param>
        /// <returns></returns>
        public ResponseResult ArchiveCustomerMail(LoginedUserInfo loginInfo, List<CustomerContacts> customerContactsList)
        {
            ResponseResult result = new ResponseResult();
            QueryExpression query = new QueryExpression { EntityType = typeof(MailMain) };
            query.Selects.Add(MailMain_.MailMainId);
            query.PageIndex = 1;
            query.PageSize = 9999;
            query.Wheres.Add(MailMain_.OCode.TEqual(loginInfo.OCode));
            query.Wheres.Add(MailMain_.OwnerUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailMain_.Deleted.TEqual(false));
            ConditionExpressionCollection conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.Or };
            foreach (var contacts in customerContactsList)
            {
                conditions.Add(MailMain_.Sender.TLike(contacts.Email));
                conditions.Add(MailMain_.Receiver.TLike(contacts.Email));
                conditions.Add(MailMain_.Cc.TLike(contacts.Email));
                conditions.Add(MailMain_.Bcc.TLike(contacts.Email));
            }
            if (conditions.Count > 0)
                query.Wheres.Add(conditions);
            else
            {
                result.Message = "不存在未归档客户邮件";
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                var contact = customerContactsList[0];
                MailMains mails = new MailMains();
                db.SelectCollection(mails.Items, query);
                foreach (var mail in mails.Items)
                {
                    MailMainBind(db, mail.MailMainId, MailFolderSourceTableEnum.Customer.ToString(), contact.ClientId, "[" + contact.ClientNo + "]" + contact.ClientName);
                }
                result.Message = "客户邮件归档完成";
            }
            return result;
        }
        #endregion 客户联系人

        #region 邮件服务相关

        /// <summary>
        /// 更新邮箱的邮件数及邮件大小
        /// </summary>
        /// <param name="db">数据库连接池,只用于邮件服务调用</param>
        /// <param name="ocode">公司编号</param>
        /// <param name="uid">操作员(员工)ID编号</param>
        /// <param name="mailBoxId">邮箱ID编号</param>
        /// <param name="mailFolderId">邮箱文件夹ID编号</param>
        /// <param name="inBox">是否收件箱,用于累加未读邮件数</param>
        /// <param name="mailSize">需累加的邮件大小</param>
        /// <param name="mailCount">需累加的邮件数，默认值为+1</param>
        /// <param name="mailLastIndex">邮箱文件夹最新收取(同步)的序号,如未传值则为+1，否则直接赋该值</param>
        public static void UpdateMailCountAndSize(IDbSession db, string ocode, string uid, string mailBoxId, string mailFolderId, bool inBox = true, long mailSize = 0, int mailCount = 1, int? mailLastIndex = null)
        {
            MailBox mailBox = new MailBox { MailBoxId = mailBoxId };
            db.Select(mailBox, MailBox_.MailCount, MailBox_.MailSize, MailBox_.MailBoxId);
            MailFolder mailFolder = new MailFolder { MailFolderId = mailFolderId };
            var folderFields = new List<PropertyExpression> { MailFolder_.MailCount, MailFolder_.MailFolderId };
            if (inBox == true)
            {
                folderFields.Add(MailFolder_.UnreadCount);
            }
            db.Select(mailFolder, folderFields);
            mailFolder.MailCount += mailCount;
            if (inBox == true)
            {
                mailFolder.UnreadCount += mailCount;
            }
            mailBox.MailCount += mailCount;
            mailBox.MailSize += mailSize;
            db.Update(mailBox, MailBox_.MailCount, MailBox_.MailSize);
            db.Update(mailFolder, folderFields);
        }
        /// <summary>
        /// 邮件归档/邮件移动文件夹
        /// </summary>
        /// <param name="db">数据库连接池</param>
        /// <param name="fromMailFolderId">如果为空，内部会自动找出一个fromMailFolderId</param>
        /// <param name="toMailFolderId">目标邮件文件夹ID编号；如为空则不归入任何文件夹，类似于彻底删除</param>
        /// <param name="mailMainId">邮件ID编号</param>
        public static void MoveMailMainToFolder(IDbSession db, string fromMailFolderId, string toMailFolderId, string mailMainId)
        {
            if (fromMailFolderId == toMailFolderId) return;
            if (string.IsNullOrWhiteSpace(fromMailFolderId))
            {
                //查找出旧的mailFolderId 
                MailFolder toMailFolder = new MailFolder
                {
                    MailFolderId = toMailFolderId,
                };
                db.Select(toMailFolder, MailFolder_.SourceTable);

                QueryExpression q = new QueryExpression() { EntityType = typeof(MailMainFolder) };
                q.Selects.Add(MailMainFolder_.MailFolderId);
                q.Selects.Add(MailMainFolder_.MailFolder.MailFolderId);
                q.Wheres.Add(MailMainFolder_.MailFolder.SourceTable.TEqual(toMailFolder.SourceTable));
                q.Wheres.Add(MailMainFolder_.MailMainId.TEqual(mailMainId));
                var folder = db.SelectOne<MailMainFolder>(q);
                if (folder != null && folder.MailFolder != null)
                    fromMailFolderId = folder.MailFolder.MailFolderId;
            }
            //从旧文件夹中删除
            MailMainFolder from = new MailMainFolder
            {
                MailMainId = mailMainId,
                MailFolderId = fromMailFolderId
            };
            db.Delete(from);

            //移动到新文件夹,
            if (!string.IsNullOrWhiteSpace(toMailFolderId))
            {
                MailMainFolder to = new MailMainFolder
                {
                    MailMainId = mailMainId,
                    MailFolderId = toMailFolderId
                };
                if (!db.Exists(to))
                {
                    db.Insert(to);
                }
            }
        }

        /// <summary>
        /// 收取邮件并输出邮件数据存储对象
        /// </summary>
        /// <param name="ocode">公司编号</param>
        /// <param name="mailMessage">MailBee邮件对象</param>
        /// <param name="messageID">邮件唯一ID</param>
        /// <param name="mailBoxId">邮箱ID编号</param>
        /// <param name="userId">用户ID编号</param>
        /// <param name="attachFolder">邮件附件存储的磁盘根目录(一般为站点的根目录)</param>
        /// <returns></returns>
        public static MailMain CreateMailMain(string ocode, MailMessage mailMessage, string messageID, string mailBoxId, string userId, string attachFolder, string emailAddress)
        {
            MailMain mailMain = new MailMain();
            mailMain.MailBody = new MailBody();
            mailMain.MailAttachs = new List<MailAttach>();

            #region MailMain

            mailMain.IsSync = false;
            mailMain.IsArchived = false;
            mailMain.CreateTime = DateTime.Now;
            mailMain.Deleted = false;
            mailMain.IsComplete = true;
            mailMain.Viewed = false;
            mailMain.IsTimer = false;
            mailMain.IsMemo = false;
            mailMain.IsGroup = false;
            mailMain.IsTop = false;
            mailMain.ReplyCount = 0;
            mailMain.ForwardCount = 0;
            mailMain.FromId = string.Empty;
            mailMain.MailBoxId = mailBoxId;
            mailMain.OwnerUID = userId;
            mailMain.CreateUID = userId;
            mailMain.OCode = ocode;
            mailMain.LabelInfo = string.Empty;
            mailMain.Subject = string.IsNullOrWhiteSpace(mailMessage.Subject) ? "" : mailMessage.Subject.Trim();
            if (mailMain.Subject.Length > 512) mailMain.Subject = mailMain.Subject.Substring(0, 512);
            mailMain.Sender = GetDisplayName(mailMessage.From);
            mailMain.MailSize = mailMessage.SizeOnServer;
            mailMain.MailState = MailStateEnum.SEND_SUCCESS.ToString();
            mailMain.MailType = mailMain.Sender.IndexOf(emailAddress) < 0 ? 1 : 2;//发件人不为自己邮箱则为收件
            if (mailMain.MailType == 2 && !mailMain.Viewed)
                mailMain.Viewed = true;
            //收件人
            StringBuilder emailAddresss = new StringBuilder();
            List<MailGroup> mailGroups = new List<MailGroup>();
            void SetEmailAddress(EmailAddressCollection addressCollection, MailGroupReceiveTypeEnum receiveType)
            {
                emailAddresss = new StringBuilder();
                if (addressCollection.Count > 0)
                {
                    for (int i = 0; i < addressCollection.Count; i++)
                    {
                        var address = addressCollection[i];
                        mailGroups.Add(new MailGroup
                        {
                            ReceiveAddress = address.Email,
                            ReceiveName = address.DisplayName,
                            ReceiveTypeCode = (int)receiveType,
                            Sorting = i + 1
                        });
                        if (string.IsNullOrWhiteSpace(address.DisplayName)) address.DisplayName = address.Email;
                        emailAddresss.Append(GetDisplayName(address) + ";");
                    }
                }
            }
            SetEmailAddress(mailMessage.To, MailGroupReceiveTypeEnum.Receiver);
            mailMain.Receiver = emailAddresss.ToString().Trim(';');
            //cc抄送
            SetEmailAddress(mailMessage.Cc, MailGroupReceiveTypeEnum.Cc);
            mailMain.Cc = emailAddresss.ToString().Trim(';');
            //bcc密送
            SetEmailAddress(mailMessage.Bcc, MailGroupReceiveTypeEnum.Bcc);
            mailMain.Bcc = emailAddresss.ToString().Trim(';');
            mailMain.MailGroups = mailGroups;
            mailMain.Reply = mailMessage.ReplyTo.ToString();
            //如果Reply为空则以发件人代替
            if (string.IsNullOrWhiteSpace(mailMain.Reply))
            {
                mailMain.Reply = mailMessage.From.Email;
            }
            mailMain.Importance = (int)mailMessage.Importance;
            mailMain.MailTime = mailMessage.DateReceived;
            mailMain.MessageId = messageID;//不能用 mailMessage.MessageID，mailMessage.MessageID不唯一，取自于邮件头;

            #endregion MailMain

            #region MailBody
            mailMain.MailBody.BodyText = mailMessage.BodyPlainText;
            mailMain.MailBody.BodyHtml = mailMessage.BodyHtmlText;
            if (!string.IsNullOrWhiteSpace(mailMain.MailBody.BodyHtml))
            {
                mailMain.MailBody.BodyHtml = Regex.Replace(mailMain.MailBody.BodyHtml, @"\<base ", @"\<base2 ", RegexOptions.IgnoreCase);
                if (mailMain.MailBody.BodyText == null)
                    mailMain.MailBody.BodyText = UtilityHelper.Nohtml(mailMain.MailBody.BodyHtml);
            }
            else
            {
                mailMain.MailBody.BodyHtml = mailMain.MailBody.BodyText + "";
            }

            #endregion MailBody

            #region MailAttachs

            string path = string.Format(@"\{0}\MailBox_{1}\{2}", ocode, mailBoxId, DateTime.Now.ToString("yyyyMMdd"));

            foreach (Attachment att in mailMessage.Attachments)
            {
                string fullPath = attachFolder + path;
                if (!Directory.Exists(fullPath))//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(fullPath);
                }
                MailAttach mailAttach = new MailAttach
                {
                    FilesName = att.Filename
                };
                if (mailAttach.FilesName.Length > 220) mailAttach.FilesName = mailAttach.FilesName.Substring(mailAttach.FilesName.Length - 220, 220);
                mailAttach.FilesType = att.Filename.LastIndexOf('.') > -1 ? att.Filename.Substring(att.Filename.LastIndexOf('.') + 1) : "";
                //本地客户端附件文件不显示后缀名，简单加密
                string actualFileName = Guid.NewGuid().ToString("N");// + (string.IsNullOrEmpty(mailAttach.FilesType) ? "" : "." + mailAttach.FilesType);
                mailAttach.FilesPath = fullPath + @"\" + actualFileName;
                mailAttach.FilesSize = UtilityHelper.ConvertFileSize(att.Size);
                mailAttach.ActualSize = att.Size;
                mailAttach.ContentID = att.ContentID + "";
                mailAttach.CreateUID = mailMain.CreateUID;
                mailAttach.OCode = ocode;
                if (!string.IsNullOrEmpty(mailAttach.ContentID) && UtilityHelper.IsImageType(mailAttach.FilesType) && mailMain.MailBody.BodyHtml.IndexOf("cid:" + mailAttach.ContentID, StringComparison.Ordinal) != -1)
                {
                    mailMain.MailBody.BodyHtml = Regex.Replace(mailMain.MailBody.BodyHtml, Regex.Escape("cid:" + mailAttach.ContentID), mailAttach.FilesPath.Replace(@"\", @"/"), RegexOptions.IgnoreCase);
                }
                else
                {
                    mailMain.AttachCount += 1;
                }
                mailMain.MailAttachs.Add(mailAttach);
                //save to disk 
                att.Save(mailAttach.FilesPath, true);
            }

            #endregion MailAttachs

            return mailMain;
        }

        /// <summary>
        /// 收取邮件并写入数据库
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailMain"></param>
        /// <param name="mailBody"></param>
        /// <param name="mailAttachs"></param>
        /// <returns></returns>
        public static MailMain InsertMailFromReceive(IDbSession db, MailMain mailMain, MailBody mailBody, List<MailAttach> mailAttachs, string defaultFolderId)
        {
            mailMain.MailMainId = UtilityHelper.GetGuid();
            db.Insert(mailMain);
            mailBody.MailMainId = mailMain.MailMainId;
            db.Insert(mailBody);
            foreach (MailAttach mailAttach in mailAttachs)//保存邮件附件
            {
                mailAttach.MailMainId = mailMain.MailMainId;
                db.Insert(mailAttach);
            }
            foreach (var email in mailMain.MailGroups)
            {
                email.MailMainId = mailMain.MailMainId;
                email.IsReceive = true;
                db.Insert(email);
            }

            #region 归档到默认文件夹
            if (!string.IsNullOrWhiteSpace(defaultFolderId))
            {
                MailMainFolder mailMainMailFolder = new MailMainFolder
                {
                    MailMainId = mailMain.MailMainId,
                    MailFolderId = defaultFolderId
                };
                db.Insert(mailMainMailFolder);

            }
            #endregion 归档到默认文件夹

            var emails = new List<string>();
            if (mailMain.MailType == (int)MailTypeEnum.InBox)
            {
                var emailGroup = UtilityHelper.GetMailReceiverGroup(mailMain.Sender);
                if (emailGroup != null && emailGroup.Count > 0)
                    emails.Add(emailGroup.First().Key);
            }
            else
            {
                emails = mailMain.MailGroups.Select(x => x.ReceiveAddress).ToList();
            }
            //归档到客户和联系人
            MailArchiveCustomer(db, mailMain.MailMainId, emails);

            #region 过滤，需要数据库连接
            EmailFilter.Filter.Handle(db, mailMain);
            #endregion

            #region 保存MessageID
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

            #endregion

            return mailMain;
        }
        /// <summary>
        /// 客户邮件归档
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailId"></param>
        /// <param name="emailAddressList"></param>
        public static void MailArchiveCustomer(IDbSession db, string mailId, IList<string> emailAddressList)
        {
            MailMain mail = new MailMain { MailMainId = mailId };
            db.Select(mail, MailMain_.OwnerUID, MailMain_.OCode);
            GetCustomerContactsList(db, mail.OwnerUID, mail.OCode);
            if (CustomerContactsList == null || CustomerContactsList.Count < 1) return;
            foreach (string email in emailAddressList)
            {
                var contact = CustomerContactsList.FirstOrDefault(x => x.Email == email);
                if (contact != null)
                {
                    MailMainBind(db, mail.MailMainId, MailFolderSourceTableEnum.Customer.ToString(), contact.ClientId, "[" + contact.ClientNo + "]" + contact.ClientName);
                }
            }
        }
        //归档到公司和联系人
        private static void MailMainBind(IDbSession db, string mailMainId, string sourceTable, string sourceId, string text)
        {
            string text2 = (text + "").Trim();

            MailMain mailMain = new MailMain
            {
                MailMainId = mailMainId
            };
            db.Select(mailMain, MailMain_.OCode, MailMain_.OwnerUID, MailMain_.Receiver, MailMain_.Sender, MailMain_.Subject, MailMain_.MailBoxId, MailMain_.MailType, MailMain_.IsArchived);
            if (sourceTable == MailFolderSourceTableEnum.Customer.ToString())//客户才设置归档
            {
                mailMain.IsArchived = true;
                db.Update(mailMain, MailMain_.IsArchived);
            }

            QueryExpression q = new QueryExpression { EntityType = typeof(MailFolder) };
            q.Selects.Add(MailFolder_.MailFolderId);
            q.Selects.Add(MailFolder_.MailCount);
            q.Wheres.Add(MailFolder_.SourceId.TEqual(sourceId));
            q.Wheres.Add(MailFolder_.SourceTable.TEqual(sourceTable));
            q.Wheres.Add(MailFolder_.OwnerUID.TEqual(mailMain.OwnerUID));
            q.Wheres.Add(MailFolder_.OCode.TEqual(mailMain.OCode));
            MailFolder mailFolder = db.SelectOne<MailFolder>(q);
            if (mailFolder == null || string.IsNullOrWhiteSpace(mailFolder.MailFolderId))
            {
                mailFolder = new MailFolder
                {
                    CreateTime = DateTime.Now,
                    CreateUID = mailMain.OwnerUID,
                    Depth = 1,
                    ServerFullFolderName = string.Empty,
                    MailBoxId = mailMain.MailBoxId,
                    MailFolderId = UtilityHelper.GetGuid(),
                    MailCount = 1,
                    MailType = (int)MailFolderEnum.Customize,
                    OCode = mailMain.OCode,
                    ParentId = string.Empty,
                    Sorting = 1,
                    UnreadCount = 0,
                    SourceId = sourceId,
                    SourceTable = sourceTable,
                    FolderName = text2,
                    OwnerUID = mailMain.OwnerUID
                };
                db.Insert(mailFolder);
            }
            else
            {
                mailFolder.MailCount += 1;
                db.Update(mailFolder, MailFolder_.MailCount);
            }
            MailMainFolder mailMainMailFolder = new MailMainFolder
            {
                MailFolderId = mailFolder.MailFolderId,
                MailMainId = mailMainId
            };
            if (!db.Exists(mailMainMailFolder))
            {
                db.Insert(mailMainMailFolder);
            }
        }
        /// <summary>
        /// 彻底删除邮件 收发服务过滤器使用
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ocode"></param>
        /// <param name="mailIds"></param>
        /// <returns></returns>
        public static ResponseResult RemoveMail(IDbSession db, string ocode, string[] mailIds)
        {
            return RemoveMail(db, mailIds, ocode, true);
        }

        #endregion 邮件服务相关

        #region 公用函数方法

        /// <summary>
        /// 邮件转换为MailBee邮件对象
        /// </summary>
        /// <param name="mailMain">邮件(主)表实体</param>
        /// <param name="mailBody">邮件正文实体</param>
        /// <param name="mailAttachs">邮件附件列表集合</param>
        /// <returns></returns>
        public static MailMessage ConvertToMailMessage(MailMain mailMain, MailBody mailBody, List<MailAttach> mailAttachs)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.MessageID = mailMain.MessageId;
            #region from to cc
            //from
            mailMessage.From = EmailAddress.Parse(mailMain.Sender);
            mailMessage.ReplyTo.Add(EmailAddress.Parse(mailMain.Sender));
            //注意：Replay是已读回执
            if (!string.IsNullOrEmpty(mailMain.Reply))
            {
                mailMessage.ConfirmRead = mailMain.Reply;
                mailMessage.ConfirmReceipt = mailMain.Reply;
            }
            //收件人
            var toEmails = mailMain.MailGroups.Where(x => x.ReceiveTypeCode == (int)MailGroupReceiveTypeEnum.Receiver);
            foreach (var email in toEmails)
            {
                mailMessage.To.Add(email.ReceiveAddress, email.ReceiveName);
            }
            //抄送
            toEmails = mailMain.MailGroups.Where(x => x.ReceiveTypeCode == (int)MailGroupReceiveTypeEnum.Cc);
            foreach (var email in toEmails)
            {
                mailMessage.Cc.Add(email.ReceiveAddress, email.ReceiveName);
            }
            //BCC 密送
            toEmails = mailMain.MailGroups.Where(x => x.ReceiveTypeCode == (int)MailGroupReceiveTypeEnum.Bcc);
            foreach (var email in toEmails)
            {
                mailMessage.Cc.Add(email.ReceiveAddress, email.ReceiveName);
            }

            #endregion from to cc
            if (string.IsNullOrWhiteSpace(mailMessage.MessageID))
                mailMessage.MessageID = "mail-" + UtilityHelper.GetGuid();
            mailMessage.Date = mailMain.MailTime;
            mailMessage.Subject = mailMain.Subject;

            if (mailMain.Importance.HasValue && mailMain.Importance < 3)//(mailMain.Importance == "3")
            {
                mailMessage.Priority = (MailPriority)mailMain.Importance.Value;//MailPriority.Highest;
                mailMessage.Importance = (MailPriority)mailMain.Importance.Value;
            }
            else
            {
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.Importance = MailPriority.Normal;
            }
            mailMessage.BodyHtmlText = mailBody.BodyHtml;
            mailMessage.MakePlainBodyFromHtmlBody();
            //注意：处理附件时一定要放在临时存储对象里
            Dictionary<string, MailAttach> tempAttachs = new Dictionary<string, MailAttach>(StringComparer.OrdinalIgnoreCase);//针对src，以本地绝对路径作为键 

            #region 加载附件

            if (mailAttachs != null && mailAttachs.Count > 0)
            {
                mailAttachs.ForEach(mailAttach =>
                {
                    string localFullPath = mailAttach.FilesPath.Replace(@"/", @"\");
                    if (!tempAttachs.ContainsKey(localFullPath))
                    {
                        tempAttachs.Add(localFullPath, mailAttach);
                    }
                });
            }

            #endregion 加载附件

            #region 将html内的src=相对目录的（如签名内的）图片转化为附件

            Regex re = new Regex("<img[^>]+?src=[\"']([^\"']+?)[\"'].+?>", RegexOptions.IgnoreCase);
            MatchCollection mc;
            mc = re.Matches(mailMessage.BodyHtmlText);
            for (int i = 0; i < mc.Count; i++)
            {

                string path = mc[i].Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(path) && !path.StartsWith("http", StringComparison.OrdinalIgnoreCase) && !path.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    string localFullPath = path.Replace(@"/", @"\");
                    //需要确保附件里有 
                    if (!tempAttachs.ContainsKey(localFullPath))
                    {
                        //说明临时附件夹内不存在，很可能来自签名的，需要先加入临时附件夹内
                        #region 获取显示用的文件名
                        string filename = string.Empty;
                        int index = localFullPath.LastIndexOf(@"\");
                        if (index > 0) filename = localFullPath.Substring(index + 1, localFullPath.Length - index - 1);
                        #endregion
                        MailAttach mailAttach = new MailAttach
                        {
                            FilesName = filename,
                            FilesPath = path
                        };
                        tempAttachs.Add(localFullPath, mailAttach);
                    }
                    //此时正则匹配到的src附件已经在临时附件表
                    if (string.IsNullOrWhiteSpace(tempAttachs[localFullPath].ContentID))
                    {
                        //如果已经存在，但是ContentId为空，那么需要给一个ContentId 一定要有contexntId
                        string contentId = "mail-" + UtilityHelper.GetGuid();
                        tempAttachs[localFullPath].ContentID = contentId;
                    }
                    mailMessage.BodyHtmlText = Regex.Replace(mailMessage.BodyHtmlText, Regex.Escape(path), "cid:" + tempAttachs[localFullPath].ContentID);
                }
            }

            #endregion 将html内的src=相对目录的（如签名内的）图片转化为附件

            #region 转化为MailBee的附件
            foreach (string localPath in tempAttachs.Keys)
            {
                //Console.WriteLine("___" + localPath + "___" + tempAttachs[localPath].FilesName + "___");
                //如果附件已经不存在了，则不加载附件
                if (File.Exists(localPath))
                {
                    mailMessage.Attachments.Add(localPath, tempAttachs[localPath].FilesName, tempAttachs[localPath].ContentID);
                }
                else
                {
                    Console.WriteLine("___" + localPath + "___" + tempAttachs[localPath].FilesName + "___");
                }
            }
            #endregion  转化为MailBee的附件

            //将base64转化为附件
            ConvertEditorBase64(mailMessage);
            return mailMessage;
        }
        private static void ConvertEditorBase64(MailMessage mailMessage)
        {
            HashSet<string> addedBase64s = new HashSet<string>();
            string pattern = @"src=""data:.*?;base64,(.*?)""";
            string pattern1 = @"src=""(data:.*?;base64.*?)""";
            MatchCollection matchs = Regex.Matches(mailMessage.BodyHtmlText, pattern);
            foreach (Match match in matchs)
            {
                Match m = new Regex(pattern, RegexOptions.Compiled).Match(match.Value);
                if (m.Groups.Count <= 0) continue;
                string base64String = m.Groups[1].Value;
                if (!addedBase64s.Contains(base64String))
                {
                    string contentId = "mail-" + UtilityHelper.GetGuid();
                    byte[] arr = Convert.FromBase64String(base64String);
                    mailMessage.Attachments.Add(arr, contentId + ".jpg", contentId, null, null, NewAttachmentOptions.None, MailTransferEncoding.None);
                    Match m1 = new Regex(pattern1, RegexOptions.Compiled).Match(match.Value);
                    if (m1.Groups.Count <= 0) continue;
                    string base64String1 = m1.Groups[1].Value;
                    mailMessage.BodyHtmlText = mailMessage.BodyHtmlText.Replace(base64String1, "cid:" + contentId);
                    addedBase64s.Add(base64String);
                }
            }
        }

        /// <summary>
        /// 传入abc@domain.com 返回abc
        /// </summary>
        /// <param name="emailAddress">邮件地址</param>
        /// <returns></returns>
        private static string GetDisplayName(EmailAddress emailAddress)
        {
            string displayName = emailAddress.DisplayName;
            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = emailAddress.Email;
            }
            displayName = displayName.Trim(new char[] { '\\', '"', '/', ',', '\'', ' ', '.', '(', ')', '-' });
            return displayName + "<" + emailAddress.Email + ">";

        }


        #endregion 公用函数方法
    }
}
