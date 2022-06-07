using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{

    /// <summary>
    /// 邮件文件夹枚举，内置文件夹(收件箱，发件箱等),存储的为int值
    /// </summary>
    public enum MailFolderEnum
    {
        /// <summary>
        /// 收件箱
        /// </summary>
        [Description("收件箱")]
        InBox = 99,
        /// <summary>
        /// 发件箱
        /// </summary>
        [Description("发件箱")]
        OutBox = 97,
        /// <summary>
        /// 草稿箱
        /// </summary>
        [Description("草稿箱")]
        DraftBox = 95,
        /// <summary>
        /// 垃圾箱
        /// </summary>
        [Description("垃圾箱")]
        TrashBox = 93,
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = 91,
        /// <summary>
        /// 自定义文件夹
        /// </summary>
        [Description("自定义文件夹")]
        Customize = 0

    }
    /// <summary>
    /// 邮箱文件夹所属表枚举
    /// </summary>
    public enum MailFolderSourceTableEnum
    {
        /// <summary>
        /// 邮箱文件夹
        /// </summary>
        [Description("邮箱文件夹")]
        MailBox,
        /// <summary>
        /// 邮件标签
        /// </summary>
        [Description("标签")]
        MailLabel,
        /// <summary>
        /// 客户
        /// </summary>
        [Description("客户")]
        Customer,
    }
    /// <summary>
    /// 邮箱文件夹树节点类型(主要用于区分是邮箱还是邮箱文件夹)
    /// </summary>
    public enum MailBoxFolderTreeNodeTypeEnum
    {
        /// <summary>
        /// 当前节点为邮箱
        /// </summary>
        [Description("邮箱")]
        MailBox,
        /// <summary>
        /// 当前节点为邮箱文件夹
        /// </summary>
        [Description("邮箱文件夹")]
        MailFolder,
        /// <summary>
        /// 邮件标签
        /// </summary>
        [Description("邮件标签")]
        MailLabel,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other
    }
    /// <summary>
    /// 邮箱全局自定义文件夹ID枚举(如：全部未读邮件)
    /// </summary>
    public enum MailFolderCustomIdEnum
    {
        /// <summary>
        /// 所有未读邮件
        /// </summary>
        [Description("所有未读邮件")]
        all_noread_mail,
    }
}
