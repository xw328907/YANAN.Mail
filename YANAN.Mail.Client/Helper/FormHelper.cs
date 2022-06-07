using YANAN.Mail.Utilities.Enums;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    /// <summary>
    /// 窗体操作(封装)
    /// </summary>
    public class FormHelper
    {
        /// <summary>
        /// 窗体对象，主要用于邮件查看窗体
        /// </summary>
        public static Dictionary<string, Form> dicForm = new Dictionary<string, Form>();

        /// <summary>
        /// 打开邮件查看窗体(独立窗体)
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        public static void OpenMailViewForm(string mailId)
        {
            if (dicForm.ContainsKey(mailId))
            {
                dicForm[mailId].Activate();
            }
            else
            {
                MailViewForm mailView = new MailViewForm(mailId);
                dicForm.Add(mailId, mailView);
                mailView.Show();
                mailView.Activate();
            }
        }
        /// <summary>
        /// 获取邮件查看窗体对象
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns></returns>
        public static Form GetMailViewForm(string mailId)
        {
            if (dicForm.ContainsKey(mailId))
            {
                return dicForm[mailId];
            }
            return null;
        }
        /// <summary>
        /// 关闭邮件查看窗体(独立窗体)
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        public static void CloseMailViewForm(string mailId)
        {
            if (dicForm.ContainsKey(mailId)) { dicForm.Remove(mailId); }
        }
        /// <summary>
        /// 打开写邮件窗口
        /// </summary>
        /// <param name="action"></param>
        /// <param name="mailId"></param>
        /// <param name="receiver"></param>
        public static void OpenComposeForm(ComposeActionEnum action = ComposeActionEnum.Write, string mailId = "", string receiver = "")
        {
            if (action != ComposeActionEnum.Write && string.IsNullOrWhiteSpace(mailId))
            {
                MessageBox.Show("请先选择邮件记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            MailComposeForm writeForm = new MailComposeForm(action, mailId, receiver) { WindowState = FormWindowState.Normal };
            writeForm.Show();
        }
    }
}
