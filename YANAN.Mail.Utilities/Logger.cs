using System;
using System.IO;

namespace YANAN.Mail.Utilities
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger
    {
        private const string SOURCE = "YANAN.Mail";
        private const string LOG = "Yanan";
        private const int SIZE = 64000;//以k为单位

        /// <summary>
        /// windows 错误事件日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteError(Exception ex)
        {
            Write(ex.ToString());
            //if (!EventLog.SourceExists(SOURCE))
            //    EventLog.CreateEventSource(SOURCE, LOG);
            //EventLog log = new EventLog(LOG);
            //if (log.OverflowAction != OverflowAction.OverwriteAsNeeded || log.MaximumKilobytes != SIZE)
            //{
            //    log.MaximumKilobytes = SIZE;
            //    log.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 0);
            //}
            //if (ex != null)
            //{
            //    EventLog.WriteEntry(SOURCE, ex.ToString());
            //}
        }

        /// <summary>
        /// 写日志，以每天为单位自动生成文件，日志文件名前缀由Prefix参数指定
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="logFileNamePrefix">文件名前缀</param>
        /// <param name="logFolder">日志的目录名</param>
        private static void Write(string text, string logFileNamePrefix, string logFolder)
        {
            string path = logFolder;
            if (string.IsNullOrEmpty(path))
            {
                path = string.Empty;//Utilities.AppConfigHelper.GetAppSetting(Utilities.ConstConfig.LogPath_AppSettingKey);
                if (string.IsNullOrEmpty(path))
                {
                    path = DefaultLogFolder + "/Log/Client";
                }
            }
            string file;
            //如果目录不存在，创建这个目录
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    return;
                }
            }

            //根据时间生成日志文件名，此时不包含扩展名
            file = "log_" + DateTime.Today.ToString("yyyy-MM-dd");

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    //生成全路径
                    string stri = i == 0 ? "" : "_" + i.ToString();
                    string FileNamePrefix2 = string.IsNullOrEmpty(logFileNamePrefix) ? "" : logFileNamePrefix + "_";
                    string fullFile = Path.GetFullPath(Path.Combine(path, FileNamePrefix2 + file + stri + ".txt"));
                    StreamWriter f = new StreamWriter(fullFile, true, System.Text.Encoding.GetEncoding("gb2312"));
                    f.WriteLine(DateTime.Now.ToString() + "\t" + text);
                    f.Close();
                    break;
                }
                catch
                {
                }
            }

        }
        /// <summary>
        /// 写日志，以每天为单位自动生成文件，日志文件名前缀由Prefix参数指定
        /// </summary>
        /// <param name="text">日志文本内容</param>
        /// <param name="logFolder">文件夹名称，如果为空，则使用默认目录</param> 
        public static void Write(string text, string logFolder)
        {
            Write(text, "", logFolder);
        }

        /// <summary>
        /// 写日志，日志目录由LogFolder配置参数决定，前缀由LogFileNamePrefix决定
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            Write(text, "", "");
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultLogFolder
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

    }
}
