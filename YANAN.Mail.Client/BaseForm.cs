using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using YANAN.Mail.IServices;
    using YANAN.Mail.Services;

    /// <summary>
    /// 基础窗体-所有的窗体都继承于此窗体
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// 消息通信-与PB业务系统通讯
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);
        /// <summary>
        /// PB业务系统主程序调用窗口句柄
        /// </summary>
        public static int? handleIdPb = null;
        /// <summary>
        /// 主窗体对象
        /// </summary>
        public MailMainForm FormMailMain = null;
        /// <summary>
        /// 邮箱服务对象
        /// </summary>
        public IMailBoxService mailBoxService = new MailBoxService();
        /// <summary>
        /// 邮件服务对象
        /// </summary>
        public IMailService mailService = new MailService();
        /// <summary>
        /// 邮件过滤服务对象
        /// </summary>
        public IMailFilterService mailFilterService = new MailFilterService();
        /// <summary>
        /// 邮件收发等操作线程通知服务对象
        /// </summary>
        public MailAlert mailAlert = new MailAlert();
        public BaseForm()
        {
            InitializeComponent();
        }
    }
}
