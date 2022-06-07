using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANAN.Mail.Utilities.Enums
{

    /// <summary>
    /// 邮件类型(收件，发件)
    /// </summary>
    public enum MailTypeEnum
    {
        /// <summary>
        /// 收件
        /// </summary>
        [Description("收件")]
        InBox = 1,
        /// <summary>
        /// 发件
        /// </summary>
        [Description("发件")]
        OutBox = 2

    }
}
