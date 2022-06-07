using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace YANAN.Mail.Core.WindowsService
{
    public class ServiceStartInfo
    {
        /// <summary>
        /// 如果为空，则使用ServiceBase的ServiceName
        /// </summary>
        public string ServiceName { get; set; }

        private string _DisplayName;
        public string DisplayName
        {
            get { return string.IsNullOrEmpty(_DisplayName) ? ServiceName : _DisplayName; }
            set { _DisplayName = value; }
        }
        public string Description { get; set; }
        public ServiceAccount Account { get; set; }
        public ServiceStartMode StartType { get; set; }
        /// <summary>
        /// 指定服务运行账户，只有Account属性为User时才会使用该属性
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 指定服务运行账户的密码，只有Account属性为User时才会使用该属性
        /// </summary>
        public string AccountPassword { get; set; }


        public string Dependencies { get; set; }

        public ServiceStartInfo()
        {
            Account = ServiceAccount.LocalSystem;
            StartType = ServiceStartMode.Automatic;
        }

        #region for install
        internal string ServiceStartAccountName
        {
            get
            {
                switch (Account)
                {
                    case ServiceAccount.LocalSystem:
                        return null;
                    case ServiceAccount.LocalService:
                        return @"NT AUTHORITY\LocalService";
                    case ServiceAccount.NetworkService:
                        return @"NT AUTHORITY\NetworkService";
                    default:
                        return this.AccountName;
                }
            }
        }
        internal string ServiceStartAccountPassword
        {
            get
            {
                switch (Account)
                {
                    case ServiceAccount.User:
                        return AccountPassword;
                    default:
                        return null;
                }
            }
        }

        internal int ServiceStartType
        {
            get
            {
                int SERVICE_AUTO_START = 0x00000002;
                int SERVICE_DEMAND_START = 0x00000003;
                int SERVICE_DISABLED = 0x00000004;
                switch (StartType)
                {
                    case ServiceStartMode.Automatic:
                        return SERVICE_AUTO_START;
                    case ServiceStartMode.Disabled:
                        return SERVICE_DISABLED;
                    default://manu
                        return SERVICE_DEMAND_START;
                }
            }
        }
        #endregion
    }
}
