namespace YANAN.Mail.Client
{
    using YANAN.Mail.Entity;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class MailViewDetailForm : BaseChildForm
    {
        private string mailId = string.Empty;
        private MailMain mailMain;
        public MailViewDetailForm(string mailId = "")
        {
            this.mailId = mailId;
            InitializeComponent();
        }
        /// <summary>
        /// 页面数据重新加载
        /// </summary>
        /// <param name="mailId"></param>
        public void Reload(string mailId = "")
        {
            this.mailId = mailId;
            LoadMail();
        }
        private void LoadMail()
        {
            int rowNum = tableLayoutPanel1.RowCount;
            if (!string.IsNullOrWhiteSpace(mailId))
            {
                mailMain = mailService.GetMailMain(mailId);
                var form = FormHelper.GetMailViewForm(mailId);
                if (form != null)
                {
                    form.Text = mailMain.Subject;
                    if (form.Name == "MailViewForm")
                    {
                        (form as MailViewForm).mailMain = mailMain;
                    }
                }
                lbl_subject.Text = mailMain.Subject;
                if (!string.IsNullOrWhiteSpace(mailMain.Cc))
                {
                    lbl_cc.Text = mailMain.Cc;
                    tableLayoutPanel1.RowStyles[2].Height = 28;
                }
                else
                {
                    tableLayoutPanel1.RowStyles[2].Height = 0;
                    rowNum--;
                }
                if (!string.IsNullOrWhiteSpace(mailMain.Bcc))
                {
                    lbl_bcc.Text = mailMain.Bcc;
                    tableLayoutPanel1.RowStyles[3].Height = 28;
                    panel_head.Height += 28;
                }
                else
                {
                    tableLayoutPanel1.RowStyles[3].Height = 0;
                    rowNum--;
                }
                panel_head.Height = rowNum * 28;
                lbl_mailtime.Text = mailMain.MailTime.ToString("yyyy-MM-dd HH:mm");
                lbl_receiver.Text = mailMain.Receiver;
                lbl_sender.Text = mailMain.Sender;

                if (mailMain != null && mailMain.MailBody != null)
                    html_mailbody.BodyInnerHTML = !string.IsNullOrWhiteSpace(mailMain.MailBody.BodyHtml) ? mailMain.MailBody.BodyHtml : mailMain.MailBody.BodyText;
                AppendAttah(mailMain.MailAttachs);
            }
        }
        private void MailViewDetail_Load(object sender, System.EventArgs e)
        {
            LoadMail();
        }
        /// <summary>
        /// 加载附件
        /// </summary>
        /// <param name="attaches"></param>
        private void AppendAttah(List<MailAttach> attaches)
        {
            FlowLayoutPanel layoutPanel;
            string key = "panel_attach";
            if (attaches != null && attaches.Count > 0)
            {
                if (!Controls.ContainsKey(key))
                {
                    layoutPanel = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Bottom,
                        Name = key,
                        Size = new System.Drawing.Size(Size.Width, 28),
                        MaximumSize = new System.Drawing.Size(Size.Width, 56)
                    };
                    Controls.Add(layoutPanel);
                }
                else
                {
                    layoutPanel = (FlowLayoutPanel)Controls[key];
                }
                layoutPanel.Controls.Clear();
                int width = 240, height = 24;
                foreach (var attach in attaches)
                {
                    Controls.AttachControl attachControl = new Controls.AttachControl(attach, Utilities.Enums.MaiAttachContextMenuModeEnum.View) { Margin = new Padding(5) };
                    layoutPanel.Controls.Add(attachControl);
                    width = attachControl.Width; height = attachControl.Height;
                }
                int totalWidth = layoutPanel.Width, totalCount = layoutPanel.Controls.Count;
                int s = totalWidth / width;
                int row = 1;
                if (totalCount > s)
                {
                    row = totalCount / s;
                    if (totalCount % s != 0) row++;
                }
                layoutPanel.Height = 35 * row;
            }
            else
            {
                if (Controls.ContainsKey(key))
                    Controls.RemoveByKey(key);
            }
        }
    }
}
