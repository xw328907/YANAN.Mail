using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 邮件过滤器执行动作类型
    /// </summary>
    public enum MailFilterEventTypeEnum
    {
        /// <summary>
        /// 移动到 邮箱文件夹
        /// </summary>
        [Description("移动到")]
        MoveToFolder = 1000,
        /// <summary>
        /// 打标签
        /// </summary>
        [Description("打标签")]
        SetLabel = 1100,
        /// <summary>
        /// 设置已读
        /// </summary>
        [Description("设置已读")]
        SetRead = 1200,
        /// <summary>
        /// 彻底删除
        /// </summary>
        [Description("彻底删除")]
        Deleted=1300
    }
}
