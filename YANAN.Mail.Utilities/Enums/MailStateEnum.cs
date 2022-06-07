using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{

    /// <summary>
    /// 邮件状态枚举
    /// </summary>
    public enum MailStateEnum
    {
        /// <summary>
        /// 草稿,初始默认值
        /// </summary>
        [Description("草稿")]
        DRAFT,
        /// <summary>
        /// 待发送
        /// </summary>
        [Description("待发送")]
        WAIT_SEND,
        /// <summary>
        /// 发送中
        /// </summary>
        [Description("发送中")]
        SENDING,
        /// <summary>
        /// 发送失败
        /// </summary>
        [Description("发送失败")]
        SEND_FAIL,
        /// <summary>
        /// 发送失败,不再发送
        /// </summary>
        [Description("发送失败,不再发送")]
        SEND_FAIL_END,
        /// <summary>
        /// 发送(收取)成功
        /// </summary>
        [Description("发送成功")]
        SEND_SUCCESS

    }
    /// <summary>
    /// 邮件发送类型;写邮件、回复、转发
    /// </summary>
    public enum MailSendAction
    {
        /// <summary>
        /// 写邮件
        /// </summary>
        [Description("写邮件")]
        Write,
        /// <summary>
        /// 回复
        /// </summary>
        [Description("回复")]
        Reply,
        /// <summary>
        /// 回复全部
        /// </summary>
        [Description("回复全部")]
        ReplyAll,
        /// <summary>
        /// 转发
        /// </summary>
        [Description("转发")]
        Forward,
        /// <summary>
        /// 以附件转发
        /// </summary>
        [Description("以附件转发")]
        ForwardAttach
    }
}
