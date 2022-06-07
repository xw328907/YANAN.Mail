using System;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    /// <summary>
    /// 窗体 - 联系人新增/编辑
    /// </summary>
    public partial class MailContactManageForm : BaseChildForm
    {
        private int ContactId = 0;
        private MailMainForm mailMainForm = null;
        public MailContactManageForm(int contactId = 0, MailMainForm mailMainForm = null)
        {
            this.mailMainForm = mailMainForm;
            InitializeComponent();
            InitControlEvents();
            LoadData(contactId);
        }
        private void InitControlEvents()
        {
            btn_save.Click += new EventHandler(btn_save_Click);
            btnAdd.Click += new EventHandler(btnAdd_Click);
        }

        public void LoadData(int contactId = 0)
        {
            ContactId = contactId;
            MailContact contact = null;
            if (ContactId > 0)
            {
                contact = mailService.GetMailContact(ContactId);
            }
            SetFormData(contact);
        }

        private void SetFormData(MailContact mailContact = null)
        {
            if (mailContact == null) mailContact = new MailContact();
            ContactId = mailContact.MailContactId;
            txtAddress.Text = mailContact.Address;
            txtCompanyName.Text = mailContact.CompanyName;
            txtContactName.Text = mailContact.ContactName;
            txtEMail.Text = mailContact.EMail;
            txtFacebook.Text = mailContact.Facebook;
            txtMemo.Text = mailContact.Memo;
            txtMobile.Text = mailContact.Mobile;
            txtPost.Text = mailContact.Post;
            txtQQ.Text = mailContact.QQ;
            txtSkype.Text = mailContact.Skype;
            txtTel.Text = mailContact.Tel;
            txtTwitter.Text = mailContact.Twitter;
            txtWeChat.Text = mailContact.WeChat;
        }
        private MailContact GetFormData()
        {
            MailContact mailContact = new MailContact
            {
                MailContactId = ContactId,
                Address = txtAddress.Text.Trim(),
                CompanyName = txtCompanyName.Text.Trim(),
                ContactName = txtContactName.Text.Trim(),
                EMail = txtEMail.Text.Trim(),
                Facebook = txtFacebook.Text.Trim(),
                Memo = txtMemo.Text.Trim(),
                Mobile = txtMobile.Text.Trim(),
                Post = txtPost.Text,
                QQ = txtQQ.Text.Trim(),
                Skype = txtSkype.Text.Trim(),
                Tel = txtTel.Text.Trim(),
                Twitter = txtTwitter.Text.Trim(),
                WeChat = txtWeChat.Text.Trim()
            };
            mailContact.ContactPinyin = PinYinHelper.GetPinyin(mailContact.ContactName);
            return mailContact;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            var mailContact = GetFormData();
            if (string.IsNullOrWhiteSpace(mailContact.ContactName))
            {
                MessageBox.Show("联系人不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(mailContact.EMail))
            {
                MessageBox.Show("邮箱地址不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!UtilityHelper.IsEmail(mailContact.EMail))
            {
                MessageBox.Show("无效的邮箱地址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            var result = mailService.SaveMailContact(CurrentUserInfo.GetLoginedUserInfo(), mailContact);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                MessageBox.Show("保存成功", "提示");
                ContactId = 0;
                LoadData();
                if (mailMainForm != null)
                    mailMainForm.LoadGridContact(true);
            }
            else
            {
                MessageBox.Show("邮箱地址不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void btnAdd_Click(object sender,EventArgs e) {
            LoadData(0);
        }
    }
}
