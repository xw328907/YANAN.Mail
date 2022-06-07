using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using CCWin.SkinControl;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;

    public partial class MailComposeForm : BasePageForm
    {
        private ListBox lbxReceive = new ListBox() ;
        private TextBox currentTextBox;
        AutoCompleteStringCollection bindingSource = new AutoCompleteStringCollection();
        private ComposeActionEnum ComposeAction = ComposeActionEnum.Write;
        /// <summary>
        /// 邮件ID（参数）
        /// </summary>
        private string mailId = string.Empty;
        /// <summary>
        /// 邮件ID
        /// </summary>
        private string mailMainId = string.Empty;
        private string selectedMailBoxId = string.Empty;
        private string lastSaveDataMD5 = string.Empty;
        /// <summary>
        /// 附件数
        /// </summary>
        private List<MailAttach> mailAttaches = new List<MailAttach>();
        /// <summary>
        /// 正则执行保存操作
        /// </summary>
        private bool isSaving = false;
        public MailComposeForm(ComposeActionEnum action = ComposeActionEnum.Write, string mailId = "", string receiver = "")
        {
            InitializeComponent();
            InitControlEvent();
            if (!string.IsNullOrWhiteSpace(receiver))
            {
                txt_receiver.Text = (receiver.Trim(';') + ";").Trim(';');
            }
            ComposeAction = action;
            this.mailId = mailId;
            if (ComposeAction == ComposeActionEnum.Write && !string.IsNullOrWhiteSpace(mailId))
                mailMainId = mailId;
            InitForm();
        }
        /// <summary>
        /// 页面控件事件绑定
        /// </summary>
        private void InitControlEvent()
        {
            toolBtn_attach.Click += new EventHandler(toolBtnAttach_Click);
            tooBtn_bcc.Click += new EventHandler(toolBtnBcc_Click);
            toolBtn_cc.Click += new EventHandler(toolBtnCc_Click);
            toolBtn_save.Click += new EventHandler(toolBtnDraftSave_Click);
            toolBtn_product.Click += new EventHandler(toolBtnProduct_Click);
            toolBtn_quit.Click += new EventHandler(toolBtnQuit_Click);
            toolBtn_send.Click += new EventHandler(toolBtnSend_Click);
            //txt_receiver.KeyUp += new KeyEventHandler(txtReceiveKeyUp);
            //lbxReceive.Click += new EventHandler(lbxReceiveClick);
            chk_timer.CheckedChanged += new EventHandler(this.chk_timer_CheckedChanged);
            combox_sign.SelectedIndexChanged += new EventHandler(combox_sign_SelectedIndexChanged);
            combox_sender.SelectedIndexChanged += new EventHandler(combox_sender_SelectedIndexChanged);
            txt_subject.SkinTxt.TextChanged += new EventHandler(txt_subject_TextChanged);
            chk_singleSend.CheckedChanged += new EventHandler(chk_singleSend_CheckedChanged);
            lblReceiver.Click += new EventHandler(lblReceiver_Click);
            lblCc.Click += new EventHandler(lblCc_Click);
            lblBcc.Click += new EventHandler(lblBcc_Click);
        }
        #region 控件事件

        //邮件主题改变事件
        private void txt_subject_TextChanged(object sender, EventArgs e)
        {
            SetFormTitle();
        }
        //定时保存
        private void timer_autosave_Tick(object sender, EventArgs e)
        {
            var timer = (Timer)sender;
            timer.Stop();
            SaveMail(false, true);
            timer.Start();
        }
        //抄送
        private void toolBtnCc_Click(object sender, EventArgs e)
        {
            txt_cc.Enabled = txt_cc.ReadOnly;
            txt_cc.ReadOnly = !txt_cc.ReadOnly;
            if (txt_cc.ReadOnly)
            {
                txt_cc.Text = string.Empty;
                txt_receiver.Focus();
            }
            else { txt_cc.Focus(); }
        }
        //密送
        private void toolBtnBcc_Click(object sender, EventArgs e)
        {
            txt_bcc.Enabled = txt_bcc.ReadOnly;
            txt_bcc.ReadOnly = !txt_bcc.ReadOnly;
            if (txt_bcc.ReadOnly)
            {
                txt_bcc.Text = string.Empty;
                txt_receiver.Focus();
            }
            else { txt_bcc.Focus(); }
        }
        //存草稿
        private void toolBtnDraftSave_Click(object sender, EventArgs e)
        {
            SaveMail(false);
        }
        //发送
        private void toolBtnSend_Click(object sender, EventArgs e)
        {
            SaveMail(true);
        }
        //附件
        private void toolBtnAttach_Click(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();
            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
                //获取用户选择文件的后缀名
                string extension = Path.GetExtension(fileDialog.FileName);
                //获取用户选择的文件，并判断文件大小不能超过50M，fileInfo.Length是以字节为单位的
                FileInfo fileInfo = new FileInfo(fileDialog.FileName);

                if (fileInfo.Length <= 52428800)
                {
                    string path = string.Format(@"\{0}\UId_{1}\{2}\", loginInfo.OCode, loginInfo.UserId, DateTime.Now.ToString("yyyyMMdd"));
                    string attahPath = UtilityHelper.MailAttachBaseDirectory + path;
                    string actualName = UtilityHelper.GetGuid();//不含后缀名
                    if (!Directory.Exists(attahPath))
                        Directory.CreateDirectory(attahPath);
                    MailAttach attach = new MailAttach
                    {
                        ContentID = string.Empty,
                        ActualSize = (int)fileInfo.Length,
                        FilesName = fileInfo.Name,
                        FilesPath = attahPath + actualName,
                        FilesSize = UtilityHelper.ConvertFileSize(fileInfo.Length),
                        FilesType = extension.Replace(".", "")
                    };
                    AppendAttah(new List<MailAttach> { attach });
                    fileInfo.CopyTo(attach.FilesPath);
                }
                else
                {
                    MessageBox.Show("附件超过50M大部分邮局都不支持", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }

        }
        //产品
        private void toolBtnProduct_Click(object sender, EventArgs e)
        {

        }
        //退出
        private void toolBtnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
        //签名切换
        private void combox_sign_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSignToMailContent();
        }
        //邮箱切换
        private void combox_sender_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combox = (CCWin.SkinControl.SkinComboBox)sender;
            if (combox.SelectedItem != null)
            {
                string value = ((ListItem)combox.SelectedItem).Value;
                selectedMailBoxId = value;
                BindSign(value);
            }
        }
        //定时发送
        private void chk_timer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                datePicker_timer.Enabled = true;
                datePicker_timer.Visible = true;
            }
            else
            {
                datePicker_timer.Enabled = false;
                datePicker_timer.Visible = false;
            }
        }
        //分别发送
        private void chk_singleSend_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            txt_cc.ReadOnly = checkBox.Checked;
            txt_cc.Text = string.Empty;
            txt_bcc.ReadOnly = checkBox.Checked;
            txt_bcc.Text = string.Empty;
        }
        /// <summary>
        /// 选择联系人 - 收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReceiver_Click(object sender, EventArgs e)
        {
            OpenContactSelectDialog(txt_receiver);
        }
        /// <summary>
        ///  选择联系人 - 抄送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCc_Click(object sender, EventArgs e)
        {
            OpenContactSelectDialog(txt_cc);
        }
        /// <summary>
        ///  选择联系人 - 密送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblBcc_Click(object sender, EventArgs e)
        {
            OpenContactSelectDialog(txt_bcc);
        }

        private void lbxReceiveClick(object sender, EventArgs e)
        {
            ListBox obj = sender as ListBox;
            var selectedData = obj.SelectedItem as ListItem;
            if (currentTextBox != null)
            {
                currentTextBox.Text = (currentTextBox.Text.Trim(';') + ";" + selectedData.Text + ";").TrimStart(';');
            }
            obj.Visible = false;
        }
        private void txtReceiveKeyUp(object sender, KeyEventArgs e)
        {
            var obj = sender as TextBox;
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Left:
                    if (lbxReceive.SelectedIndex > 0)
                        lbxReceive.SelectedIndex--;
                    break;
                case Keys.Down:
                case Keys.Right:
                    if (lbxReceive.SelectedIndex < lbxReceive.Items.Count - 1)
                        lbxReceive.SelectedIndex++;
                    break;
                case Keys.Enter:
                    var selectedData = lbxReceive.SelectedItem as ListItem;
                    obj.Text = (obj.Text.Trim(';') + ";" + selectedData.Text + ";").TrimStart(';');
                    lbxReceive.Visible = false;
                    break;
                default:
                    lbxReceive.Visible = false;
                    if (!string.IsNullOrWhiteSpace(obj.Text))
                    {
                        var val = obj.Text.Split(new[] { ';' });
                        if (val.Length > 0)
                        {
                            string keyword = val[val.Length - 1];
                            if (!string.IsNullOrWhiteSpace(keyword))
                            {
                                var list = mailService.GetComposeMailContactsList(CurrentUserInfo.GetLoginedUserInfo(), keyword);
                                if (list.Count > 0)
                                {
                                    currentTextBox = obj;

                                    lbxReceive.DataSource = list;
                                    lbxReceive.DisplayMember = "Text";
                                    lbxReceive.ValueMember = "Value";
                                    if (lbxReceive.Visible == false)
                                    {
                                        lbxReceive.Visible = true;
                                        lbxReceive.Location = new System.Drawing.Point(obj.Location.X, obj.Location.Y + obj.Height);
                                        lbxReceive.Show();
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            //obj.Select(obj.Text.Length, 1); //光标定位到文本框最后
        }

        #endregion 控件事件

        /// <summary>
        /// 窗体内容初始化
        /// </summary>
        private void InitForm()
        {
            txt_receiver.Height = 28;
            txt_receiver.Focus();
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            #region 发件人数据源绑定

            var mailboxs = mailBoxService.GetListMailBoxCurrentUser(CurrentUserInfo.GetLoginedUserInfo());
            IList<ListItem> mailboxList = new List<ListItem>();
            MailBox defaultMailBox = null;
            if (mailboxs != null && mailboxs.Count > 0)
            {
                foreach (var mailbox in mailboxs)
                {
                    mailboxList.Add(new ListItem { Text = mailbox.MailAddress, Value = mailbox.MailBoxId, Tag = mailbox });
                    if (mailbox.IsDefault == true) defaultMailBox = mailbox;
                }
                if (defaultMailBox == null) defaultMailBox = mailboxs[0];
            }
            combox_sender.Bind(mailboxList);


            #endregion 发件人数据源绑定

            if (!string.IsNullOrWhiteSpace(mailId))
            {
                MailMain mailMain;
                if (ComposeAction != ComposeActionEnum.ForwardAsAttach) { mailMain = mailService.GetMailMain(mailId); }
                else { mailMain = mailService.ComposeMailAsAttach(loginInfo, mailId); }
                var box = mailboxs.FirstOrDefault(x => x.MailBoxId == mailMain.MailBoxId);
                if (box != null) defaultMailBox = box;
                switch (ComposeAction)
                {
                    case ComposeActionEnum.Forward:
                        txt_subject.Text = "Fw: " + mailMain.Subject;
                        if (mailMain.MailAttachs != null && mailMain.MailAttachs.Count > 0)
                        {
                            AppendAttah(mailMain.MailAttachs);
                        }
                        break;
                    case ComposeActionEnum.ForwardAsAttach:
                        txt_subject.Text = "Fw: " + mailMain.Subject;
                        if (mailMain.MailAttachs != null && mailMain.MailAttachs.Count > 0)
                        {
                            AppendAttah(mailMain.MailAttachs);
                        }
                        break;
                    case ComposeActionEnum.Reply:
                        txt_subject.Text = "Re: " + mailMain.Subject;
                        txt_receiver.Text = mailMain.Sender + ";";
                        break;
                    case ComposeActionEnum.ReplyAll:
                        txt_subject.Text = "Re: " + mailMain.Subject;
                        txt_receiver.Text = mailMain.Sender + ";";
                        txt_cc.Text = mailMain.Cc;
                        break;
                    default:
                        txt_subject.Text = mailMain.Subject;
                        txt_receiver.Text = mailMain.Receiver;
                        txt_cc.Text = mailMain.Cc;
                        txt_bcc.Text = mailMain.Bcc;
                        chk_important.Checked = mailMain.Importance == 1;
                        chk_read.Checked = mailMain.IsReadReply;
                        chk_singleSend.Checked = mailMain.IsGroup;
                        chk_timer.Checked = mailMain.IsTimer == true;
                        if (mailMain.IsTimer == true && mailMain.TimerSendTime.HasValue)
                        { datePicker_timer.Value = mailMain.TimerSendTime.Value; datePicker_timer.Visible = true; datePicker_timer.Enabled = true; }
                        if (mailMain.MailAttachs != null && mailMain.MailAttachs.Count > 0)
                        {
                            AppendAttah(mailMain.MailAttachs);
                        }
                        break;
                }
                txtContent.BodyInnerHTML = InitMailHtmlBody(mailMain);
                SetSignToMailContent();
                lastSaveDataMD5 = GetMailInfoMD5();
            }
            if (defaultMailBox != null)
            {
                combox_sender.SelectedValue = defaultMailBox.MailBoxId;
                if (!string.IsNullOrWhiteSpace(defaultMailBox.Bcc))
                {
                    txt_bcc.Text = (txt_bcc.Text.TrimEnd(';') + ";" + defaultMailBox.Bcc + ";").TrimStart(';');
                }
                if (!string.IsNullOrWhiteSpace(defaultMailBox.Cc))
                {
                    txt_cc.Text = (txt_cc.Text.TrimEnd(';') + ";" + defaultMailBox.Cc + ";").TrimStart(';');
                }
            }

            timer_autosave.Enabled = true;
            timer_autosave.Interval = 60000;//1分钟
            timer_autosave.Tick += new EventHandler(timer_autosave_Tick);

            //txt_receiver.AutoCompleteCustomSource.Add("wang");
            //txt_receiver.AutoCompleteCustomSource.Add("wangy");
            //txt_receiver.AutoCompleteCustomSource.Add("yy");
            //txt_receiver.AutoCompleteCustomSource.Add("yyyy");
            //txt_receiver.AutoCompleteCustomSource = bindingSource;
            //txt_receiver.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txt_receiver.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void OpenContactSelectDialog(SkinTextBox textBox)
        {
            MailContactSelectForm contactSelectForm = new MailContactSelectForm();
            if (DialogResult.OK == contactSelectForm.ShowDialog())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var contact in contactSelectForm.SelectedMailContactList)
                {
                    sb.Append(UtilityHelper.GetMailAddressText(contact.EMail, contact.ContactName) + ";");
                }
                if (sb.ToString().Length > 0)
                {
                    textBox.Text = (textBox.Text.Trim(';') + ";" + sb.ToString()).TrimStart(';');
                }
            }
        }
        /// <summary>
        /// 邮箱签名绑定
        /// </summary>
        /// <param name="mailBoxId"></param>
        private void BindSign(string mailBoxId)
        {
            if (!string.IsNullOrWhiteSpace(mailBoxId))
            {
                var signs = mailService.GetListSelfMailSign(CurrentUserInfo.GetLoginedUserInfo(), mailBoxId, 1);
                IList<ListItem> signList = new List<ListItem>();
                ListItem defaultSign = null;
                if (signs != null)
                {
                    foreach (var sign in signs)
                    {
                        signList.Add(new ListItem { Text = sign.SignatureName, Value = sign.SignatureContent });
                        if (sign.IsDefault == true)
                        {
                            defaultSign = new ListItem { Value = sign.SignatureContent, Text = sign.SignatureName };
                        }
                    }
                }
                combox_sign.Bind(signList);
                if (defaultSign != null)
                {
                    combox_sign.SelectedValue = defaultSign.Value;
                }
            }
        }
        /// <summary>
        /// 初始化邮件正文内容
        /// </summary>
        /// <param name="mailMain"></param>
        /// <returns></returns>
        private string InitMailHtmlBody(MailMain mailMain)
        {
            StringBuilder sb = new StringBuilder();
            switch (ComposeAction)
            {
                case ComposeActionEnum.Forward:
                case ComposeActionEnum.Reply:
                case ComposeActionEnum.ReplyAll:
                    sb.Append("<br/><br/>");
                    sb.Append("<p m=\"sign_content\"></p>");
                    sb.Append("<p m=\"sign_content_end\"></p>");
                    sb.Append("<div style=\"font-family:Arial;BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM:5px; PADDING-LEFT: 5px; PADDING-RIGHT: 0cm; BORDER-TOP: #b5c4df 1pt solid; BORDER-RIGHT: medium none; PADDING-TOP: 3pt;font-size:12px;background-color: #efefef;display:block\" > ");
                    sb.Append("<p>");
                    sb.AppendFormat("<strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{1}:</span></strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\"><a href=\"mailto:{0}\">{0}</a></span><br/>", mailMain.Sender, "From");
                    sb.AppendFormat("<strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{1}:</span></strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{0}</span><br/>", mailMain.MailTime.ToString("yyyy/MM/dd hh:mm:ss"), "Time");
                    sb.AppendFormat("<strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{1}:</span></strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\"><a href=\"mailto:{0}\">{0}</a></span><br/>", mailMain.Receiver, "To");
                    if (!string.IsNullOrWhiteSpace(mailMain.Cc))
                        sb.AppendFormat("<strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{1}:</span></strong><span style=\"FONT -FAMILY: Arial;FONT-SIZE: 12px;\"><a href=\"mailto:{0}\">{0}</a></span><br/>", mailMain.Cc, "Cc");
                    sb.AppendFormat("<strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{1}:</span></strong><span style=\"FONT-FAMILY: Arial;FONT-SIZE: 12px;\">{0}</span><br/>", mailMain.Subject, "Subject");
                    sb.Append("</p>");
                    sb.Append("</div>");
                    break;
            }
            if (mailMain.MailBody != null && !string.IsNullOrWhiteSpace(mailMain.MailBody.BodyHtml))
                sb.Append(mailMain.MailBody.BodyHtml);
            return sb.ToString();
        }
        /// <summary>
        /// 设置邮件签名记录
        /// </summary>
        private void SetSignToMailContent()
        {
            var selectItem = combox_sign.SelectedItem;
            if (selectItem != null)
            {
                string value = ((ListItem)selectItem).Value;
                string content = txtContent.BodyInnerHTML ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string newContent = string.Empty;
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        var split1 = content.Split(new string[] { "<P m=\"sign_content\">" }, StringSplitOptions.RemoveEmptyEntries);
                        var split2 = content.Split(new string[] { "<P m=\"sign_content_end\">" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split1.Length > 0 && split2.Length > 1)
                        {
                            newContent = split1[0] + "<P m=\"sign_content\"><p><hr style=\"text-align:left;width:50%;margin-left:0\" /></p>" + value + "</P><P m=\"sign_content_end\">" + split2[1];
                        }
                    }
                    else
                    {
                        newContent = "<br/><br/><P m=\"sign_content\"><p><hr style=\"text-align:left;width:50%;margin-left:0\" /></p>" + value + "</P><P m=\"sign_content_end\"></P>";

                    }

                    // newContent = Regex.Replace(content, "(<p m=\"sign_content\">).+(</p>)", value, RegexOptions.IgnoreCase);
                    txtContent.BodyInnerHTML = newContent;
                }
            }
        }
        /// <summary>
        /// 保存(发送)邮件
        /// </summary>
        /// <param name="isSend">是否发送；true=发送（默认值）、false=存草稿</param>
        /// <param name="autoSave">是否自动保存,isSend=false 时值有效</param>
        private void SaveMail(bool isSend = true, bool autoSave = false)
        {
            if (isSaving) return;
            var mail = GetMailSaveData();
            var mailMD5 = GetMailInfoMD5(mail);
            if (lastSaveDataMD5 == mailMD5) { return; }
            else
            {
                lastSaveDataMD5 = mailMD5;
            }
            if (isSend)//发送时才需要校验
            {
                if (string.IsNullOrWhiteSpace(mail.Receiver))
                {
                    MessageBox.Show("收件人为空或格式错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(mail.MailBoxId))
            {
                if (!autoSave)
                {
                    MessageBox.Show("请选择发件人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                return;
            }
            if (autoSave != true)
                isSaving = true;
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            var result = isSend ? mailService.MailSend(loginInfo, mail) : mailService.MailSave(loginInfo, mail);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                mailMainId = result.Data.ToString();
                if (isSend)
                {
                    if (mail.IsTimer != true && !mail.TimerSendTime.HasValue)
                    {
                        mailAlert.SendMail(loginInfo.OCode, new string[] { mail.MailMainId });
                    }
                    else
                    {
                        MessageBox.Show(string.Format("邮件保存成功,将于{0}定时发送", mail.TimerSendTime.Value.ToString("yyyy-MM-dd HH:mm")), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    Close();
                }
                else
                {
                    SetFormTitle(DateTime.Now.ToLongTimeString() + " 已保存");
                }
                isSaving = false;
            }
        }
        private void SetFormTitle(string lastText = "")
        {
            if (string.IsNullOrWhiteSpace(lastText))
            {
                var old = Text.Split(new string[] { "写邮件" }, StringSplitOptions.RemoveEmptyEntries);
                lastText = old.Length > 1 ? old[1].Trim() : string.Empty;
            }
            Text = txt_subject.Text + " - 写邮件   " + lastText;
        }
        /// <summary>
        /// 获取邮件发送/保存数据
        /// </summary>
        /// <returns></returns>
        private MailMain GetMailSaveData()
        {
            MailMain mail = new MailMain
            {
                Subject = txt_subject.Text.Trim(),
                MailBoxId = selectedMailBoxId,
                MailType = 2,
                MailBody = new MailBody { BodyHtml = txtContent.BodyInnerHTML },
                Importance = chk_important.Checked ? 1 : 3,
                IsReadReply = chk_read.Checked,
                IsGroup = chk_singleSend.Checked,
                IsTimer = chk_timer.Checked,
                MailAttachs = mailAttaches
            };
            mail.MailMainId = mailMainId;

            switch (ComposeAction)
            {
                case ComposeActionEnum.Forward:
                case ComposeActionEnum.ForwardAsAttach:
                    mail.FromId = mailId;
                    mail.FromTypeId = 2;
                    break;
                case ComposeActionEnum.Reply:
                case ComposeActionEnum.ReplyAll:
                    mail.FromId = mailId;
                    mail.FromTypeId = 1;
                    break;
            }

            List<ListItem> emails = new List<ListItem>();
            emails = getEmailList(txt_receiver.Text.Trim());
            if (emails.Count > 0)
            {
                foreach (var email in emails)
                {
                    mail.MailGroups.Add(new MailGroup
                    {
                        ReceiveTypeCode = (int)MailGroupReceiveTypeEnum.Receiver,
                        ReceiveAddress = email.Value,
                        ReceiveName = email.Text
                    });
                }
                mail.Receiver = string.Join(";", emails.Select(x => x.Tag));
            }
            if (mail.IsGroup == false)
            {
                emails = getEmailList(txt_cc.Text.Trim());
                if (emails.Count > 0)
                {
                    foreach (var email in emails)
                    {
                        mail.MailGroups.Add(new MailGroup
                        {
                            ReceiveTypeCode = (int)MailGroupReceiveTypeEnum.Cc,
                            ReceiveAddress = email.Value,
                            ReceiveName = email.Text
                        });
                    }
                    mail.Cc = string.Join(";", emails.Select(x => x.Tag));
                }
                emails = getEmailList(txt_bcc.Text.Trim());
                if (emails.Count > 0)
                {
                    foreach (var email in emails)
                    {
                        mail.MailGroups.Add(new MailGroup
                        {
                            ReceiveTypeCode = (int)MailGroupReceiveTypeEnum.Bcc,
                            ReceiveAddress = email.Value,
                            ReceiveName = email.Text
                        });
                    }
                    mail.Bcc = string.Join(";", emails.Select(x => x.Tag));
                }
            }
            if (mail.IsTimer == true && datePicker_timer.Enabled == true)
            {
                mail.TimerSendTime = datePicker_timer.Value;
            }
            return mail;
        }
        /// <summary>
        /// 获取邮箱列表，用于获取收件人、抄送、密送
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        private List<ListItem> getEmailList(string emails)
        {
            List<ListItem> list = new List<ListItem>();
            if (!string.IsNullOrWhiteSpace(emails))
            {
                Regex regexEmail = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                var emailSplits = emails.Split(';');
                foreach (string str in emailSplits)
                {
                    string name = string.Empty; string email = string.Empty;
                    if (str.IndexOf("<") > -1 && str.IndexOf(">") > -1)
                    {
                        var s = str.Split('<');
                        name = s[0];
                        email = s[1].Replace(">", "");
                    }
                    else
                    {
                        email = str;
                        name = str.Split('@')[0];
                    }
                    if (regexEmail.IsMatch(email))
                    {
                        list.Add(new ListItem { Value = email, Text = name, Tag = UtilityHelper.GetMailAddressText(email, name) });
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取邮件的MD5值(用于自动保存判断内容是否有更改)
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        private string GetMailInfoMD5(MailMain mail = null)
        {
            if (mail == null)
                mail = GetMailSaveData();
            string val = JsonSerializationHelper.JsonSerialize(mail);
            return EncryptHelper.MD5(val);
        }
        /// <summary>
        /// 附加附件
        /// </summary>
        /// <param name="attaches">本次需要附加的附件记录集合</param>
        private void AppendAttah(List<MailAttach> attaches)
        {
            FlowLayoutPanel layoutPanel;
            string key = "panel_attach";
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
            int width = 240, height = 24;
            if (attaches.Count > 0)
            {
                mailAttaches.AddRange(attaches);
            }
            layoutPanel.Controls.Clear();
            foreach (var attach in mailAttaches)
            {
                Controls.AttachControl attachControl = new Controls.AttachControl(attach, MaiAttachContextMenuModeEnum.Edit)
                {
                    Margin = new Padding(5)
                };
                attachControl.EventCompletedCallback += new EventHandler<Controls.AttachControlCallbackEventArgs>((sender, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.MailAttach.MailMainId) || e.MailAttach.MailMainId.Equals(mailMainId))
                    {//非转发引入的即可删除物理文件
                        if (File.Exists(e.MailAttach.FilesPath)) File.Delete(e.MailAttach.FilesPath);
                    }
                    mailAttaches.Remove(e.MailAttach);
                    AppendAttah(new List<MailAttach>());
                });
                layoutPanel.Controls.Add(attachControl);
                width = attachControl.Width; height = attachControl.Height;
            }
            if (mailAttaches.Count < 1)
            {
                Controls.Remove(layoutPanel);
                return;
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
    }
}
