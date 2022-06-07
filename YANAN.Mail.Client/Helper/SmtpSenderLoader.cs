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
    public class SmtpSenderLoader : SmtpSenderLoaderBase
    {

        public override Dictionary<string, List<string>> LoadAllSendTasks()
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            List<string> list = new List<string>();
            string companyCode = CurrentUserInfo.GetLoginedUserInfo().OCode;
            using (IDbSession cn = DbSessionHelper.OpenSessionSQLite())
            {
                #region 读取发送任务

                QueryExpression q2 = new QueryExpression() { EntityType = typeof(MailMain) };
                q2.Selects.Add(MailMain_.MailMainId);
                q2.Wheres.Add(MailMain_.Deleted.TEqual(false));
                q2.Wheres.Add(MailMain_.OCode.TEqual(companyCode));
                q2.Wheres.Add(MailMain_.MailType.TEqual((int)MailTypeEnum.OutBox));
                q2.Wheres.Add(MailMain_.MailState.TEqual(MailStateEnum.WAIT_SEND.ToString()));
                q2.PageIndex = 1;
                q2.PageSize = 10;
                ConditionExpressionCollection cs3 = new ConditionExpressionCollection
                {
                    ConditionRelation = ConditionRelation.Or
                };
                cs3.Add(MailMain_.IsTimer.TEqual(false));
                cs3.Add(MailMain_.IsTimer.TEqual(true).And(MailMain_.TimerSendTime.TLessThanOrEqual(DateTime.Now)));
                q2.Wheres.Add(cs3);
                MailMains mailMains2 = new MailMains();
                cn.SelectCollection(mailMains2.Items, q2);
                list.AddRange(mailMains2.Items.Select(x => x.MailMainId));
                #endregion
            }
            dic.Add(companyCode, list);
            return dic;
        }


        /// <summary>
        /// 加载发送邮件任务
        /// 调用此方法前，外部要先判断好是否定时发送等状态，mailIds数组长度不要太大
        /// </summary>
        /// <param name="ocode"></param>
        /// <param name="mailIds"></param>
        /// <returns></returns>
        public override List<SmtpSenderBase> LoadSendTasks(string ocode, string[] mailIds)
        {
            List<SmtpSenderBase> list = new List<SmtpSenderBase>();
            if (mailIds == null || mailIds.Length == 0) return list;
            using (IDbSession cn = DbSessionHelper.OpenSessionSQLite())
            {
                string idlist = "'" + string.Join("','", mailIds) + "'";
                QueryExpression q2 = new QueryExpression() { EntityType = typeof(MailMain) };
                q2.Selects.Add(MailMain_.ALL);
                q2.Selects.Add(MailMain_.MailBody.ALL);
                q2.Selects.Add(MailMain_.MailBox.ALL);
                q2.Wheres.Add(MailMain_.MailMainId.TIn(idlist));
                q2.Wheres.Add(MailMain_.OCode.TEqual(ocode));
                ConditionExpressionCollection cs3 = new ConditionExpressionCollection
                {
                    ConditionRelation = ConditionRelation.Or
                };
                cs3.Add(MailMain_.IsTimer.TEqual(false));
                cs3.Add(MailMain_.IsTimer.TEqual(true).And(MailMain_.TimerSendTime.TLessThanOrEqual(DateTime.Now)));
                q2.Wheres.Add(cs3);
                MailMains mailMains2 = new MailMains();
                cn.SelectCollection(mailMains2.Items, q2);
                foreach (MailMain mailMain2 in mailMains2.Items)
                {
                    string key = SmtpSenderHost.CreateKey(ocode, mailMain2.MailMainId.ToString());
                    if (SmtpSenderHost.SenderPool.TaskExists(key))
                    {
                        continue;
                    }

                    #region load mailAttachs 读取邮件附件

                    QueryExpression q1 = new QueryExpression() { EntityType = typeof(MailAttach) };
                    q1.Selects.Add(MailAttach_.ALL);
                    q1.Wheres.Add(MailAttach_.MailMainId.TEqual(mailMain2.MailMainId));
                    mailMain2.MailAttachs = new List<MailAttach>();
                    cn.SelectCollection(mailMain2.MailAttachs, q1);

                    #endregion load mailAttachs 读取邮件附件

                    #region load mail group 分别发送/收件人抄送

                    QueryExpression q3 = new QueryExpression() { EntityType = typeof(MailGroup) };
                    q3.Selects.Add(MailGroup_.ALL);
                    q3.Wheres.Add(MailGroup_.MailMainId.TEqual(mailMain2.MailMainId));
                    ConditionExpressionCollection cs = new ConditionExpressionCollection
                    {
                        ConditionRelation = ConditionRelation.Or
                    };
                    cs.Add(MailGroup_.IsSend.TEqual(null));//上次发送成功的不用加载
                    cs.Add(MailGroup_.IsSend.TEqual(false));
                    cs.Add(MailGroup_.IsReceive.TEqual(null));//上次发送成功的不用加载
                    cs.Add(MailGroup_.IsReceive.TEqual(false));
                    q3.Wheres.Add(cs);
                    q3.PageSize = int.MaxValue;
                    q3.PageIndex = 1;
                    mailMain2.MailGroups = new List<MailGroup>();
                    cn.SelectCollection(mailMain2.MailGroups, q3);

                    #endregion  load mail group 分别发送/收件人抄送

                    //创建MailMessage
                    MailMessage mailMessage = MailService.ConvertToMailMessage(mailMain2, mailMain2.MailBody, mailMain2.MailAttachs);
                    mailMain2.MailSize = mailMessage.Size;
                    SmtpSender mailSender = new SmtpSender
                    {
                        Key = key,
                        Port = mailMain2.MailBox.SmtpPort == 0 ? 25 : mailMain2.MailBox.SmtpPort,
                        ServerAddress = mailMain2.MailBox.SmtpServer,
                        UserName = mailMain2.MailBox.MailAddress,
                        Password = EncryptHelper.DecodeBase64(mailMain2.MailBox.MailPassword),
                        OCode = ocode,
                        MailMain = mailMain2,
                        IsDistribution = mailMain2.IsGroup,
                        MailMessage = mailMessage
                    };
                    if (mailMain2.IsGroup)
                    {
                        mailSender.GroupAddress = new Dictionary<string, string>();
                        mailMain2.MailGroups.ForEach(mailGroup =>
                        {
                            if (mailGroup.ReceiveTypeCode == (int)MailGroupReceiveTypeEnum.Receiver && !mailSender.GroupAddress.ContainsKey(mailGroup.ReceiveAddress))
                            {
                                mailSender.GroupAddress.Add(mailGroup.ReceiveAddress, mailGroup.MailGroupId.ToString());
                            }
                        });
                    }
                    list.Add(mailSender);
                }
            }

            return list;
        }



    }
}
