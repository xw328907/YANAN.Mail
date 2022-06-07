using System;
using System.Collections.Generic;

namespace YANAN.Mail.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MailMain
    {
        /// <summary>
        /// 邮件大小(转换后)，如：9.8K
        /// </summary>
        public string MailSizeString { get { return Utilities.UtilityHelper.ConvertFileSize(MailSize); } }

        /// <summary>
        /// 邮件所属文件夹ID编号
        /// </summary>
        public string MailFolderId { get; set; }
        //public string MailStateText
        //{
        //    get
        //    {
        //        string val = string.Empty;
        //        if (Enum.TryParse(MailState, out MailStateEnum stateEnum))
        //        {
        //            val = stateEnum.GetDescription();
        //        }
        //        return val;
        //    }
        //}
        /// <summary>
        /// 邮件所属文件夹类型
        /// </summary>
        public int MailFolderType { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiveName
        {
            get
            {
                return Utilities.UtilityHelper.GetMailReceiveCcName(Receiver);
            }
        }
        /// <summary>
        /// 发件人
        /// </summary>
        public string SendName
        {
            get
            {
                return Utilities.UtilityHelper.GetMailReceiveCcName(Sender);
            }
        }

        /// <summary>
        /// 附件集合
        /// </summary>
        public List<MailAttach> MailAttachs { get; set; } = new List<MailAttach>();

        /// <summary>
        /// 收件人/抄送人集合
        /// </summary>
        public List<MailGroup> MailGroups { get; set; } = new List<MailGroup>();

    }
    public partial class MailJudge
    {
        /// <summary>
        /// 所属邮箱地址
        /// </summary>
        public string MailBoxAddress
        {
            get
            {
                if (MailBox != null && !string.IsNullOrWhiteSpace(MailBox.MailAddress))
                    return MailBox.MailAddress;
                return string.Empty;
            }
        }
    }

    public partial class MailContact
    {
        /// <summary>
        /// 联系人(格式化后,联系人+邮箱地址)，用于联系人列表
        /// </summary>
        public string Linkman
        {
            get
            {
                string nl = Environment.NewLine;
                return (ContactName ?? string.Empty) + nl + "    "+EMail;
            }
        }
    }
}
