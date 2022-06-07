using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANAN.Mail.Utilities
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 得到当前应用程序的Bin根目录不包括\
        /// </summary>
        /// <returns></returns>
        public static string GetBaseDirectoryBin()
        {
            var baseDirectory = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            if (AppDomain.CurrentDomain.SetupInformation.PrivateBinPath == null)
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return baseDirectory;
        }
        /// <summary>
        /// 得到当前应用程序的根目录
        /// </summary>
        /// <returns></returns>
        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
