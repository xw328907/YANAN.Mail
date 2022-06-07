using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities.Enums;

    public partial class MailSignForm : BaseChildForm
    {
        private int SelectSignId = 0;
        public MailSignForm()
        {
            InitializeComponent();
            InitControlsEvent();
            InitFrom();
        }
        private void InitControlsEvent()
        {
            combo_MailBoxId.DropDownStyle = ComboBoxStyle.DropDownList;
            tree_sign.NodeMouseClick += new TreeNodeMouseClickEventHandler(tree_sign_NodeMouseClick);
            toolBtn_add.Click += new EventHandler(toolBtn_add_Click);
            toolBtn_delete.Click += new EventHandler(toolBtn_delete_Click);
            btn_save.Click += new EventHandler(btn_save_Click);
        }
        private void InitFrom()
        {
            InitAddForm();

            #region 邮箱数据源绑定

            var mailboxs = mailBoxService.GetListMailBoxCurrentUser(CurrentUserInfo.GetLoginedUserInfo());
            IList<ListItem> mailboxList = new List<ListItem>();
            MailBox defaultMailBox = new MailBox();
            if (mailboxs != null)
            {
                foreach (var mailbox in mailboxs)
                {
                    mailboxList.Add(new ListItem { Text = mailbox.MailAddress, Value = mailbox.MailBoxId, Tag = mailbox });
                    if (mailbox.IsDefault == true) defaultMailBox = mailbox;
                }
            }
            combo_MailBoxId.Bind(mailboxList);
            if (defaultMailBox != null && !string.IsNullOrWhiteSpace(defaultMailBox.MailBoxId))
                combo_MailBoxId.SelectedValue = defaultMailBox.MailBoxId;

            #endregion 邮箱数据源绑定

            LoadTreeMailSign();
        }
        private void tree_sign_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (SelectSignId.ToString() != e.Node.Name)//有改变选中节点
            {
                int.TryParse(e.Node.Name, out SelectSignId);
                if (e.Node.Tag != null)
                {
                    var nodeTag = (MailSignature)e.Node.Tag;
                    LoadMailSignData(nodeTag);
                }
            }
        }
        //添加
        private void toolBtn_add_Click(object sender, EventArgs e)
        {
            InitAddForm();
        }
        //删除
        private void toolBtn_delete_Click(object sender, EventArgs e)
        {
            var node = tree_sign.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请选择要删除的签名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (MessageBox.Show("确定删除签名" + node.Text + "?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                return;
            var result = mailService.RemoveMailSign(CurrentUserInfo.GetLoginedUserInfo(), SelectSignId);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                MessageBox.Show("删除成功", "提示");
                SelectSignId = 0;
                tree_sign.Nodes.Remove(node);
            }
            else
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            MailSignature signature = new MailSignature
            {
                MailSignatureId = SelectSignId,
                SignatureName = txt_SignatureName.Text.Trim(),
                SignatureContent = txt_signContent.BodyInnerHTML,
                IsDefault = radio_default_yes.Checked,
                MailBoxId = ((ListItem)combo_MailBoxId.SelectedItem).Value,
                SignType = 1
            };
            var result = mailService.SaveMailSign(loginInfo, signature);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                MessageBox.Show("保存成功", "提示");
                signature = result.Data as MailSignature;
                SelectSignId = signature.MailSignatureId;
                LoadTreeMailSign();
            }
            else
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>
        /// 初始化添加窗口数据
        /// </summary>
        private void InitAddForm()
        {
            //txt_signContent.BodyInnerHTML = "";
            //txt_SignatureName.Text = string.Empty;
            //radio_default_no.Checked = true;
            LoadMailSignData(new MailSignature { IsDefault = false, SignatureContent = string.Empty, SignatureName = string.Empty });
        }
        private void LoadMailSignData(MailSignature signature)
        {
            SelectSignId = signature.MailSignatureId;
            txt_SignatureName.Text = signature.SignatureName;
            if (!string.IsNullOrWhiteSpace(signature.MailBoxId))
                combo_MailBoxId.SelectedValue = signature.MailBoxId;
            if (signature.IsDefault == true) { radio_default_yes.Checked = true; }
            else { radio_default_no.Checked = true; }
            txt_signContent.BodyInnerHTML = signature.SignatureContent;
        }
        /// <summary>
        /// 加载邮箱签名列表
        /// </summary>
        public void LoadTreeMailSign()
        {
            tree_sign.Nodes.Clear();
            List<TreeNode> nodes = new List<TreeNode>();
            string mailboxId = string.Empty;
            if (combo_MailBoxId.SelectedItem != null)
                mailboxId = ((ListItem)combo_MailBoxId.SelectedItem).Value;
            var list = mailService.GetListSelfMailSign(CurrentUserInfo.GetLoginedUserInfo(), mailboxId);
            foreach (var item in list)
            {
                var node = new TreeNode
                {
                    Name = item.MailSignatureId.ToString(),
                    Tag = item,
                    Text = item.SignatureName
                };
                nodes.Add(node);
            }
            tree_sign.Nodes.AddRange(nodes.ToArray());
        }
    }
}
