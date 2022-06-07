using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Core
{
    public abstract class SmtpSenderLoaderBase
    {
        public abstract Dictionary<string, List<string>> LoadAllSendTasks();
        public abstract List<SmtpSenderBase> LoadSendTasks(string companyCode, string[] mailIds);
    }
}
