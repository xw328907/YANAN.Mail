using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Core
{
    using YANAN.Mail.Entity;

    public abstract class ImapReceiverLoaderBase
    {
        /// <summary>
        /// 加载邮箱信息
        /// </summary>
        /// <param name="ocode">公司编号</param>
        /// <param name="mailBoxId">邮箱ID</param>
        /// <returns></returns>
        public abstract ImapReceiverBase LoadMailBox(string ocode, string mailBoxId);
        /// <summary>
        /// 加载所有IMAP协议的邮箱ID
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, List<string>> LoadAllMailBoxIds();//键是ocode,值是MailBoxId列表
    }
}
