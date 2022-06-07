using MailBee;
using MailBee.Mime;
using MailBee.SmtpMail;
using YANAN.Mail.Core.ThreadTask;
using YANAN.Mail.Entity;
using YANAN.Mail.Utilities.Enums;
using System;
using System.Collections.Generic;

namespace YANAN.Mail.Core
{
    public abstract class SmtpSenderBase : TaskBase
    {
        #region 发邮件必须有的属性
        /// <summary>
        /// 公司编号
        /// </summary>
        public string OCode { get; set; }
        /// <summary>
        /// 邮件信息
        /// </summary>
        public MailMain MailMain { get; set; }
        /// <summary>
        /// 邮件发送服务器地址
        /// </summary>
        public string ServerAddress { get; set; }
        /// <summary>
        /// 邮件发送端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 待发送的邮件
        /// </summary>
        public MailMessage MailMessage { get; set; }
        /// <summary>
        /// 是否分别发送
        /// </summary>
        public bool IsDistribution { get; set; }

        #endregion 发邮件必须有的属性

        /// <summary>
        /// 接收者列表， z，发送过的不要放在这里,键是收件地址，值是id
        /// </summary>
        public Dictionary<string, string> GroupAddress { get; set; }

        public int TaskCount { get; private set; }
        public int CompletedCount { get; private set; }//下载完成后才会+1，为了能够继续，下载失败也会+1
        public SmtpWorkingStep CurrentSmtpWorkingStep { get; private set; }
        public string CurrentReceiver { get; private set; }//在分发模式下当前收件人特别有用
        public string CurrentSubject { get; private set; }//单封邮件下载完成后才会设置，
        public Exception CurrentException { get; private set; }
        public string CurrentGroupId { get; private set; }

        public SmtpSenderBase()
        {
            Port = 25;
        }

        public override void DoWork()
        {
            CurrentReceiver = "";
            CurrentSubject = "";
            CurrentException = null;
            Smtp smtp = new Smtp();
            if (Logger.MailBeeLogTrack == true)
            {
                string path = Logger.MailBeeLogTrackFilePath;
                if (string.IsNullOrWhiteSpace(path))
                    path = Utilities.AssemblyHelper.GetBaseDirectory();
                path += "\\log_smtp\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                smtp.Log.Enabled = true;
                smtp.Log.Filename = path + Key + ".txt";//加上Key用来避免不同线程写日志冲突
                smtp.Log.Clear();
            }
            try
            {

                #region 属性校验
                CurrentSmtpWorkingStep = SmtpWorkingStep.Prepare;
                SmtpWorking();
                if (MailMessage == null)
                {
                    throw new Exception("参数错误:MailMessage");
                }
                if (string.IsNullOrWhiteSpace(ServerAddress))
                {
                    throw new Exception("邮箱SMTP地址为空");
                }
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    throw new Exception("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("邮箱密码不能为空");
                }
                if (Port <= 0)
                {
                    throw new Exception(string.Format("错误的邮件端口号{0}", Port));
                }
                if (IsDistribution && GroupAddress == null)
                {
                    throw new Exception("分别发送模式下GroupAddress不允许为空");
                }

                CurrentSmtpWorkingStep = SmtpWorkingStep.Ready;
                SmtpWorking();
                #endregion

                SaveSendBegin();

                //mailbee8.0版本后会自动encode 详情见：http://www.afterlogic.com/mailbee/docs/History.htm
                //MailMessage.Charset = "UTF-8";
                //MailMessage.EncodeAllHeaders(System.Text.Encoding.GetEncoding(MailMessage.Charset), HeaderEncodingOptions.Base64); 
                smtp.Message = MailMessage;
                if (!string.IsNullOrWhiteSpace(MailMessage.ConfirmRead) || !string.IsNullOrWhiteSpace(MailMessage.ConfirmReceipt))
                {
                    // Configure Delivery Status Notification (DSN).
                    smtp.DeliveryNotification.NotifyCondition = DsnNotifyCondition.Always;
                }

                SmtpServer smtpServer = new SmtpServer
                {
                    Name = ServerAddress,
                    Port = Port,
                    AccountName = UserName,
                    Password = Password,
                    AuthMethods = AuthenticationMethods.Auto
                };
                smtp.SmtpServers.Add(smtpServer);

                CurrentSmtpWorkingStep = SmtpWorkingStep.Connecting;
                SmtpWorking();
                smtp.Connect();
                CurrentSmtpWorkingStep = SmtpWorkingStep.Connected;
                SmtpWorking();

                if (IsDistribution)
                {
                    #region 如果是群发
                    int errorCount = 0;
                    TaskCount = GroupAddress.Keys.Count;
                    CompletedCount = 0;
                    foreach (string address in GroupAddress.Keys)
                    {
                        try
                        {
                            CurrentReceiver = address;
                            CurrentGroupId = GroupAddress[address];
                            CurrentSubject = MailMessage.Subject;
                            CurrentSmtpWorkingStep = SmtpWorkingStep.DistributSending;
                            SmtpWorking();

                            MailMessage.To.Clear();
                            MailMessage.To.Add(EmailAddress.Parse(address));

                            try
                            {
                                smtp.Send();
                            }
                            catch
                            {
                                smtp.SmtpServers[0].AuthMethods = AuthenticationMethods.SaslLogin;
                                smtp.Connect();
                                smtp.Send();
                            }

                            CurrentSmtpWorkingStep = SmtpWorkingStep.DistributSended;
                            SmtpWorking();

                            SaveSendGroupOK();
                        }
                        catch (MailBeeException ex)
                        {
                            errorCount++;
                            CurrentException = ex;
                            SaveSendGroupError();
                            ProgramError();
                            //如果出错则继续下一封发送 
                        }
                        CompletedCount++;
                    }

                    if (errorCount == 0)
                    {
                        //发送中状态应该是只对群发有效
                        SaveSendOK();//如果全部成功，则设置为成功
                    }
                    else
                    {
                        throw new Exception(string.Format("分别发送时有{0}封失败。{1}", errorCount, CurrentException.Message));
                    }
                    #endregion
                }
                else
                {
                    #region 如果不是群发
                    CurrentReceiver = MailMessage.To.ToString();
                    CurrentSubject = MailMessage.Subject;

                    CurrentSmtpWorkingStep = SmtpWorkingStep.Sending;
                    SmtpWorking();

                    try
                    {
                        smtp.Send();
                    }
                    catch
                    {
                        smtp.SmtpServers[0].AuthMethods = AuthenticationMethods.SaslLogin;
                        smtp.Connect();
                        smtp.Send();
                    }

                    CurrentSmtpWorkingStep = SmtpWorkingStep.Sended;
                    SmtpWorking();

                    SaveSendOK();
                    #endregion
                }

                CloseSmtpConnection(smtp);

                CurrentSmtpWorkingStep = SmtpWorkingStep.End;
                SmtpWorking();
            }
            catch (Exception ex)
            {
                CloseSmtpConnection(smtp);
                CurrentException = ex;
                try
                {
                    SaveSendError();
                }
                catch (Exception ex2)
                {
                    CurrentException = ex2;
                }
                ProgramError();
            }
        }


        private void CloseSmtpConnection(Smtp smtp)
        {
            try
            {
                smtp.Disconnect();
                smtp.Dispose();
            }
            catch { }
        }
        private void SmtpWorking()
        {
            MailServerStatus status = new MailServerStatus();
            if (CurrentSmtpWorkingStep == SmtpWorkingStep.Connecting)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("正在连接邮局服务器{0}...", UserName);
            }
            else if (CurrentSmtpWorkingStep == SmtpWorkingStep.Connected)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("{0}连接成功", UserName);
            }
            else if (CurrentSmtpWorkingStep == SmtpWorkingStep.Sending)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("正在发送:{0}", CurrentSubject);
            }
            else if (CurrentSmtpWorkingStep == SmtpWorkingStep.DistributSending)
            {
                status.StatusCode = StatusCode.Progress;
                status.Message = string.Format("正在发送至:{0}", CurrentReceiver);
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            else if (CurrentSmtpWorkingStep == SmtpWorkingStep.End)
            {
                status.StatusCode = StatusCode.End;
                status.Message = string.Format("{0}发送成功", UserName);
            }
            if (!string.IsNullOrWhiteSpace(status.Message))
            {
                Console.WriteLine(Key + "\t" + status.ToString());
                SmtpSenderHost.StatusBags.Enqueue(Key, status);
            }
        }
        private void ProgramError()
        {
            MailServerStatus status = new MailServerStatus();
            if (CurrentSmtpWorkingStep == SmtpWorkingStep.DistributSending)
            {
                status.StatusCode = StatusCode.Progress;
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            else
            {
                status.StatusCode = StatusCode.Error;
            }
            if (CurrentException is MailBeeException)
            {
                status.Message = string.Format("{0}邮局返回错误：{1}", UserName, CurrentException.Message);
            }
            else
            {
                status.Message = string.Format("{0}系统出现错误:{1}", UserName, CurrentException.Message);
            }
            SmtpSenderHost.StatusBags.Enqueue(Key, status);
            Console.WriteLine(Key + "\t" + status.ToString());
            Logger.WriteError(CurrentException);
        }
        public abstract void SaveSendBegin(); //分别发送的和非分别发送的都会调用，整个邮件开始发送
        public abstract void SaveSendOK(); //分别发送的和非分别发送的都会调用，整个邮件彻底成功
        public abstract void SaveSendError(); //分别发送的和非分别发送的都会调用，发送邮件失败或者部分失败
        public abstract void SaveSendGroupOK();//针对分别发送调用，单封邮件成功
        public abstract void SaveSendGroupError();//针对分别发送调用，单封邮件失败

        public override void OnWorkError(Exception exception)
        {
            Logger.WriteError(exception);
            base.OnWorkError(exception);
        }

    }
}
