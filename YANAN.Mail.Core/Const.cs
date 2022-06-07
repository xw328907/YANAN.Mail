using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Core
{
    public static class Const
    {
        /// <summary>
        /// 收邮件的线程数
        /// </summary>
        public const int ReceiveThreadCount = 1;//收邮件的线程数
        /// <summary>
        /// 收邮件间隔时间，单位毫秒;
        /// 注意：不是MailBox中的ReceiveTimer，否则如果MailBox.ReceiveTimer很小的时候就无法正确触发 
        /// </summary>
        public const int ReceiveTimer = 120000;
        /// <summary>
        /// 发邮件的线程数
        /// </summary>
        public const int SendThreadCount = 1;//发邮件的线程数
        /// <summary>
        /// 发邮件间隔时间，单位毫秒;
        /// </summary>
        public const int SendTimer = 6000;
        /// <summary>
        /// 索引线程数
        /// </summary>
        public const int IndexThreadCount = 1;//索引线程数 
        /// <summary>
        /// 索引间隔时间，单位毫秒;
        /// </summary>
        public const int IndexTimer = 120000;
        /// <summary>
        /// 过滤线程数
        /// </summary>
        public const int FilterThreadCount = 3;//过滤线程数 
        /// <summary>
        /// 邮件服务名称 Key=ServiceName
        /// </summary>
        public const string MailServerName = "ServiceName";
        /// <summary>
        /// 邮件服务端口 Key=ServicePort
        /// </summary>
        public const string MailServerPort = "ServicePort";
        /// <summary>
        /// 站点根目录 key
        /// </summary>
        public const string WebSiteBaseDir = "WebSiteBaseDir";
    }
}
