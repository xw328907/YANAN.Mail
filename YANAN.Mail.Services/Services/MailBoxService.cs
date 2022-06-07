using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace YANAN.Mail.Services
{
    using MailBee;
    using MailBee.ImapMail;
    using MailBee.Pop3Mail;
    using MailBee.SmtpMail;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using YANAN.Mail.Utilities.Extensions;
    using System.Data;
    using Titan;

    public class MailBoxService : IServices.IMailBoxService
    {
        #region 邮箱MailBox

        /// <summary>
        /// 添加邮箱绑定；保存前如下逻辑判断控制：
        /// 1、检查邮箱配置是否正确，能否链接邮局正常收发邮件
        /// 2、判断用户邮箱是否重复(本应控制整个系统是否重复,目前仅判断单一用户名下是否重复)
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        public ResponseResult AddMailBox(LoginedUserInfo loginInfo, MailBox mailBox)
        {
            ResponseResult result = new ResponseResult();
            if (mailBox == null)
            {
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.GetDescription();
                return result;
            }
            result = VerifyMainBox(loginInfo, mailBox);
            if (result.Code != ResponseCodeEnum.SUCCESS.ToString())
            {
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    { MailBox_.MailAddress.PropertyName, ConditionOperator.Equal, mailBox.MailAddress },
                    { MailBox_.OwnerUID.PropertyName, ConditionOperator.Equal, loginInfo.UserId },
                    { MailBox_.Deleted.PropertyName, ConditionOperator.Equal, false }
                };
                if (db.Exists<MailBox>(conditions))
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_EXIST.ToString();
                    result.Message = "邮箱已存在";
                    return result;
                }
                try
                {
                    db.BeginTransaction();
                    if (mailBox.IsDefault == true)
                    {
                        MailBox box = new MailBox { IsDefault = false };
                        db.BatchUpdate(box, new ConditionExpression(MailBox_.OwnerUID.PropertyName, ConditionOperator.Equal, mailBox.OwnerUID), MailBox_.IsDefault.PropertyName);
                    }
                    mailBox.MailPassword = EncryptHelper.EncodeBase64(mailBox.MailPassword);
                    mailBox.Deleted = false;
                    mailBox.CreateTime = DateTime.Now;
                    mailBox.CreateUID = loginInfo.UserId;
                    mailBox.OwnerUID = loginInfo.UserId;
                    mailBox.OCode = loginInfo.OCode;
                    if (string.IsNullOrWhiteSpace(mailBox.ShowName))
                        mailBox.ShowName = mailBox.MailAddress;
                    if (mailBox.SendTimer < 1) mailBox.SendTimer = 5;
                    if (mailBox.ReceiveTimer < 1) mailBox.ReceiveTimer = 15;
                    if (mailBox.Sorting < 1)
                        mailBox.Sorting = 1;
                    mailBox.MailCount = 0;
                    mailBox.MailSize = 0;
                    mailBox.MailBoxId = UtilityHelper.GetGuid();
                    db.Insert(mailBox);
                    #region  pop3协议增加邮箱文件夹
                    if (mailBox.ProtocolTypeId == 1)
                    {
                        MailFolder mailFolder = new MailFolder
                        {
                            Depth = 1,
                            OwnerUID = mailBox.CreateUID,
                            SourceId = mailBox.MailBoxId,
                            SourceTable = MailFolderSourceTableEnum.MailBox.ToString(),
                            CreateTime = DateTime.Now,
                            CreateUID = mailBox.CreateUID,
                            OCode = mailBox.OCode,
                            MailBoxId = mailBox.MailBoxId,
                            ParentId = string.Empty,
                            Sorting = 1,
                            UnreadCount = 0
                        };

                        mailFolder.FolderName = MailFolderEnum.InBox.GetDescription();
                        mailFolder.MailFolderId = UtilityHelper.GetGuid();
                        mailFolder.Sorting = 1;
                        mailFolder.MailType = (int)MailFolderEnum.InBox;
                        db.Insert(mailFolder);

                        mailFolder.FolderName = MailFolderEnum.OutBox.GetDescription();
                        mailFolder.MailFolderId = UtilityHelper.GetGuid();
                        mailFolder.Sorting = 2;
                        mailFolder.MailType = (int)MailFolderEnum.OutBox;
                        db.Insert(mailFolder);

                        mailFolder.FolderName = MailFolderEnum.DraftBox.GetDescription();
                        mailFolder.MailFolderId = UtilityHelper.GetGuid();
                        mailFolder.Sorting = 3;
                        mailFolder.MailType = (int)MailFolderEnum.DraftBox;
                        db.Insert(mailFolder);

                        mailFolder.FolderName = MailFolderEnum.TrashBox.GetDescription();
                        mailFolder.MailFolderId = UtilityHelper.GetGuid();
                        mailFolder.Sorting = 4;
                        mailFolder.MailType = (int)MailFolderEnum.TrashBox;
                        db.Insert(mailFolder);

                        mailFolder.FolderName = MailFolderEnum.Deleted.GetDescription();
                        mailFolder.MailFolderId = UtilityHelper.GetGuid();
                        mailFolder.Sorting = 5;
                        mailFolder.MailType = (int)MailFolderEnum.Deleted;
                        db.Insert(mailFolder);
                    }
                    #endregion  pop3协议增加邮箱文件夹
                    result.Data = mailBox;
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    result.Message = ex.Message;
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    //  throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 更新邮箱
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        public ResponseResult UpdateMailBox(LoginedUserInfo loginInfo, MailBox mailBox)
        {
            ResponseResult result = new ResponseResult();
            if (mailBox == null || string.IsNullOrWhiteSpace(mailBox.MailBoxId))
            {
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.GetDescription();
                return result;
            }
            if (mailBox.ProtocolTypeId < 1) mailBox.ProtocolTypeId = 1;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailBox oldBox = new MailBox { MailBoxId = mailBox.MailBoxId };
                bool flag = db.Select(oldBox);
                if (!flag)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                    result.Message = "邮箱不存在或已删除";
                    return result;
                }
                else if (oldBox.OCode != loginInfo.OCode || oldBox.OwnerUID != loginInfo.UserId)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                    result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                    return result;
                }
                if (oldBox.MailCount > 0 && oldBox.ProtocolTypeId != mailBox.ProtocolTypeId)//已收取邮件不允许更改协议类型
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "不允许修改收件协议类型,如需修改请先删除后再重新配置";
                    return result;
                }
                mailBox.OCode = oldBox.OCode;
                if (string.IsNullOrWhiteSpace(mailBox.MailPassword))
                {//修改时可不输入邮箱密码,如未输入则取原密码进行验证
                    mailBox.MailPassword = EncryptHelper.DecodeBase64(oldBox.MailPassword);
                }
                result = VerifyMainBox(loginInfo, mailBox);
                if (result.Code != ResponseCodeEnum.SUCCESS.ToString())
                {
                    return result;
                }
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    { MailBox_.MailAddress.PropertyName, ConditionOperator.Equal, mailBox.MailAddress },
                    { MailBox_.MailBoxId.PropertyName, ConditionOperator.NotEqual, mailBox.MailBoxId },
                    { MailBox_.OwnerUID.PropertyName, ConditionOperator.Equal, mailBox.OwnerUID },
                    { MailBox_.OCode.PropertyName, ConditionOperator.Equal, mailBox.OCode },
                    { MailBox_.Deleted.PropertyName, ConditionOperator.Equal, false }
                };
                if (db.Exists<MailBox>(conditions))
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_EXIST.ToString();
                    result.Message = "邮箱已存在";
                    return result;
                }
                try
                {
                    db.BeginTransaction();
                    if (mailBox.IsDefault == true)
                    {
                        MailBox box = new MailBox { IsDefault = false };
                        conditions = new ConditionExpressionCollection
                        {
                            ConditionRelation = ConditionRelation.And
                        };
                        conditions.Add(MailBox_.OwnerUID.PropertyName, ConditionOperator.Equal, oldBox.OwnerUID);
                        conditions.Add(MailBox_.OCode.PropertyName, ConditionOperator.Equal, mailBox.OCode);
                        conditions.Add(MailBox_.Deleted.PropertyName, ConditionOperator.Equal, false);
                        conditions.Add(MailBox_.MailBoxId.PropertyName, ConditionOperator.NotEqual, mailBox.MailBoxId);
                        db.BatchUpdate(box, conditions, MailBox_.IsDefault);
                    }
                    List<string> updates = new List<string> {
                        MailBox_.IsDefault.PropertyName, MailBox_.KeepDays.PropertyName, MailBox_.MailAddress.PropertyName,MailBox_.ShowName.PropertyName,
                        MailBox_.NickName.PropertyName, MailBox_.PopPort.PropertyName, MailBox_.PopServer.PropertyName, MailBox_.ProtocolTypeId.PropertyName,
                        MailBox_.SmtpPort.PropertyName, MailBox_.SmtpServer.PropertyName, MailBox_.Deleted.PropertyName,MailBox_.ReceiveTimer.PropertyName
                        ,MailBox_.ReceiveBeginTime.PropertyName,MailBox_.Bcc.PropertyName,MailBox_.Cc.PropertyName
                    };
                    if (string.IsNullOrWhiteSpace(mailBox.ShowName))
                        mailBox.ShowName = mailBox.MailAddress;
                    if (!string.IsNullOrWhiteSpace(mailBox.MailPassword))
                    {
                        updates.Add(MailBox_.MailPassword.PropertyName);
                        mailBox.MailPassword = EncryptHelper.EncodeBase64(mailBox.MailPassword);
                    }
                    mailBox.Deleted = false;
                    db.Update(mailBox, updates);
                    result.Data = mailBox;
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
        /// 校验邮箱收发服务配置是否正确
        /// </summary>
        /// <param name="mailBox">邮箱配置信息Dto</param>
        /// <returns>ResponseResult.Code=SUCCESS 校验成功</returns>
        public ResponseResult VerifyMainBox(LoginedUserInfo loginInfo, MailBox mailBox)
        {
            ResponseResult result = new ResponseResult();
            Global.LicenseKey = ConstConfig.MailBeeLicenseKey;

            #region 收取邮箱验证IMAP/POP3
            if (mailBox.ProtocolTypeId == 2)
            {
                Imap imap = new Imap
                {
                    Timeout = 10000
                };
                try
                {
                    imap.Connect(mailBox.PopServer, mailBox.PopPort == 0 ? 143 : mailBox.PopPort);
                }
                catch (Exception ex)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "IMAP地址或端口不正确或无法连接:" + ex.Message;
                    return result;
                }
                try
                {
                    imap.Login(mailBox.MailAddress, mailBox.MailPassword, AuthenticationMethods.Auto);
                }
                catch (Exception ex)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "IMAP用户名或密码错误:" + ex.Message;
                    return result;
                }
                finally
                {
                    if (imap.IsConnected)
                        imap.Disconnect();
                }
            }
            else if (mailBox.ProtocolTypeId == 1)
            {
                Pop3 pop3 = new Pop3() { Timeout = 10000 };
                try
                {
                    pop3.Connect(mailBox.PopServer, mailBox.PopPort == 0 ? 110 : mailBox.PopPort);
                }
                catch (Exception ex)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "POP3地址或端口不正确或无法连接:" + ex.Message;
                    return result;
                }
                try
                {
                    pop3.Login(mailBox.MailAddress, mailBox.MailPassword, AuthenticationMethods.Auto);
                }
                catch (Exception ex)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "POP3用户名或密码错误:" + ex.Message;
                    return result;
                }
                finally
                {
                    if (pop3.IsConnected) pop3.Disconnect();
                }
            }
            else
            {
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.Message = "收件协议错误";
                return result;
            }
            #endregion 收取邮箱验证IMAP/POP3

            #region 发送邮箱验证Smtp
            Smtp smtp = new Smtp();
            SmtpServer smtpServer = new SmtpServer
            {
                Timeout = 10000,
                Name = mailBox.SmtpServer,
                Port = mailBox.SmtpPort == 0 ? 25 : mailBox.SmtpPort,
                AccountName = mailBox.MailAddress,
                Password = mailBox.MailPassword,
                AuthMethods = AuthenticationMethods.Auto
            };
            smtp.SmtpServers.Add(smtpServer);
            try
            {
                smtp.Connect();
            }
            catch (Exception ex)
            {
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.Message = "SMTP地址或端口不正确或无法连接:" + ex.Message;
                return result;
            }
            finally
            {
                if (smtp.IsConnected) smtp.Disconnect();
            }
            #endregion 发送邮箱验证Smtp
            return result;
        }
        /// <summary>
        /// 获取当前用户的邮箱列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public IList<MailBox> GetListMailBoxCurrentUser(LoginedUserInfo loginInfo)
        {
            QueryExpression query = new QueryExpression { EntityType = typeof(MailBox) };
            query.Wheres.Add(MailBox_.OwnerUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailBox_.Deleted.TEqual(false));
            query.Selects.Add(MailBox_.ALL.Where(o => o.PropertyName != MailBox_.MailPassword.PropertyName));
            query.OrderBys.Add(MailBox_.Sorting.PropertyName, OrderType.Asc);
            query.OrderBys.Add(MailBox_.CreateTime.PropertyName, OrderType.Asc);
            MailBoxs mailBoxs = new MailBoxs();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                db.SelectCollection(mailBoxs.Items, query);
            }
            //mailBoxs.Items.ForEach(item => { item.MailPassword = string.Empty; });
            return mailBoxs.Items;
        }
        /// <summary>
        /// 删除邮箱，逻辑如下：
        /// 1、判断邮箱是否存在
        /// 2、只有邮箱所有者才可删除
        /// 3、执行删除
        /// 4、如当前删除邮箱为默认邮箱则再设置一个默认邮箱
        /// </summary>
        /// <param name="mailBoxId">邮箱id</param>
        /// <returns></returns>
        public ResponseResult RemoveMailBox(LoginedUserInfo loginInfo, string mailBoxId)
        {
            ResponseResult result = new ResponseResult() { Message = "删除成功" };
            if (string.IsNullOrWhiteSpace(mailBoxId))
            {
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.GetDescription();
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailBox mailBox = new MailBox { MailBoxId = mailBoxId };
                bool flag = db.Select(mailBox);
                if (!flag || mailBox.Deleted == true)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                    result.Message = "邮箱不存在或已删除";
                    return result;
                }
                if (mailBox.OwnerUID != loginInfo.UserId)
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                    result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                    return result;
                }
                try
                {
                    db.BeginTransaction();
                    mailBox.Deleted = true;
                    db.Update(mailBox, MailBox_.Deleted);
                    if (mailBox.IsDefault == true)
                    {
                        QueryExpression query = new QueryExpression { EntityType = typeof(MailBox) };
                        query.Selects.Add(MailBox_.MailBoxId);
                        query.Selects.Add(MailBox_.IsDefault);
                        query.Wheres.Add(MailBox_.OwnerUID.TEqual(mailBox.OwnerUID));
                        query.Wheres.Add(MailBox_.Deleted.TEqual(false));
                        query.OrderBys.Add(MailBox_.Sorting.PropertyName, OrderType.Asc);
                        query.OrderBys.Add(MailBox_.CreateTime.PropertyName, OrderType.Asc);
                        var defaultBox = db.SelectOne<MailBox>(query);
                        if (defaultBox != null)
                        {
                            defaultBox.IsDefault = true;
                            db.Update(defaultBox, MailBox_.IsDefault);
                        }
                    }
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "删除失败";
                }
            }

            return result;
        }
        #endregion 邮箱MailBox

        #region 邮箱文件夹

        /// <summary>
        /// 添加邮箱文件夹/修改文件夹名
        /// </summary>
        /// <param name="mailFolder">需添加的邮箱文件夹对象</param>
        /// <returns></returns>
        public ResponseResult AddMailFolder(LoginedUserInfo loginInfo, MailFolder mailFolder)
        {
            ResponseResult result = new ResponseResult();
            if (mailFolder == null || string.IsNullOrWhiteSpace(mailFolder.FolderName) || string.IsNullOrWhiteSpace(mailFolder.MailBoxId))
            {
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.GetDescription();
                result.Code = ResponseCodeEnum.ERROR.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                return result;
            }
            mailFolder.CreateTime = DateTime.Now;
            mailFolder.CreateUID = loginInfo.UserId;
            mailFolder.OwnerUID = mailFolder.CreateUID;
            mailFolder.OCode = loginInfo.OCode;
            mailFolder.Depth = 1;
            mailFolder.MailType = (int)MailFolderEnum.Customize;
            mailFolder.SourceId = mailFolder.MailBoxId;
            mailFolder.SourceTable = MailFolderSourceTableEnum.MailBox.ToString();
            mailFolder.UnreadCount = 0;
            mailFolder.MailCount = 0;
            if (mailFolder.ParentId == null) mailFolder.ParentId = string.Empty;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                    {
                        MailFolder_.SourceId.TEqual(mailFolder.SourceId),
                        MailFolder_.SourceTable.TEqual(mailFolder.SourceTable),
                        MailFolder_.ParentId.TEqual(mailFolder.ParentId),
                        MailFolder_.FolderName.TEqual(mailFolder.FolderName)
                    };
                if (!string.IsNullOrWhiteSpace(mailFolder.MailFolderId))
                {
                    conditions.Add(MailFolder_.MailFolderId.TNotEqual(mailFolder.MailFolderId));
                }
                if (db.Exists<MailFolder>(conditions))
                {
                    result.Message = "文件夹名已存在";
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_EXIST.ToString();
                    return result;
                }
                if (string.IsNullOrWhiteSpace(mailFolder.MailFolderId))
                {
                    if (!string.IsNullOrWhiteSpace(mailFolder.ParentId))
                    {
                        MailFolder parentFolder = new MailFolder { MailFolderId = mailFolder.ParentId };
                        bool flag = db.Select(parentFolder, MailFolder_.MailFolderId, MailFolder_.Depth);
                        if (flag) mailFolder.Depth = parentFolder.Depth + 1;
                    }
                    mailFolder.MailFolderId = UtilityHelper.GetGuid();
                    if (mailFolder.ServerFullFolderName == null)
                        mailFolder.ServerFullFolderName = string.Empty;
                    db.Insert(mailFolder);
                }
                else
                {
                    db.Update(mailFolder, MailFolder_.FolderName);
                }
                result.Data = mailFolder;
            }
            return result;
        }
        /// <summary>
        /// 获取邮箱下所有文件夹
        /// </summary>
        /// <param name="mailboxIds">邮箱ID编号数组</param>
        /// <returns></returns>
        public IList<MailFolder> GetListMailFolderByMailBoxIds(string[] mailboxIds)
        {
            List<MailFolder> list = new List<MailFolder>();
            if (mailboxIds == null || mailboxIds.Length < 1) return list;
            string ids = "'" + string.Join("','", mailboxIds) + "'";
            QueryExpression query = new QueryExpression { EntityType = typeof(MailFolder) };
            query.Wheres.Add(MailFolder_.SourceId.TIn(ids));
            query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
            query.Selects.Add(MailFolder_.ALL);
            query.OrderBys.Add(MailFolder_.Sorting.PropertyName, OrderType.Asc);
            query.OrderBys.Add(MailFolder_.CreateTime.PropertyName, OrderType.Asc);
            MailFolders folders = new MailFolders();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                db.SelectCollection(folders.Items, query);
            }
            return folders.Items;
        }

        /// <summary>
        /// 删除邮箱文件夹，逻辑如下：
        /// 1、只能删除归属于自己的文件夹
        /// 2、只能删除自定义类型文件夹 MailType=0
        /// 3、存在子文件夹的不能删除
        /// 4、文件夹存在邮件的不能删除
        /// 5、删除文件夹
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public ResponseResult RemoveMailFolder(LoginedUserInfo loginInfo, string folderId)
        {
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrWhiteSpace(folderId))
            {
                result.Message = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.ErrorCode = ResponseErrorCodeEnum.ERROR_PARAMETER.ToString();
                result.Code = ResponseCodeEnum.ERROR.ToString();
                return result;
            }
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {

                MailFolder mailFolder = new MailFolder { MailFolderId = folderId };
                bool flag = db.Select(mailFolder);
                if (!flag)
                {
                    result.Message = ResponseErrorCodeEnum.ERROR_NOT_EXIST.GetDescription();
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                    return result;
                }
                if (mailFolder.OwnerUID != loginInfo.UserId)
                {
                    result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                    return result;
                }
                if (mailFolder.MailType != (int)MailFolderEnum.Customize)
                {
                    result.Message = "系统文件夹禁止删除";
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                    return result;
                }
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    MailFolder_.MailBoxId.TEqual(mailFolder.MailBoxId),
                    MailFolder_.ParentId.TEqual(mailFolder.MailFolderId)
                };
                if (!db.Exists<MailFolder>(conditions))
                {
                    conditions = new ConditionExpressionCollection
                            {
                                { MailMainFolder_.MailFolderId.PropertyName, ConditionOperator.Equal, mailFolder.MailFolderId },
                                { MailMainFolder_.MailFolder.SourceTable.PropertyName, ConditionOperator.Equal, mailFolder.SourceTable }
                            };
                    var countMailFolderMail = db.SelectCount<MailMainFolder>(conditions);
                    if (countMailFolderMail > 0)
                    {
                        result.Message = "文件夹存在邮件,请先移除邮件后再删除";
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        return result;
                    }
                    db.Delete(mailFolder);
                }
                else
                {
                    result.Message = "存在子文件夹,请先删除子文件夹";
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    return result;
                }

            }
            return result;
        }
        /// <summary>
        /// 设置邮箱文件夹全部邮件已读
        /// </summary>
        /// <param name="mailFolderId">邮件文件夹ID编号</param>
        /// <returns></returns>
        public ResponseResult SetMailFolderRead(LoginedUserInfo loginInfo, string mailFolderId)
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
                        sql += "update MailMain set Viewed=1 where MailMainId in(select MailMain.MailMainId from MailMain left join MailMainFolder mmf on MailMain.MailMainId=mmf.MailMainId where MailMain.Viewed=0 and mmf.MailFolderId=@MailFolderId);";
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
                        command.Parameters.Add("@OwnerUID", DbType.String).Value = loginInfo.UserId;
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
        /// 获取当前登录用户的客户列表(存在往来邮件的客户)
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public IList<MailFolder> GetListMailCustomer(LoginedUserInfo loginInfo)
        {
            List<MailFolder> list = new List<MailFolder>();
            var customers = MailService.CustomerContactsList;
            if (customers == null || customers.Count < 1) return list;
            IList<string> customerIds = customers.Select(x => x.ClientId).Distinct().ToList();
            string ids = "'" + string.Join("','", customerIds) + "'";
            QueryExpression query = new QueryExpression { EntityType = typeof(MailFolder) };
            query.Wheres.Add(MailFolder_.SourceId.TIn(ids));
            query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.Customer.ToString()));
            query.Wheres.Add(MailFolder_.MailCount.TGreaterThan(0));
            query.Wheres.Add(MailFolder_.OCode.TEqual(loginInfo.OCode));
            query.Selects.Add(MailFolder_.ALL);
            query.OrderBys.Add(MailFolder_.Sorting.PropertyName, OrderType.Asc);
            query.OrderBys.Add(MailFolder_.FolderName.PropertyName, OrderType.Asc);
            query.IsDistinct = true;
            MailFolders folders = new MailFolders();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                db.SelectCollection(folders.Items, query);
            }
            return folders.Items;
        }
        #endregion 邮箱文件夹

    }
}
