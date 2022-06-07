using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 邮箱设置tab页枚举
    /// </summary>
    public enum MailSettingTabPageEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default = -1,
        /// <summary>
        /// 邮箱设置
        /// </summary>
        [Description("邮箱设置")]
        MailBox = 0,
        /// <summary>
        /// 签名
        /// </summary>
        [Description("签名")]
        Signature = 1,
        /// <summary>
        /// 模板
        /// </summary>
        [Description("模板")]
        Template = 2,
        /// <summary>
        /// 过滤器
        /// </summary>
        [Description("过滤器")]
        Filter = 3,
        /// <summary>
        /// 标签
        /// </summary>
        [Description("标签")]
        Label = 4,
        /// <summary>
        /// 黑名单，拒收
        /// </summary>
        [Description("黑名单")]
        Judge = 5
    }
}
