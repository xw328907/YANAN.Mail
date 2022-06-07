using System;
using System.Collections.Generic;
using System.Linq;

namespace YANAN.Mail.Core
{
    using MailBee;
    using MailBee.ImapMail;
    using MailBee.Mime;
    using MailBee.Security;
    using YANAN.Mail.Core.ThreadTask;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities.Enums;

    public abstract class ImapReceiverBase : TaskBase
    {
        public Imap imap;
        /// <summary>
        /// IMAP协议是否支持推送模式IDLE，true支持，false不支持
        /// </summary>
        public bool IsIdle { get; set; }
        public string MailServerAddress { get; set; }
        public int MailServerPort { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        /// <summary>
        /// 邮箱下所有邮箱列表(SourceTable=MailBox)
        /// </summary>
        public List<MailFolder> MailFolderList { get; set; }
        /// <summary>
        /// 默认收取邮件开始时间
        /// </summary>
        public DateTime? ReceiveDefaultTime { get; set; }
        /// <summary>
        /// 已收取的邮件UID
        /// </summary>
        public HashSet<string> LatestSavedMessageUids { get; set; } = new HashSet<string>();
        /// <summary>
        /// 收取失败的邮件UID
        /// </summary>
        public Dictionary<string, int> LatestSavedErrorMessageUids { get; set; } = new Dictionary<string, int>();
        /// <summary>
        /// 邮箱黑名单列表
        /// </summary>
        public HashSet<string> Blacklist { get; set; } = new HashSet<string>();
        /// <summary>
        /// 定时运行的间隔，单位：分钟
        /// </summary>
        public int TimerMinutes { get; set; }
        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime NextWorkTime { get; set; }
        /// <summary>
        /// 重连时间间隔，单位：分钟
        /// </summary>
        public const int ReConnectTimerMinutes = 20;
        /// <summary>
        /// 下次重连时间
        /// </summary>
        public DateTime ReConnectWorkTime { get; set; }

        #region 每次执行收取都会变化的属性
        /// <summary>
        /// 当前操作的文件夹对象
        /// </summary>
        public MailFolder CurrentMailFolder { get; set; }
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
        /// 总未读邮件数
        /// </summary>
        public int TotalUnReadCount { get; set; }
        /// <summary>
        /// 本次总下载邮件大小(单位:Byte字节)
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
        public ImapWorkingStep CurrentImapWorkingStep { get; private set; }
        /// <summary>
        /// 当前下载的邮件标题，单封邮件下载完成后才会设置
        /// </summary>
        public string CurrentSubject { get; private set; }
        /// <summary>
        /// 当前异常类
        /// </summary>
        public Exception CurrentException { get; private set; }

        #endregion 每次执行收取都会变化的属性

        public override void DoWork()
        {
            if (IsIdle)
                DoWorkIdle();
            else
                DoWorkPolling();
        }
        public async void DoWorkIdle()
        {
            if (imap == null || imap.IsAborted)
                imap = new Imap();
            ReConnectWorkTime = DateTime.Now;
            if (MailServerPort != 110)
            {
                imap.SslMode = SslStartupMode.OnConnect;
                imap.SslProtocol = SecurityProtocol.TlsAuto;
            }
            if (Logger.MailBeeLogTrack == true)
            {
                string path = Logger.MailBeeLogTrackFilePath;
                if (string.IsNullOrWhiteSpace(path))
                    path = Utilities.AssemblyHelper.GetBaseDirectory();
                path += @"\log_imap\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\";
                if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                imap.Log.Enabled = true;
                imap.Log.Filename = path + Key + ".txt";//加上Key用来避免不同线程写日志冲突
                imap.Log.Clear();
            }
            CurrentImapWorkingStep = ImapWorkingStep.Connecting;
            ImapWorking();
            try
            {
                await imap.ConnectAsync(MailServerAddress, MailServerPort);
                CurrentImapWorkingStep = ImapWorkingStep.Connected;
                ImapWorking();
                if (imap.GetExtension("IDLE") != null)
                {
                    CurrentImapWorkingStep = ImapWorkingStep.Logining;
                    ImapWorking();
                    await imap.LoginAsync(EmailAddress, EmailPassword);
                    CurrentImapWorkingStep = ImapWorkingStep.Logined;
                    ImapWorking();
                    imap.MessageStatus += new ImapMessageStatusEventHandler(ImapMessageStatusHandler);
                    imap.Idling += new ImapIdlingEventHandler(ImapIdlingHandler);
                    while (true)
                    {
                        CurrentImapWorkingStep = ImapWorkingStep.GettingFoldering;
                        ImapWorking();
                        FolderCollection folderCollection = await imap.DownloadFoldersAsync();
                        CurrentImapWorkingStep = ImapWorkingStep.GettingFolder;
                        ImapWorking();
                        if (folderCollection.Count > 0)
                        {
                            SaveMailFolder(folderCollection);
                            CurrentImapWorkingStep = ImapWorkingStep.GettingFolderSync;
                            ImapWorking();
                            foreach (Folder folder in folderCollection)
                            {
                                CurrentImapWorkingStep = ImapWorkingStep.GettingFolderMailIdle;
                                ImapWorking();
                                await imap.SelectFolderAsync(folder.Name);
                                await imap.IdleAsync();
                                try
                                {
                                    TotalDownloadedCount = 0;
                                    TotalDownloadedSize = 0;
                                    TotalErrorCount = 0;
                                    TotalUnReadCount = 0;
                                    CompletedCount = 0;
                                    string search = "ALL";
                                    CurrentMailFolder = MailFolderList.FirstOrDefault(x => x.ServerFullFolderName == folder.Name);
                                    if (CurrentMailFolder != null && CurrentMailFolder.LastMailTime.HasValue)
                                    {
                                        search = "SENTSINCE \"" + ImapUtils.GetImapDateString(CurrentMailFolder.LastMailTime.Value) + "\"";
                                    }
                                    else if (ReceiveDefaultTime.HasValue)
                                    {
                                        search = "SENTSINCE \"" + ImapUtils.GetImapDateString(ReceiveDefaultTime.Value) + "\"";
                                    }
                                    CurrentImapWorkingStep = ImapWorkingStep.GettingFolderMail;
                                    ImapWorking();
                                    UidCollection uids = (UidCollection)await imap.SearchAsync(true, search, null);
                                    if (uids != null && uids.Count > 0)
                                    {
                                        TaskCount = uids.Count;
                                        CurrentImapWorkingStep = ImapWorkingStep.TotalCountGot;
                                        ImapWorking();
                                        foreach (var uid in uids)
                                        {
                                            DownloadMail(imap, uid.ToString());
                                        }
                                        if (LastestMailTime.HasValue)
                                            CurrentMailFolder.LastMailTime = LastestMailTime;
                                        //if (TotalDownloadedCount > 0)
                                        //{
                                        //    SaveCompleted(TotalDownloadedCount, TotalDownloadedSize);
                                        //}
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CurrentException = ex;
                                    ProgramError();
                                }
                            }
                        }
                        if (LatestSavedErrorMessageUids.Count > 0)
                        {
                            var errorIds = LatestSavedErrorMessageUids.Keys;
                            foreach (string uid in errorIds)
                            {
                                var ids = uid.Split('_');
                                if (ids.Length < 2) continue;
                                CurrentMailFolder = MailFolderList.FirstOrDefault(x => x.MailFolderId == ids[0]);
                                DownloadMail(imap, ids[1]);
                            }
                        }
                        if (DateTime.Now.Subtract(ReConnectWorkTime) > new TimeSpan(0, ReConnectTimerMinutes, 0))
                        {
                            CurrentImapWorkingStep = ImapWorkingStep.TimeoutReConnect;
                            ImapWorking();
                            if (imap != null)
                            {
                                if (imap.IsConnected && !imap.IsAborted)
                                {
                                    imap.StopIdle();
                                    imap.Disconnect();
                                }
                                imap.Dispose();
                            }
                            DoWork();
                        }
                        CurrentImapWorkingStep = ImapWorkingStep.End;
                        ImapWorking();
                        NextWorkTime = DateTime.Now.AddMinutes(TimerMinutes);
                    }
                }
            }
            catch (MailBeeUserAbortException)
            {
                CurrentImapWorkingStep = ImapWorkingStep.AbortByUser;
                ImapWorking();
            }
            catch (Exception ex)
            {
                CurrentException = ex;
                ProgramError();
            }
        }
        public void DoWorkPolling()
        {
            imap = new Imap();
            if (MailServerPort != 110)
            {
                imap.SslMode = SslStartupMode.OnConnect;
                imap.SslProtocol = SecurityProtocol.TlsAuto;
            }
            if (Logger.MailBeeLogTrack == true)
            {
                string path = Logger.MailBeeLogTrackFilePath;
                if (string.IsNullOrWhiteSpace(path))
                    path = Utilities.AssemblyHelper.GetBaseDirectory();
                path += @"\log_imap\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\";
                if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                imap.Log.Enabled = true;
                imap.Log.Filename = path + Key + ".txt";//加上Key用来避免不同线程写日志冲突
                imap.Log.Clear();
            }
            CurrentImapWorkingStep = ImapWorkingStep.Connecting;
            ImapWorking();
            try
            {
                imap.Connect(MailServerAddress, MailServerPort);
                CurrentImapWorkingStep = ImapWorkingStep.Connected;
                ImapWorking();
                CurrentImapWorkingStep = ImapWorkingStep.Logining;
                ImapWorking();
                imap.Login(EmailAddress, EmailPassword);
                CurrentImapWorkingStep = ImapWorkingStep.Logined;
                ImapWorking();
                CurrentImapWorkingStep = ImapWorkingStep.GettingFoldering;
                ImapWorking();
                FolderCollection folderCollection = imap.DownloadFolders();
                CurrentImapWorkingStep = ImapWorkingStep.GettingFolder;
                ImapWorking();
                if (folderCollection.Count > 0)
                {
                    SaveMailFolder(folderCollection);
                    CurrentImapWorkingStep = ImapWorkingStep.GettingFolderSync;
                    ImapWorking();
                    foreach (Folder folder in folderCollection)
                    {
                        CurrentImapWorkingStep = ImapWorkingStep.GettingFolderMailIdle;
                        ImapWorking();
                        imap.SelectFolder(folder.Name);
                        try
                        {
                            TotalDownloadedCount = 0;
                            TotalDownloadedSize = 0;
                            TotalErrorCount = 0;
                            TotalUnReadCount = 0;
                            CompletedCount = 0;
                            string search = "ALL";
                            CurrentMailFolder = MailFolderList.FirstOrDefault(x => x.ServerFullFolderName == folder.Name);
                            if (CurrentMailFolder == null) continue;
                            if (CurrentMailFolder.LastMailTime.HasValue)
                            {
                                search = "SENTSINCE \"" + ImapUtils.GetImapDateString(CurrentMailFolder.LastMailTime.Value) + "\"";
                            }
                            else if (ReceiveDefaultTime.HasValue)
                            {
                                search = "SENTSINCE \"" + ImapUtils.GetImapDateString(ReceiveDefaultTime.Value) + "\"";
                            }
                            CurrentImapWorkingStep = ImapWorkingStep.GettingFolderMail;
                            ImapWorking();
                            UidCollection uids = (UidCollection)imap.Search(true, search, null);
                            if (uids != null && uids.Count > 0)
                            {
                                TaskCount = uids.Count;
                                CurrentImapWorkingStep = ImapWorkingStep.TotalCountGot;
                                ImapWorking();
                                foreach (var uid in uids)
                                {
                                    DownloadMail(imap, uid.ToString());
                                }
                                if (LastestMailTime.HasValue)
                                    CurrentMailFolder.LastMailTime = LastestMailTime;
                                //if (TotalDownloadedCount > 0)
                                //{
                                //    SaveCompleted(TotalDownloadedCount, TotalDownloadedSize);
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            CurrentException = ex;
                            ProgramError();
                        }
                    }
                }
                if (LatestSavedErrorMessageUids.Count > 0)
                {
                    foreach (string uid in LatestSavedErrorMessageUids.Keys)
                    {
                        var ids = uid.Split('_');
                        if (ids.Length < 2) continue;
                        CurrentMailFolder = MailFolderList.FirstOrDefault(x => x.MailFolderId == ids[0]);
                        DownloadMail(imap, ids[1]);
                    }
                }
                CurrentImapWorkingStep = ImapWorkingStep.End;
                ImapWorking();
            }
            catch (MailBeeUserAbortException)
            {
                CurrentImapWorkingStep = ImapWorkingStep.AbortByUser;
                ImapWorking();
            }
            catch (Exception ex)
            {
                CurrentException = ex;
                ProgramError();
            }
            finally
            {
                if (imap != null)
                {
                    if (imap.IsConnected) imap.Disconnect();
                    if (!imap.IsAborted) imap.Abort();
                    imap.Dispose();
                }
            }
        }

        private void ImapMessageStatusHandler(object sender, ImapMessageStatusEventArgs e)
        {
            // RECENT status means new messages have just arrived to IMAP account. Initiate stopping idle to proceed to downloading the messages
            if ("RECENT" == e.StatusID)
            {
                var imp = (Imap)sender;
                imp.StopIdle();
            }
        }
        private void ImapIdlingHandler(object sender, ImapIdlingEventArgs e)
        {
            // If the difference between start timer time and the current time >= timeout, initiate stopping idle
            //if (DateTime.Now.Subtract(NextWorkTime) >= new TimeSpan(0, TimerMinutes, 0))
            if (DateTime.Now >= NextWorkTime)
            {
                var imp = (Imap)sender;
                imp.StopIdle();
            }
        }
        public void DownloadMail(Imap imap, string messageUid)
        {
            string muid = CurrentMailFolder.MailFolderId + "_" + messageUid;
            if (LatestSavedMessageUids.Contains(muid)) { CompletedCount++; return; }
            try
            {
                LatestSavedMessageUids.Add(muid);//先将ID加入保存成功队列
                long id = Convert.ToInt64(messageUid);
                MailMessage msg = imap.DownloadEntireMessage(id, true);
                var msgid = msg.MessageID;//处理通过我方客户端发送的邮件，因为暂未与邮局同步邮件
                if (!string.IsNullOrWhiteSpace(msgid)) msgid = msgid.TrimStart('<').TrimEnd('>');
                if (CheckMailFromSystem(msgid, muid)) { CompletedCount++; return; }
                CurrentSubject = msg.Subject;
                //CurrentImapWorkingStep = ImapWorkingStep.OneMailDownloaded;
                //ImapWorking();
                if (Blacklist == null || !Blacklist.Contains(msg.From.Email))
                {
                    CurrentMailFolder.MailCount += 1;
                    TotalDownloadedSize += msg.SizeOnServer;
                    TotalDownloadedCount++;
                    SaveDownloadedMessage(msg, muid);
                    CurrentImapWorkingStep = ImapWorkingStep.OneMailDownloadedAndSaved;
                    ImapWorking();
                }
                else
                {
                    SaveRejectedMessage(muid, msg.DateReceived);
                    CurrentImapWorkingStep = ImapWorkingStep.OneMailRejected;
                    ImapWorking();
                }
            }
            catch (Exception ex)
            {
                if (!LatestSavedErrorMessageUids.Keys.Contains(muid))
                    LatestSavedErrorMessageUids.Add(muid, 1);
                else
                {
                    LatestSavedErrorMessageUids[muid] = LatestSavedErrorMessageUids[muid] + 1;
                }
                if (LatestSavedErrorMessageUids[muid] > 4)//重复下载错误超过3次则停止收取
                {
                    LatestSavedErrorMessageUids.Remove(muid);
                }
                if (LatestSavedMessageUids.Contains(muid))//如保存失败则将id移出成功队列
                    LatestSavedMessageUids.Remove(muid);
                SaveErrorMessage(muid);
                CurrentException = ex;
                TotalErrorCount++;
                ProgramError(muid);
            }
            CompletedCount += 1;
        }
        private void ImapWorking()
        {
            MailServerStatus status = new MailServerStatus();
            switch (CurrentImapWorkingStep)
            {
                case ImapWorkingStep.AbortByUser:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}终止收取邮箱", EmailAddress);
                    break;
                case ImapWorkingStep.BeginDeleting:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}正在删除邮局服务器邮件...", EmailAddress);
                    break;
                case ImapWorkingStep.Connected:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}连接邮局服务器成功", EmailAddress);
                    break;
                case ImapWorkingStep.Connecting:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}正在连接邮局服务器...", EmailAddress);
                    break;
                case ImapWorkingStep.Deleted:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}邮局服务器邮件删除完成", EmailAddress);
                    break;
                case ImapWorkingStep.End:
                    status.StatusCode = StatusCode.End;
                    status.Message = string.Format("{0}收取完成", EmailAddress);
                    status.CompletedCount = TotalErrorCount;
                    status.TaskCount = TotalDownloadedCount;
                    break;
                case ImapWorkingStep.GettingFolder:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}下载邮箱文件夹完成", EmailAddress);
                    break;
                case ImapWorkingStep.GettingFoldering:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}下载邮箱文件夹...", EmailAddress);
                    break;
                case ImapWorkingStep.GettingFolderMail:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}下载邮箱文件夹[{1}]邮件...", EmailAddress, CurrentMailFolder.FolderName);
                    break;
                case ImapWorkingStep.GettingFolderMailIdle:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}检查邮箱文件夹状态", EmailAddress);
                    break;
                case ImapWorkingStep.GettingFolderSync:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}邮箱文件夹同步完成", EmailAddress);
                    break;
                case ImapWorkingStep.Logined:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}登录成功", EmailAddress);
                    break;
                case ImapWorkingStep.Logining:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}正在登录...", EmailAddress);
                    break;
                case ImapWorkingStep.OneMailDeleted:
                    status.StatusCode = StatusCode.Progress;
                    status.Message = string.Format("{0}邮局服务器邮件[{1}]删除完成", EmailAddress, CurrentSubject);
                    status.CompletedCount = CompletedCount + 1;
                    status.TaskCount = TaskCount;
                    break;
                case ImapWorkingStep.OneMailDeleting:
                    status.StatusCode = StatusCode.Progress;
                    status.Message = string.Format("{0}正在删除邮局服务器邮件[{1}]", EmailAddress, CurrentSubject);
                    break;
                case ImapWorkingStep.OneMailDownloaded:
                    status.StatusCode = StatusCode.Progress;
                    status.Message = string.Format("{0}新邮件:{1} 下载完成", EmailAddress, CurrentSubject);
                    break;
                case ImapWorkingStep.OneMailDownloadedAndSaved:
                    status.StatusCode = StatusCode.Progress;
                    status.Message = string.Format("{0}新邮件:{1} 收取成功", EmailAddress, CurrentSubject);
                    status.CompletedCount = CompletedCount + 1;
                    status.TaskCount = TaskCount;
                    break;
                case ImapWorkingStep.OneMailRejected:
                    status.StatusCode = StatusCode.Progress;
                    status.Message = string.Format("{0}拒收了邮件:{1}", EmailAddress, CurrentSubject);
                    status.CompletedCount = CompletedCount + 1;
                    status.TaskCount = TaskCount;
                    break;
                case ImapWorkingStep.Prepare:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}邮箱信息开始校验", EmailAddress);
                    break;
                case ImapWorkingStep.Ready:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}邮箱信息校验完成", EmailAddress);
                    break;
                case ImapWorkingStep.TimeoutReConnect:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}邮局服务器邮件删除完成", EmailAddress);
                    break;
                case ImapWorkingStep.TotalCountGot:
                    status.StatusCode = StatusCode.Info;
                    status.Message = string.Format("{0}邮箱文件夹[{1}]获取邮件{2}封", EmailAddress, CurrentMailFolder.FolderName, TaskCount);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(status.Message))
            {
                ImapReceiverHost.StatusBags.Enqueue(Key, status);
                Console.WriteLine(DateTime.Now.ToString("MM-dd HH:mm:ss_fff") + " " + Key + "\t" + status.ToString());
            }
        }
        private void ProgramError(string messageUid = "")
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
                status.Message = string.Format("{0}邮局返回错误：{1}", EmailAddress, CurrentException.Message);
            }
            else
            {
                status.Message = string.Format("{0}系统出现错误，请联系系统管理员，错误详情为：{1}", EmailAddress, CurrentException.Message);
            }
            ImapReceiverHost.StatusBags.Enqueue(Key, status);
            Console.WriteLine(DateTime.Now.ToString() + " " + Key + "\t" + status.ToString());
            Logger.WriteError(CurrentException);
        }
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="exception"></param>
        public override void OnWorkError(Exception exception)
        {
            Logger.WriteError(exception);
            base.OnWorkError(exception);
        }

        /// <summary>
        /// 保存邮件
        /// </summary>
        /// <param name="mailMessage"></param>
        public abstract void SaveDownloadedMessage(MailMessage mailMessage, string messageUid);
        /// <summary>
        /// 保存拒收邮件
        /// </summary>
        /// <param name="messageUid"></param>
        /// <param name="mailTime"></param>
        public abstract void SaveRejectedMessage(string messageUid, DateTime mailTime);
        /// <summary>
        /// 保存邮箱文件夹
        /// </summary>
        /// <param name="folderCollection"></param>
        public abstract void SaveMailFolder(FolderCollection folderCollection);
        /// <summary>
        /// 保存收取失败的邮件
        /// </summary>
        /// <param name="messageUid"></param>
        public abstract void SaveErrorMessage(string messageUid);
        /// <summary>
        /// 邮件收取保存成功执行
        /// </summary>
        public abstract void SaveCompleted(int savedTotalCount, long savedTotalSize);
        /// <summary>
        /// 检查邮件是否已存在本地,本地发送出去的,如存在返回true则不应继续保存当前邮件
        /// </summary>
        /// <param name="messageUID">邮件MessageUid</param>
        /// <param name="msgUid">邮件在邮局中唯一UID</param>
        public abstract bool CheckMailFromSystem(string messageUID, string msgUid);
    }
}
