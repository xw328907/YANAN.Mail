using YANAN.Mail.Entity;
using YANAN.Mail.Utilities.Enums;
using System;
namespace YANAN.Mail.Client
{
    /// <summary>
    /// 邮件查看页
    /// </summary>
    public partial class MailViewForm : BasePageForm
    {
        private string mailId = "";
        public MailMain mailMain;
        public MailViewForm(string mailId = "")
        {
            this.mailId = mailId;
            InitializeComponent();
            toolBtn_reply.Click += new EventHandler(toolBtn_reply_Click);
            toolBtn_replyAll.Click += new EventHandler(toolBtn_reply_Click);
            toolBtn_move.Click += new EventHandler(toolBtn_move_Click);
            toolBtn_forward.Click += new EventHandler(toolBtn_forward_Click);
            toolBtn_delete.Click += new EventHandler(toolBtn_delete_Click);
            toolBtn_quit.Click += new EventHandler(toolBtnQuit_Click);
            MailViewDetailForm viewDetail = new MailViewDetailForm(this.mailId) { Dock = System.Windows.Forms.DockStyle.Fill, TopLevel = false };
            panel_body.Controls.Clear();
            panel_body.Controls.Add(viewDetail);
            viewDetail.Show();
        }

        private void MailView_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            FormHelper.CloseMailViewForm(mailId);
        }
        //退出
        private void toolBtnQuit_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }
        private void toolBtn_reply_Click(object sender, EventArgs e)
        {
            FormHelper.OpenComposeForm(ComposeActionEnum.Reply, mailId);
        }

        private void toolBtn_replyAll_Click(object sender, EventArgs e)
        {
            FormHelper.OpenComposeForm(ComposeActionEnum.ReplyAll, mailId);
        }
        private void toolBtn_forward_Click(object sender, EventArgs e)
        {
            FormHelper.OpenComposeForm(ComposeActionEnum.Forward, mailId);
        }

        private void toolBtn_move_Click(object sender, EventArgs e)
        {
            if (mailMain != null)
                MailActionHelper.MoveMailFolder(mailMain);
        }

        private void toolBtn_delete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mailId))
            {
                MailActionHelper.Remove(mailId, (result) =>
                {
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                    {
                        this.CloseForm();
                    }
                });
            }
        }
    }
}
