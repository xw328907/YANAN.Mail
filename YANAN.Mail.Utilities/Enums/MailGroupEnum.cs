using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{

    /// <summary>
    /// 邮件收件人类型，MailGroup.ReceiveTypeCode
    /// </summary>
    public enum MailGroupReceiveTypeEnum
    {
        /// <summary>
        /// 收件人
        /// </summary>
        [Description("收件人")]
        Receiver = 1,
        /// <summary>
        /// 抄送
        /// </summary>
        [Description("抄送")]
        Cc = 2,
        /// <summary>
        /// 密送
        /// </summary>
        [Description("密送")]
        Bcc = 3

    }

}
