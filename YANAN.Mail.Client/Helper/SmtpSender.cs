using System;
using System.Collections.Generic;
using System.Linq;

namespace YANAN.Mail.Client
{
    using MailBee.Mime;
    using YANAN.Mail.Core;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Services;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using Titan;

    public class SmtpSender : SmtpSenderBase
    {

        public override void SaveSendBegin()
        {
            if (MailMain.MailState != MailStateEnum.SENDING.ToString())
            {
                using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
                {
                    MailMain.MailState = MailStateEnum.SENDING.ToString();//设置为发送中
                    db.Update(MailMain, MailMain_.MailState);
                    db.Close();
                }
            }
        }

        public override void SaveSendOK()
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();
                    MailMain.MailState = MailStateEnum.SEND_SUCCESS.ToString();//设置为发送成功
                    MailMain.Viewed = true;
                    List<string> mailUpdateFields = new List<string> { MailMain_.MailState.PropertyName, MailMain_.Viewed.PropertyName };
                    //MailMain.MailSize因为如果正文存在插入的图片则会在组装完MailMessage对象后重新赋值
                    if (MailMain.MailSize > 0) mailUpdateFields.Add(MailMain_.MailSize.PropertyName);
                    db.Update(MailMain, mailUpdateFields);
                    QueryExpression query;

                    #region 移动到发件箱

                    MailMainFolder oldMailMainMailFolder = new MailMainFolder
                    {
                        MailMainId = MailMain.MailMainId
                    };

                    #region 找出草稿箱

                    MailFolder folder = new MailFolder();
                    query = new QueryExpression
                    {
                        EntityType = typeof(MailFolder)
                    };
                    query.Wheres.Add(MailFolder_.SourceId.TEqual(MailMain.MailBoxId));
                    query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
                    query.Wheres.Add(MailFolder_.MailType.TEqual((int)MailFolderEnum.DraftBox));
                    query.Selects.Add(MailFolder_.MailFolderId);
                    folder = db.SelectOne<MailFolder>(query);
                    if (folder == null || string.IsNullOrWhiteSpace(folder.MailFolderId)) throw new Exception("保存失败:读取草稿箱失败");
                    oldMailMainMailFolder.MailFolderId = folder.MailFolderId;

                    #endregion 找出草稿箱

                    #region 找出发件箱

                    folder = new MailFolder();
                    query = new QueryExpression
                    {
                        EntityType = typeof(MailFolder)
                    };
                    query.Wheres.Add(MailFolder_.SourceId.TEqual(MailMain.MailBoxId));
                    query.Wheres.Add(MailFolder_.SourceTable.TEqual(MailFolderSourceTableEnum.MailBox.ToString()));
                    query.Wheres.Add(MailFolder_.MailType.TEqual((int)MailFolderEnum.OutBox));
                    query.Selects.Add(MailFolder_.MailFolderId);
                    folder = db.SelectOne<MailFolder>(query);
                    if (folder == null || string.IsNullOrWhiteSpace(folder.MailFolderId)) throw new Exception("保存失败:读取发件箱失败");
                    MailMainFolder newMailMainMailFolder = new MailMainFolder
                    {
                        MailMainId = MailMain.MailMainId,
                        MailFolderId = folder.MailFolderId
                    };

                    #endregion 找出发件箱

                    db.Delete(oldMailMainMailFolder);
                    if (!db.Exists(newMailMainMailFolder))
                    {
                        db.Insert(newMailMainMailFolder);
                    }

                    #endregion 移动到发件箱

                    MailService.UpdateMailCountAndSize(db, OCode, MailMain.OwnerUID, MailMain.MailBoxId, folder.MailFolderId, folder.MailType == (int)MailFolderEnum.InBox, (long)MailMain.MailSize);

                    MailService.MailArchiveCustomer(db, MailMain.MailMainId, MailMain.MailGroups.Select(x => x.ReceiveAddress).ToList());

                    #region 收件人/抄送人添加至通讯录 
                    Dictionary<string, string> contactsAddDic = new Dictionary<string, string>();
                    query = new QueryExpression { EntityType = typeof(MailContact) };
                    query.Wheres.Add(MailContact_.EMail.TIn("'" + string.Join("','", MailMain.MailGroups.Select(x => x.ReceiveAddress)) + "'"));
                    query.Wheres.Add(MailContact_.OwnerUID.TEqual(MailMain.OwnerUID));
                    query.Wheres.Add(MailContact_.OCode.TEqual(MailMain.OCode));
                    query.Selects.Add(MailContact_.MailContactId);
                    query.Selects.Add(MailContact_.LastContactTime);
                    MailContacts mailContacts = new MailContacts();
                    db.SelectCollection(mailContacts.Items, query);
                    if (mailContacts != null && mailContacts.Items.Count > 0)
                    {//如联系人已存在则更新联系人的最后联系时间
                        MailContact mailContact = new MailContact
                        {
                            LastContactTime = DateTime.Now
                        };
                        var condition = new ConditionExpressionCollection
                            {
                                { MailContact_.MailContactId.PropertyName, ConditionOperator.In, string.Join(",", mailContacts.Items.Select(x => x.MailContactId)) }
                            };
                        db.BatchUpdate(mailContact, condition, MailContact_.LastContactTime);
                        var emailList = mailContacts.Items.Select(xx => xx.EMail).ToList();
                        //取出未存在的联系人
                        contactsAddDic = MailMain.MailGroups.Where(x => emailList.Contains(x.ReceiveAddress)).ToDictionary(x => x.ReceiveAddress, x => x.ReceiveName);
                    }
                    else
                    {
                        contactsAddDic = MailMain.MailGroups.ToDictionary(x => x.ReceiveAddress, x => x.ReceiveName);
                    }
                    foreach (var item in contactsAddDic)
                    {
                        if (string.IsNullOrWhiteSpace(item.Key)) continue;
                        EmailAddress address = EmailAddress.Parse(item.Key);
                        MailContact obj = new MailContact
                        {
                            CreateUID = MailMain.CreateUID,
                            CreateTime = DateTime.Now,
                            EMail = address.Email,
                            OwnerUID = MailMain.OwnerUID,
                            ContactName = (!string.IsNullOrWhiteSpace(item.Value) ? item.Value : address.DisplayName),
                            OCode = MailMain.OCode,
                            Skype = string.Empty
                        };
                        obj.ContactPinyin = PinYinHelper.GetPinyin(obj.ContactName);
                        if (string.IsNullOrWhiteSpace(obj.ContactName))//如果联系人名称为空则去邮箱的前缀名
                            obj.ContactName = obj.EMail.Split(new char[] { '@' })[0];
                        db.Insert(obj);
                    }

                    #endregion 收件人/抄送人添加至通讯录

                    //过滤
                    Services.EmailFilter.Filter.Handle(db, MailMain);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw ex;
                }
            }
        }

        public override void SaveSendError()
        {
            if (MailMain.MailState != MailStateEnum.SEND_FAIL.ToString())
            {
                using (IDbSession cn = DbSessionHelper.OpenSessionSQLite())
                {
                    MailMain.MailState = MailStateEnum.SEND_FAIL.ToString();//设置为发送失败
                    cn.Update(MailMain, MailMain_.MailState);
                    cn.Close();
                }
            }
        }

        public override void SaveSendGroupOK()
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailGroup mailGroup = new MailGroup
                {
                    MailGroupId = Convert.ToInt32(CurrentGroupId),
                    IsSend = true,
                    IsReceive = true,
                    SendTime = DateTime.Now
                };
                db.Update(mailGroup, MailGroup_.IsSend, MailGroup_.IsReceive, MailGroup_.SendTime);
            }
        }

        public override void SaveSendGroupError()
        {
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                MailGroup mailGroup = new MailGroup
                {
                    MailGroupId = Convert.ToInt32(CurrentGroupId),
                    IsSend = true,
                    IsReceive = false,
                    SendTime = DateTime.Now
                };
                db.Update(mailGroup, MailGroup_.IsSend, MailGroup_.IsReceive, MailGroup_.SendTime);
            }
        }
    }
}
