using System;

namespace YANAN.Mail.Entity
{
    using YANAN.Mail.Utilities.Enums;

    public class MailSearchDto
    {
        /// <summary>
        /// 邮箱ID编号
        /// </summary>
        public string MailBoxId { get; set; }
        /// <summary>
        /// 邮箱文件夹类型（邮箱、客户、标签）
        /// </summary>
        public string MailFolderSourceTableCode { get; set; } = MailFolderSourceTableEnum.MailBox.ToString();
        /// <summary>
        /// 邮箱文件夹ID编号
        /// </summary>
        public string MailFolderId { get; set; }
        /// <summary>
        /// 邮箱文件夹类型（收件箱、发件箱等）
        /// </summary>
        public int MailFolderTypeCode { get; set; }
        /// <summary>
        /// 标签邮件
        /// </summary>
        public bool? IsLabelMail { get; set; }
        /// <summary>
        /// 邮件标签ID编号
        /// </summary>
        public string MailLabelId { get; set; }
        /// <summary>
        /// 未读邮件
        /// </summary>
        public bool? UnRead { get; set; }
        /// <summary>
        /// 置顶邮件
        /// </summary>
        public bool? IsTopMail { get; set; }
        /// <summary>
        /// 搜索关键词(匹配主题、正文等)
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string MailBody { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 是否有附件
        /// </summary>
        public bool? HasAttach { get; set; }
        /// <summary>
        /// 已删除的邮件 true 
        /// </summary>
        public bool? Deleted { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndTime { get; set; }

    }
}
