using System;
using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 邮件优先级枚举
    /// </summary>
    public enum MailPriorityEnum
    {
        /// <summary>
        /// 无,未设置
        /// </summary>
        [Description("无")]
        None = 0,
        /// <summary>
        /// 最高
        /// </summary>
        [Description("最高")]
        ighest = 1,
        /// <summary>
        /// 高
        /// </summary>
        [Description("高")]
        High = 2,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 3,
        /// <summary>
        /// 低
        /// </summary>
        [Description("低")]
        Low = 4,
        /// <summary>
        /// 最低
        /// </summary>
        [Description("最低")]
        Lowest = 5
    }
}
