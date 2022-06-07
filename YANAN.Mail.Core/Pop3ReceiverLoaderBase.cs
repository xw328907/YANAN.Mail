using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YANAN.Mail.Core.ThreadTask;

namespace YANAN.Mail.Core
{
    public abstract class Pop3ReceiverLoaderBase
    {

        public abstract Pop3ReceiverBase LoadMailBox(string companyCode,string mailBoxId);
        public abstract Dictionary<string, List<string>> LoadAllMailBoxIds();//键是ocode,值是MailBoxId列表

        
    }
}
