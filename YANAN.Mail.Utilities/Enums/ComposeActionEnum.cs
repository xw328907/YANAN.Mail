using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANAN.Mail.Utilities.Enums
{

    /// <summary>
    /// 写邮件操作类型枚举
    /// </summary>
    public enum ComposeActionEnum
    {
        /// <summary>
        /// 写邮件
        /// </summary>
        [Description("写邮件")]
        Write = 1,
        /// <summary>
        /// 回复
        /// </summary>
        [Description("回复")]
        Reply = 2,
        /// <summary>
        /// 回复全部
        /// </summary>
        [Description("回复全部")]
        ReplyAll = 3,
        /// <summary>
        /// 转发
        /// </summary>
        [Description("转发")]
        Forward = 4,
        /// <summary>
        /// 以附件转发
        /// </summary>
        [Description("以附件转发")]
        ForwardAsAttach = 5


    }
}
