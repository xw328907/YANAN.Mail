using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 邮件附件显示右键菜单模式
    /// </summary>
    public enum MaiAttachContextMenuModeEnum
    {
        /// <summary>
        /// 邮件查看页
        /// </summary>
        [Description("邮件查看")]
        View = 1,
        /// <summary>
        /// 邮件编辑(发送)页
        /// </summary>
        [Description("邮件编辑")]
        Edit = 2
    }
}
