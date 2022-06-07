using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += Application_ThreadException;
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AutoMapperConfig.Initialize();
            //Mutex mutex = new Mutex(false, "ThisShouldOnlyRunOnce");
            //bool Running = !mutex.WaitOne(0, false);
            //if (!Running)
            {
                Services.DatabaseVersionScript databaseVersion = new Services.DatabaseVersionScript();
                bool flag = databaseVersion.InitDatabase();
                Dictionary<string, string> cmdArgs = new Dictionary<string, string>();
                if (args.Length > 0 && CurrentUserInfo.IsLogined == false)
                {
                    Regex cmdRegEx = new Regex(@"(?<name>.+?):(?<val>.+)");
                    foreach (string s in args)
                    {
                        Match m = cmdRegEx.Match(s);
                        if (m.Success)
                        {
                            cmdArgs.Add(m.Groups[1].Value, m.Groups[2].Value);
                        }
                    }
                    if (cmdArgs.Count > 0 && cmdArgs.ContainsKey("uid") && cmdArgs.ContainsKey("ocode"))
                    {
                        LoginForm.Login(new Entity.LoginUserInfo
                        {
                            OCode = cmdArgs["ocode"],
                            UserId = cmdArgs["uid"],
                            LoginPwd = cmdArgs["pwd"]
                        });
                    }
                    if (cmdArgs.ContainsKey("handle")) { BaseForm.handleIdPb = int.Parse(cmdArgs["handle"]); }
                }
                if (!CurrentUserInfo.IsLogined)
                {
                    LoginForm login = new LoginForm();
                    if (DialogResult.OK != login.ShowDialog())
                    {
                        return;
                    }
                }

                if (cmdArgs.ContainsKey("includ") && cmdArgs["includ"] != null && cmdArgs["includ"].ToLower() == "true")
                    Application.Run(new MailMainForm(true));
                else
                    Application.Run(new MainForm());
            }
        }

        ///<summary>
        /// 发生未处理UI线程异常时处理的方法
        ///</summary>
        ///<param name="sender"> </param>
        ///<param name="e"> </param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            if (ex != null)
            {
                Utilities.Logger.WriteError(ex);
            }
            MessageBox.Show("系统错误,请稍候重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// 发生未处理非UI线程异常时处理的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {//todo exception 
                Utilities.Logger.WriteError(ex);
            }
            MessageBox.Show("系统错误,请稍候重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}
