using System;
using System.Collections.Generic;

namespace YANAN.Mail.Core
{
    using MailBee;
    using MailBee.Mime;
    using MailBee.Pop3Mail;
    using YANAN.Mail.Core.ThreadTask;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities.Enums;
    using System.Linq;

    public abstract class Pop3ReceiverBase : TaskBase
    {

        #region static
        private static Dictionary<string, bool> NewMailFromMaxs = new Dictionary<string, bool>(); //companycode+"_"+mailboxid 作为主键  true=邮箱序号越大越新
        /// <summary>
        /// 用于判断邮件顺序是从小到大还是从大到小
        /// </summary>
        /// <param name="pop3"></param>
        /// <param name="serverAddress"></param>
        /// <param name="remoteTotalCount"></param>
        /// <returns></returns>
        private static bool GetNewMailFromMax(Pop3 pop3, string serverAddress, int remoteTotalCount)
        {
            if (NewMailFromMaxs.ContainsKey(serverAddress))
            {
                return NewMailFromMaxs[serverAddress];
            }

            if (remoteTotalCount < 4) return true;//如果小于4封邮件则不知道顺序类型，返回默认的从最大开始，不要加入缓存，因为下次如果超过4封可以加入缓存
            if (!NewMailFromMaxs.ContainsKey(serverAddress))
            {
                lock (NewMailFromMaxs)
                {
                    if (!NewMailFromMaxs.ContainsKey(serverAddress))
                    {
                        MailMessage msgMax = pop3.DownloadMessageHeader(remoteTotalCount);//收下最大的一封
                        MailMessage msgOne = pop3.DownloadMessageHeader(1);//收下最小的一封 
                        NewMailFromMaxs.Add(serverAddress, msgMax.DateReceived > msgOne.DateReceived);
                    }
                }
            }
            return NewMailFromMaxs[serverAddress];
        }

        #endregion

        /// <summary>
        /// 定时运行的间隔，单位：分钟
        /// </summary>
        public int TimerMinutes { get; set; }

        public DateTime NextWorkTime { get; set; }

        #region 收邮件必须有的属性
        public bool IsImap { get; set; }
        public string ServerAddress { get; set; }
        public int Port { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱下所有非自定义邮箱列表(收件箱、发件箱等系统内置文件夹)
        /// </summary>
        public List<MailFolder> MailFolderList { get; set; }
        /// <summary>
        /// 已收取的邮件UID
        /// </summary>
        public HashSet<string> LatestSavedMessageUids { get; set; }
        /// <summary>
        /// 收取失败的邮件UID
        /// </summary>
        public Dictionary<string, int> LatestSavedErrorMessageUids { get; set; }
        /// <summary>
        /// 邮箱黑名单列表
        /// </summary>
        public HashSet<string> Blacklist { get; set; }
        /// <summary>
        /// 当前操作的文件夹(POP3为收件箱)
        /// </summary>
        public MailFolder MailFolderObject { get; set; }
        /// <summary>
        /// 默认收取邮件开始时间
        /// </summary>
        public DateTime? ReceiveDefaultTime { get; set; }

        #endregion 收邮件必须有的属性

        #region 每次执行收取都会变化的属性

        /// <summary>
        /// 最新一封邮件时间，用于保存邮箱文件夹最新邮件时间
        /// </summary>
        public DateTime? LastestMailTime { get; set; }
        /// <summary>
        /// 本次总邮件错误数
        /// </summary>
        public int TotalErrorCount { get; set; }
        /// <summary>
        /// 本次总共下载完成的数量
        /// </summary>
        public int TotalDownloadedCount { get; set; }
        /// <summary>
        /// 本次总下载邮件大小
        /// </summary>
        public long TotalDownloadedSize { get; set; }
        /// <summary>
        /// 本次总需下载邮件数
        /// </summary>
        public int TaskCount { get; private set; }
        /// <summary>
        /// 完成任务数(下载完成后+1，为了能够继续，下载失败也+1)
        /// </summary>
        public int CompletedCount { get; private set; }
        /// <summary>
        /// 当前进行的步骤
        /// </summary>
        public Pop3WorkingStep CurrentPop3WorkingStep { get; private set; }
        /// <summary>
        /// 当前下载邮件标题，单封邮件下载完成后才会设置
        /// </summary>
        public string CurrentSubject { get; private set; }
        /// <summary>
        /// 当前异常类
        /// </summary>
        public Exception CurrentException { get; private set; }
        #endregion 每次执行收取都会变化的属性

        /// <summary>
        /// 一封邮件下载错误次数，键是hashcode,值是出现次数
        /// </summary>
        private Dictionary<int, int> oneMailErrors = new Dictionary<int, int>();
        /// <summary>
        /// 收取单封邮件时如果mailbee错误大于这个数，就会中断，值=3
        /// </summary>
        private const int MAX_ONE_MAIL_ERROR_COUNT = 3;

        public Pop3ReceiverBase()
        {
            Port = 110;
            LatestSavedMessageUids = new HashSet<string>();
            Blacklist = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
        /// <summary>
        /// 执行邮件收件服务
        /// </summary>
        public override void DoWork()
        {
            DoWork_Pop3();
        }
        private void DoWork_Pop3()
        {
            TotalErrorCount = 0;
            TotalDownloadedCount = 0;
            TotalDownloadedSize = 0;
            TaskCount = 0;
            CompletedCount = 0;
            CurrentSubject = "";
            CurrentException = null;
            oneMailErrors = new Dictionary<int, int>();
            Pop3 pop3 = new Pop3();
            if (Logger.MailBeeLogTrack == true)
            {
                string path = Logger.MailBeeLogTrackFilePath;
                if (string.IsNullOrWhiteSpace(path))
                    path = Utilities.AssemblyHelper.GetBaseDirectory();
                path += "\\log_pop3\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                pop3.Log.Enabled = true;
                pop3.Log.Filename = path + Key + ".txt";//加上Key用来避免不同线程写日志冲突
                pop3.Log.Clear();
            }
            try
            {
                #region 属性校验
                CurrentPop3WorkingStep = Pop3WorkingStep.Prepare;
                Pop3Working();
                if (string.IsNullOrWhiteSpace(ServerAddress))
                {
                    throw new Exception("POP3地址不能为空");
                }
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    throw new Exception("登录邮局的用户名不能为空");
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("登录邮局的密码不能为空");
                }
                if (Port <= 0)
                {
                    throw new Exception(string.Format("错误的邮件端口号{0}", Port));
                }
                if (LatestSavedMessageUids == null)
                {
                    LatestSavedMessageUids = new HashSet<string>();
                    //throw new Exception("已收取列表不允许为空");
                }
                CurrentPop3WorkingStep = Pop3WorkingStep.Ready;
                Pop3Working();
                #endregion 属性校验

                #region 连接准备就绪
                CurrentPop3WorkingStep = Pop3WorkingStep.Connecting;
                Pop3Working();
                if (Port != 110)
                {
                    pop3.SslMode = MailBee.Security.SslStartupMode.OnConnect;
                    pop3.SslProtocol = MailBee.Security.SecurityProtocol.TlsAuto;
                }

                pop3.Connect(ServerAddress, Port);
                CurrentPop3WorkingStep = Pop3WorkingStep.Connected;
                Pop3Working();

                CurrentPop3WorkingStep = Pop3WorkingStep.Logining;
                Pop3Working();
                pop3.Login(UserName, Password);
                CurrentPop3WorkingStep = Pop3WorkingStep.Logined;
                Pop3Working();

                CurrentPop3WorkingStep = Pop3WorkingStep.GettingTotalCount;
                Pop3Working();
                int remoteTotalCount = pop3.InboxMessageCount;//会话期间从远程返回的邮件数量  

                CurrentPop3WorkingStep = Pop3WorkingStep.TotalCountGot;
                Pop3Working();

                //CurrentPop3WorkingStep = Pop3WorkingStep.GettingTotalSize;
                //Pop3Working();
                //long remoteTotalSize = pop3.InboxSize;//会话期间从远程返回的邮件总大小  
                //CurrentPop3WorkingStep = Pop3WorkingStep.TotalSizeGot;
                //Pop3Working();

                #endregion 连接准备就绪
                TaskCount = remoteTotalCount;//如果邮局总的邮件数小于需收取的 则全部收取
                if (remoteTotalCount >= MailFolderObject.MailCount)
                    TaskCount = remoteTotalCount - MailFolderObject.MailCount;
                if (!LastestMailTime.HasValue && ReceiveDefaultTime.HasValue)
                    LastestMailTime = ReceiveDefaultTime;
                //判断邮局邮件排序方式：true 倒序、false 顺序
                bool newMailFromMax = GetNewMailFromMax(pop3, ServerAddress + UserName, remoteTotalCount);
                int forword = newMailFromMax ? -1 : 1;
                //从前往后收
                int start = newMailFromMax ? remoteTotalCount : 1;
                int remain = TaskCount;//剩余条数
                CompletedCount = 0;
                while (remain > 0)
                {
                    if (start > 0)
                    {
                        string messageUid = pop3.GetMessageUidFromIndex(start);
                        //该邮件已经存在,已被收取,跳过 
                        if (!LatestSavedMessageUids.Contains(messageUid))
                        {
                            DownloadMessageAndSave(pop3, messageUid, start);//DownloadMessage方法内部不能抛出异常否则不会继续
                        }
                        else { CompletedCount++; }
                    }
                    start += forword;
                    remain -= 1;
                }

                //再次收取 错误的邮件
                if (LatestSavedErrorMessageUids != null && LatestSavedErrorMessageUids.Count > 0)
                {
                    TaskCount = LatestSavedErrorMessageUids.Count;
                    CompletedCount = 0;
                    foreach (string messageUid in LatestSavedErrorMessageUids.Keys)
                    {
                        if (LatestSavedMessageUids.Contains(messageUid))
                        {
                            CompletedCount++;
                            continue;
                        }
                        int index = pop3.GetMessageIndexFromUid(messageUid);
                        DownloadMessageAndSave(pop3, messageUid, index, false);//DownloadMessage方法内部不能抛出异常否则不会继续 
                    }
                }
                if (TotalDownloadedCount > 0)
                    SaveLatest(MailFolderObject, TotalDownloadedCount, TotalDownloadedSize);

                #region 根据保留天数KeepDays删除服务器上邮件
                HashSet<string> toDeleteMessageUids = LoadDeleteMessageUids();
                if (toDeleteMessageUids != null && toDeleteMessageUids.Count > 0)
                {
                    TaskCount = toDeleteMessageUids.Count;
                    CompletedCount = 0;
                    CurrentPop3WorkingStep = Pop3WorkingStep.BeginDeleting;
                    Pop3Working();
                    List<string> deletedMessageUids = new List<string>(toDeleteMessageUids.Count);
                    foreach (string messageUid in toDeleteMessageUids)
                    {
                        CurrentPop3WorkingStep = Pop3WorkingStep.OneMailDeleting;
                        Pop3Working();
                        try
                        {
                            int index = pop3.GetMessageIndexFromUid(messageUid);
                            if (index > 0) //如果为0表示不存在这份邮件
                            {
                                pop3.DeleteMessage(index);
                            }
                            deletedMessageUids.Add(messageUid);
                        }
                        catch (Exception ex)
                        {
                            //需要保存错误
                            CurrentException = ex;
                            ProgramError(messageUid);//内部要保存错误的messageUid防止重复删除
                        }
                        CompletedCount++;
                        CurrentPop3WorkingStep = Pop3WorkingStep.OneMailDeleted;
                        Pop3Working();
                    }
                    if (deletedMessageUids.Count > 0) //提高性能一次性保存
                    {
                        SaveDeleteMessages(deletedMessageUids);
                    }
                    CurrentPop3WorkingStep = Pop3WorkingStep.Deleted;
                    Pop3Working();
                }
                #endregion

                ClosePop3Connection(pop3);
                CurrentPop3WorkingStep = Pop3WorkingStep.End;
                Pop3Working();
            }
            catch (Exception ex)
            {
                ClosePop3Connection(pop3);
                CurrentPop3WorkingStep = Pop3WorkingStep.End;//异常也同样结束
                Pop3Working();
                CurrentException = ex;
                ProgramError(null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pop3"></param>
        /// <param name="messageUid"></param>
        /// <param name="index"></param>
        /// <param name="checkMailTime">是否按时间过滤邮件收取(此参数主要用于规避收取错误的邮件时跳过验证)</param>
        private void DownloadMessageAndSave(Pop3 pop3, string messageUid, int index, bool checkMailTime = true)
        {
            try
            {
                //先将ID加入成功队列
                CurrentPop3WorkingStep = Pop3WorkingStep.OneMailDownloading;
                Pop3Working();
                MailMessage msg = pop3.DownloadEntireMessage(index);
                if (!checkMailTime || !LastestMailTime.HasValue || msg.DateReceived >= LastestMailTime)//未收取过(未设置收取开始时间)或者邮件时间大于最后收取时间
                {
                    CurrentPop3WorkingStep = Pop3WorkingStep.OneMailDownloaded;
                    CurrentSubject = msg.Subject;
                    Pop3Working();
                    LatestSavedMessageUids.Add(messageUid);
                    if (Blacklist == null || !Blacklist.Contains(msg.From.Email))
                    {
                        MailFolderObject.MailCount += 1;
                        TotalDownloadedSize += msg.SizeOnServer;
                        TotalDownloadedCount++;
                        SaveDownloadedMessage(msg, messageUid);
                        CurrentPop3WorkingStep = Pop3WorkingStep.OneMailDownloadedAndSaved;
                        Pop3Working();
                    }
                    else
                    {
                        SaveRejectedMessage(messageUid, msg.DateReceived);
                        CurrentPop3WorkingStep = Pop3WorkingStep.OneMailRejected;
                        Pop3Working();
                    }
                }

            }
            catch (MailBeeException ex)
            {
                //如果是mailbee错误，则应该继续接收下一封，不过出错太多则应该终止  
                CollectOneMailMailBeeError(ex);//如果MailBee错误太多则优先抛出程序异常 
                CurrentException = ex;
                //下载失败也要将MessageID加入列表 
                SaveErrorMessage(messageUid);
                if (!LatestSavedErrorMessageUids.Keys.Contains(messageUid))
                {
                    LatestSavedErrorMessageUids.Add(messageUid, 1);
                }
                else
                {
                    LatestSavedErrorMessageUids[messageUid] = LatestSavedErrorMessageUids[messageUid] + 1;
                }
                if (LatestSavedErrorMessageUids[messageUid] > 3)
                    LatestSavedErrorMessageUids.Remove(messageUid);
                if (LatestSavedMessageUids.Contains(messageUid))
                {
                    LatestSavedMessageUids.Remove(messageUid);
                }
                TotalErrorCount++;
                ProgramError(messageUid);
            }
            CompletedCount += 1;
        }

        private void CollectOneMailMailBeeError(MailBeeException ex)
        {
            int hash = (ex.Message + "").GetHashCode();
            if (oneMailErrors.ContainsKey(hash))
            {
                oneMailErrors[hash] = oneMailErrors[hash] + 1;
            }
            else
            {
                oneMailErrors.Add(hash, 1);
            }
            if (oneMailErrors.Count > MAX_ONE_MAIL_ERROR_COUNT)
            {
                //如果MailBee错误太多则优先抛出程序异常
                throw new Exception(string.Format("连续{0}次出现收件错误：{1}", oneMailErrors.Count, ex.Message));
            }
        }

        private void ClosePop3Connection(Pop3 pop3)
        {
            try
            {
                if (pop3 != null)
                {
                    if (pop3.IsConnected)
                        pop3.Disconnect();
                    pop3.Dispose();
                }
            }
            catch { }
        }

        private void ProgramError(string messageUid) //单份邮件接收时messageUid才有效,返回加工过的messageUid
        {
            MailServerStatus status = new MailServerStatus();
            if (string.IsNullOrWhiteSpace(messageUid))
            {
                status.StatusCode = StatusCode.Error;
            }
            else
            {
                status.StatusCode = StatusCode.Progress;
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            if (CurrentException is MailBeeException)
            {
                status.Message = string.Format("{0}邮局返回错误：{1}", UserName, CurrentException.Message);
            }
            else
            {
                status.Message = string.Format("{0}系统出现错误，请联系系统管理员，错误详情为：{1}", UserName, CurrentException.Message);
            }
            Pop3ReceiverHost.StatusBags.Enqueue(Key, status);
            Console.WriteLine(Key + "\t" + status.ToString());
            //Mail.Core.Logger.WriteError(CurrentException);
        }
        private void Pop3Working()
        {
            MailServerStatus status = new MailServerStatus();
            if (CurrentPop3WorkingStep == Pop3WorkingStep.Connecting)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("{0}正在登录...", UserName);
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.Logined)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("{0}登录成功", UserName);
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.GettingTotalCount)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("{0}获取邮件总数", UserName);
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.TotalInfoGot)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = string.Format("{0}计算邮件收取信息完成", UserName);
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.OneMailRejected)
            {
                status.StatusCode = StatusCode.Progress;
                status.Message = string.Format("{0}拒收了邮件:{1}", UserName, CurrentSubject);
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.OneMailDownloadedAndSaved)
            {
                status.StatusCode = StatusCode.Progress;
                status.Message = string.Format("{0}收到新邮件:{1}", UserName, CurrentSubject);
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.BeginDeleting)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = "正在删除服务器上旧邮件...";
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.OneMailDeleted)
            {
                status.StatusCode = StatusCode.Progress;
                status.Message = "正在删除服务器上旧邮件...";
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.Deleted)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = "服务器上旧邮件删除完成";
            }
            else if (CurrentPop3WorkingStep == Pop3WorkingStep.End)
            {
                status.StatusCode = StatusCode.End;
                status.Message = string.Format("{0}收取完成", UserName);
                status.CompletedCount = TotalErrorCount;
                status.TaskCount = TotalDownloadedCount;
            }
            if (!string.IsNullOrWhiteSpace(status.Message))
            {
                Pop3ReceiverHost.StatusBags.Enqueue(Key, status);
                Console.WriteLine(DateTime.Now.ToString("MM-dd HH:mm:ss_fff") + " " + Key + "\t" + status.ToString());
            }
        }

        #region 必须重写的方法
        /// <summary>
        /// 保存最后一次接收的邮件数和容量
        /// </summary>
        /// <param name="savedTotalSize"></param>
        /// <param name="savedTotalCount"></param>
        public abstract void SaveLatest(MailFolder folder, int savedTotalCount, long savedTotalSize);
        //public abstract bool CheckMailMessageUIDExist(string messageUid);//邮箱初始化时全部读取
        /// <summary>
        /// 保存收下来的邮件
        /// </summary>
        /// <param name="mailMessage"></param>
        public abstract void SaveDownloadedMessage(MailMessage mailMessage, string messageUid);
        public abstract void SaveRejectedMessage(string messageUid, DateTime mailTime);
        /// <summary>
        /// 为提高性能批量保存
        /// </summary>
        /// <param name="deleteMessageUids"></param>
        public abstract void SaveDeleteMessages(List<string> deleteMessageUids);
        public abstract HashSet<string> LoadDeleteMessageUids();

        public abstract void SaveErrorMessage(string messageUid);

        #endregion 必须重写的方法

        public override void OnWorkError(Exception exception)
        {
            Logger.WriteError(exception);
            base.OnWorkError(exception);
        }
    }
}
