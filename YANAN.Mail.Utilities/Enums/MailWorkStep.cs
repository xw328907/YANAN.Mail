using System.Runtime.Serialization;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 过滤步骤
    /// </summary>
    [DataContract]
    public enum FilterStep
    {
        /// <summary>
        /// 开始准备，属性校验，还没开始连接
        /// </summary>
        [EnumMember]
        Prepare,
        /// <summary>
        /// 开始连接到服务器
        /// </summary>
        [EnumMember]
        Filting,
        /// <summary>
        /// 结束
        /// </summary>
        [EnumMember]
        End,
    }
    /// <summary>
    /// POP3(收取)任务步骤
    /// </summary>
    [DataContract]
    public enum Pop3WorkingStep
    {
        /// <summary>
        /// 开始准备，属性校验，还没开始连接
        /// </summary>
        [EnumMember]
        Prepare,

        /// <summary>
        /// 准备完成
        /// </summary>
        [EnumMember]
        Ready,

        /// <summary>
        /// 开始连接到服务器
        /// </summary>
        [EnumMember]
        Connecting,

        /// <summary>
        /// 连接服务器成功
        /// </summary>
        [EnumMember]
        Connected,

        /// <summary>
        /// 开始登陆
        /// </summary>
        [EnumMember]
        Logining,

        /// <summary>
        /// 登录成功
        /// </summary>
        [EnumMember]
        Logined,

        /// <summary>
        /// 获取邮局邮件总数
        /// </summary>
        [EnumMember]
        GettingTotalCount,

        /// <summary>
        /// 获取邮局邮件总数完成
        /// </summary>
        [EnumMember]
        TotalCountGot,

        /// <summary>
        /// 获取邮局邮件总大小
        /// </summary>
        [EnumMember]
        GettingTotalSize,

        /// <summary>
        /// 获取邮局邮件总大小完成
        /// </summary>
        [EnumMember]
        TotalSizeGot,

        /// <summary>
        /// 计算邮件收取信息完成
        /// </summary>
        [EnumMember]
        TotalInfoGot,

        /// <summary>
        /// 开始下载单封邮件
        /// </summary>
        [EnumMember]
        OneMailDownloading,

        /// <summary>
        /// 单封邮件下载完成
        /// </summary>
        [EnumMember]
        OneMailDownloaded,

        /// <summary>
        /// 单封邮件下载完成并保存成功
        /// </summary>
        [EnumMember]
        OneMailDownloadedAndSaved,

        /// <summary>
        /// 单封邮件黑名单被拒绝
        /// </summary>
        [EnumMember]
        OneMailRejected,

        /// <summary>
        /// 开始删除邮局服务器上的邮件
        /// </summary>
        [EnumMember]
        BeginDeleting,


        /// <summary>
        /// 开始删除邮局服务器上单封邮件
        /// </summary>
        [EnumMember]
        OneMailDeleting,

        /// <summary>
        /// 邮局服务器上单封邮件删除完成
        /// </summary>
        [EnumMember]
        OneMailDeleted,

        /// <summary>
        /// 删除完成
        /// </summary>
        [EnumMember]
        Deleted,

        /// <summary>
        /// 结束/完成
        /// </summary>
        [EnumMember]
        End,
    }
    /// <summary>
    /// IMAP(收取)任务步骤
    /// </summary>
    [DataContract]
    public enum ImapWorkingStep
    {
        /// <summary>
        /// 开始准备，属性校验，还没开始连接
        /// </summary>
        [EnumMember]
        Prepare,

        /// <summary>
        /// 准备完成
        /// </summary>
        [EnumMember]
        Ready,
        /// <summary>
        /// 用户终止
        /// </summary>
        [EnumMember]
        AbortByUser,
        /// <summary>
        /// 开始连接到服务器
        /// </summary>
        [EnumMember]
        Connecting,

        /// <summary>
        /// 连接服务器成功
        /// </summary>
        [EnumMember]
        Connected,

        /// <summary>
        /// 开始登陆
        /// </summary>
        [EnumMember]
        Logining,

        /// <summary>
        /// 登录成功
        /// </summary>
        [EnumMember]
        Logined,

        /// <summary>
        /// 获取邮箱文件夹
        /// </summary>
        [EnumMember]
        GettingFoldering,
        /// <summary>
        /// 获取邮箱文件夹完成
        /// </summary>
        [EnumMember]
        GettingFolder,
        /// <summary>
        /// 邮箱文件夹同步完成
        /// </summary>
        [EnumMember]
        GettingFolderSync,
        /// <summary>
        /// 监听邮箱文件夹邮件
        /// </summary>
        [EnumMember]
        GettingFolderMailIdle,
        /// <summary>
        /// 获取邮箱文件夹邮件
        /// </summary>
        [EnumMember]
        GettingFolderMail,
        /// <summary>
        /// 获取邮局文件夹内邮件总数完成
        /// </summary>
        [EnumMember]
        TotalCountGot,

        /// <summary>
        /// 单封邮件下载完成
        /// </summary>
        [EnumMember]
        OneMailDownloaded,

        /// <summary>
        /// 单封邮件下载完成并保存成功
        /// </summary>
        [EnumMember]
        OneMailDownloadedAndSaved,

        /// <summary>
        /// 单封邮件黑名单被拒绝
        /// </summary>
        [EnumMember]
        OneMailRejected,

        /// <summary>
        /// 开始删除邮局服务器上的邮件
        /// </summary>
        [EnumMember]
        BeginDeleting,


        /// <summary>
        /// 开始删除邮局服务器上单封邮件
        /// </summary>
        [EnumMember]
        OneMailDeleting,

        /// <summary>
        /// 邮局服务器上单封邮件删除完成
        /// </summary>
        [EnumMember]
        OneMailDeleted,

        /// <summary>
        /// 删除完成
        /// </summary>
        [EnumMember]
        Deleted,
        /// <summary>
        /// 超时重连
        /// </summary>
        [EnumMember]
        TimeoutReConnect,
        /// <summary>
        /// 结束/完成
        /// </summary>
        [EnumMember]
        End,
    }
    /// <summary>
    /// SMTP(发送)任务步骤
    /// </summary>
    [DataContract]
    public enum SmtpWorkingStep
    {
        /// <summary>
        /// 开始准备，属性校验，还没开始连接
        /// </summary>
        [EnumMember]
        Prepare,

        /// <summary>
        /// 准备完成
        /// </summary>
        [EnumMember]
        Ready,

        /// <summary>
        /// 连接到服务器时的错误
        /// </summary>
        [EnumMember]
        Connecting,

        /// <summary>
        /// 服务器连接成功
        /// </summary>
        [EnumMember]
        Connected,

        /// <summary>
        ///  发送中
        /// </summary>
        [EnumMember]
        Sending,

        /// <summary>
        /// 已发送
        /// </summary>
        [EnumMember]
        Sended,

        /// <summary>
        ///  分别发送中
        /// </summary>
        [EnumMember]
        DistributSending,

        /// <summary>
        /// 分别发送完成
        /// </summary>
        [EnumMember]
        DistributSended,

        /// <summary>
        /// 结束/完成
        /// </summary>
        [EnumMember]
        End,
    }
}
