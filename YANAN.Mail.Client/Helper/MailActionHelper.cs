using YANAN.Mail.Entity;
using YANAN.Mail.IServices;
using YANAN.Mail.Services;
using YANAN.Mail.Utilities;
using YANAN.Mail.Utilities.Enums;
using System;
using System.Windows.Forms;
namespace YANAN.Mail.Client
{
    /// <summary>
    /// 邮件操作(封装)
    /// </summary>
    public class MailActionHelper
    {
        /// <summary>
        /// 邮件服务对象
        /// </summary>
        private static IMailService mailService = new MailService();
        /// <summary>
        /// 邮件移动至... (文件夹)
        /// </summary>
        /// <param name="mailMain">邮件数据对象(需包含邮件ID、邮箱ID、所属邮箱文件夹ID)</param>
        /// <param name="action">移动成功后执行方法</param>
        public static void MoveMailFolder(MailMain mailMain, Action<string> action = null)
        {
            if (mailMain == null) return;
            MailFolderSelectForm folderSelectForm = new MailFolderSelectForm(mailMain.MailBoxId);
            if (DialogResult.OK == folderSelectForm.ShowDialog())
            {
                string folderId = folderSelectForm.SelectedFolderId;
                if (!string.IsNullOrWhiteSpace(folderId) && folderId != mailMain.MailFolderId)
                {
                    IMailService mailService = new MailService();
                    var result = mailService.MoveMailToFolder(mailMain.MailMainId, folderId);
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                    {
                        action?.Invoke(folderSelectForm.SelectedFolderId);
                    }
                    else
                    {
                        MessageBox.Show("移动邮件失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailId"></param>
        public static void Remove(string mailId, Action<ResponseResult> action = null)
        {
            if (string.IsNullOrWhiteSpace(mailId))
            {
                MessageBox.Show("请先选择邮件记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); return;
            }
            var result = mailService.RemoveMail(CurrentUserInfo.GetLoginedUserInfo(), new string[] { mailId });
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                action?.Invoke(result);
                MessageBox.Show("删除成功", "提示");
            }
            else
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

    }
}
