using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using CCWin.SkinControl;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities.Enums;
    /// <summary>
    /// 邮箱设置(配置)
    /// </summary>
    public partial class MailSettingForm : BaseChildForm
    {
        private string SelectedMailBoxId = null;
        public MailSettingForm()
        {
            InitializeComponent();
            combo_receiveTime.SelectedIndexChanged += new EventHandler(combo_receiveTime_SelectedIndexChanged);
        }
        private void MailSetting_Load(object sender, EventArgs e)
        {
            IList<ListItem> comboList;
            comboList = new List<ListItem> { new ListItem("永久保留", "36500"), new ListItem("保留三个月", "90"), new ListItem("保留一个月", "30"), new ListItem("不保留", "0") };
            cbox_KeepDays.Bind(comboList);
            comboList = new List<ListItem> { new ListItem("POP3", "1"), new ListItem("IMAP", "2") };
            cbox_ProtocolTypeId.Bind(comboList);
            comboList = new List<ListItem> { new ListItem("全部", "0"), new ListItem("自定义", "1") };
            combo_receiveTime.Bind(comboList);
            InitAddForm();
            LoadTreeMailBox();
        }
        /// <summary>
        /// 初始化添加窗口数据
        /// </summary>
        private void InitAddForm()
        {
            tree_mailbox.SelectedNode = null;
            SelectedMailBoxId = null;
            LoadMailBoxFormData(new MailBox { ReceiveTimer = 15, KeepDays = 36500, ProtocolTypeId = 1 });
        }
        public void LoadTreeMailBox()
        {
            tree_mailbox.Nodes.Clear();
            List<TreeNode> nodes = new List<TreeNode>();
            var list = mailBoxService.GetListMailBoxCurrentUser(CurrentUserInfo.GetLoginedUserInfo());
            TreeNode selectedNode = null;
            foreach (var item in list)
            {
                var node = new TreeNode
                {
                    Name = item.MailBoxId,
                    Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailBox.ToString(), Data = item },
                    Text = item.ShowName,
                    ImageKey = "mail_receive_box.png"
                };
                node.SelectedImageKey = node.ImageKey;
                nodes.Add(node);
                if (!string.IsNullOrWhiteSpace(SelectedMailBoxId) && node.Name == SelectedMailBoxId)
                    selectedNode = node;
            }
            tree_mailbox.Nodes.AddRange(nodes.ToArray());
            tree_mailbox.SelectedNode = selectedNode;
        }
        /// <summary>
        /// 邮箱文件夹数据转换为树节点结构
        /// </summary>
        /// <param name="folderDto"></param>
        /// <returns></returns>
        private static TreeNode GetTreeNode(MailFolder folderDto)
        {
            TreeNode node = new TreeNode
            {
                Name = folderDto.MailFolderId,
                Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString(), Data = folderDto },
                Text = folderDto.FolderName,
            };

            return node;
        }
        public void LoadMailBoxFormData(MailBox mailBox)
        {
            txt_bcc.Text = mailBox.Bcc;
            txt_cc.Text = mailBox.Cc;
            txt_MailAddress.Text = mailBox.MailAddress;
            txt_MailPassword.Text = string.Empty;
            txt_ShowName.Text = mailBox.ShowName;
            txt_PopPort.Text = mailBox.PopPort.ToString();
            txt_PopServer.Text = mailBox.PopServer;

            txt_NickName.Text = mailBox.NickName;
            txt_SmtpPort.Text = mailBox.SmtpPort.ToString();
            txt_SmtpServer.Text = mailBox.SmtpServer;
            cbox_KeepDays.SelectedValue = mailBox.KeepDays.ToString();
            cbox_ProtocolTypeId.SelectedValue = mailBox.ProtocolTypeId.ToString();
            chk_ReceiveTimer.Checked = mailBox.ReceiveTimer > 0;
            if (mailBox.ReceiveBeginTime.HasValue)
            {
                combo_receiveTime.SelectedIndex = 1;
                dataPicker_ReceiveBeginTime.Value = mailBox.ReceiveBeginTime.Value;
            }
            else
            {
                combo_receiveTime.SelectedIndex = 0;
            }
            if (mailBox.ReceiveTimer > 0)
            {
                txt_ReceiveTimer.Enabled = true;
                txt_ReceiveTimer.Text = mailBox.ReceiveTimer.ToString();
            }
            else
            {
                txt_ReceiveTimer.Enabled = false;
            }
            if (!string.IsNullOrWhiteSpace(mailBox.MailBoxId))
            {
                cbox_ProtocolTypeId.Enabled = false;
            }
            else
            {
                cbox_ProtocolTypeId.Enabled = true;
            }
            if (mailBox.IsDefault == true) radio_default_yes.Checked = true;
            else radio_default_no.Checked = true;
            combo_receiveTime.Enabled = mailBox.MailCount <= 0;
            dataPicker_ReceiveBeginTime.Enabled = mailBox.MailCount <= 0;
            txt_PopServer.Enabled = mailBox.MailCount <= 0;
            txt_PopPort.Enabled = mailBox.MailCount <= 0;
            txt_SmtpPort.Enabled = mailBox.MailCount <= 0;
            txt_SmtpServer.Enabled = mailBox.MailCount <= 0;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            int.TryParse(txt_ReceiveTimer.Text.Trim(), out int receiverTimer);
            int.TryParse(txt_PopPort.Text.Trim(), out int popPort);
            int.TryParse(txt_SmtpPort.Text.Trim(), out int smtpPort);
            int.TryParse(cbox_KeepDays.SelectedValue.ToString(), out int keepDay);
            int.TryParse(cbox_ProtocolTypeId.SelectedValue.ToString(), out int protocolTypeId);
            MailBox mailBox = new MailBox
            {
                MailAddress = txt_MailAddress.Text.Trim(),
                MailPassword = txt_MailPassword.Text.Trim(),
                NickName = txt_NickName.Text.Trim(),
                PopPort = popPort,
                PopServer = txt_PopServer.Text.Trim(),
                ProtocolTypeId = protocolTypeId,
                SendTimer = 15,
                ShowName = txt_ShowName.Text.Trim(),
                Sorting = 1,
                SmtpPort = smtpPort,
                SmtpServer = txt_SmtpServer.Text.Trim(),
                KeepDays = keepDay,
                Cc = txt_cc.Text.Trim(),
                Bcc = txt_bcc.Text.Trim(),
                IsDefault = radio_default_yes.Checked
            };
            if (combo_receiveTime.SelectedIndex != 0)//自定义收取日期
            {
                mailBox.ReceiveBeginTime = DateTime.Parse(dataPicker_ReceiveBeginTime.Value.ToShortDateString());
            }
            else { mailBox.ReceiveBeginTime = null; }
            var node = tree_mailbox.SelectedNode;
            if (!string.IsNullOrWhiteSpace(SelectedMailBoxId))//选中修改
            {
                mailBox.MailBoxId = SelectedMailBoxId;
            }
            else
            {
                mailBox.Sorting = tree_mailbox.Nodes.Count + 1;
            }
            if (string.IsNullOrWhiteSpace(mailBox.MailAddress))
            {
                MessageBox.Show("邮箱地址不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(mailBox.MailBoxId) && string.IsNullOrWhiteSpace(mailBox.MailPassword))
            {//新增时不能为空
                MessageBox.Show("邮箱密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(cbox_ProtocolTypeId.SelectedItem.ToString()))
            {
                MessageBox.Show("协议类型不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(mailBox.PopServer))
            {
                MessageBox.Show("收件服务器不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (popPort < 1)
            {
                MessageBox.Show("收件服务器端口不能为空且必须为数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(mailBox.SmtpServer))
            {
                MessageBox.Show("发件服务器不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (smtpPort < 1)
            {
                MessageBox.Show("发件服务器端口不能为空且必须为数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (chk_ReceiveTimer.Checked)
            {
                if (receiverTimer < 1)
                {
                    MessageBox.Show("定时收取间隔时间必须大于零且为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                mailBox.ReceiveTimer = receiverTimer;
            }
            else
            {
                mailBox.ReceiveTimer = -1;
            }

            string name = mailBox.MailAddress.Split('@')[0];
            if (string.IsNullOrWhiteSpace(mailBox.NickName)) mailBox.NickName = name;
            if (string.IsNullOrWhiteSpace(mailBox.ShowName)) mailBox.ShowName = name;
            bool isAdd = string.IsNullOrWhiteSpace(mailBox.MailBoxId);
            var result = isAdd ? mailBoxService.AddMailBox(loginInfo, mailBox) : mailBoxService.UpdateMailBox(loginInfo, mailBox);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                MessageBox.Show("保存成功", "提示");
                var data = result.Data as MailBox;
                SelectedMailBoxId = data.MailBoxId;
                LoadTreeMailBox();
                if (isAdd) { mailAlert.MailBoxInserted(loginInfo.OCode, data.MailBoxId); }
                else { mailAlert.MailBoxUpdated(loginInfo.OCode, data.MailBoxId); }
            }
            else
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void toolStripBtn_add_Click(object sender, EventArgs e)
        {
            InitAddForm();
        }

        private void toolStripBtn_delete_Click(object sender, EventArgs e)
        {
            var node = tree_mailbox.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请选择要删除的邮箱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (MessageBox.Show("确定删除邮箱" + node.Text + "?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                return;
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            var result = mailBoxService.RemoveMailBox(loginInfo, node.Name);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                InitAddForm();
                tree_mailbox.Nodes.Remove(node);
                mailAlert.MailBoxDeleted(loginInfo.OCode, node.Name);
                MessageBox.Show("删除成功", "提示");
            }
            else
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void tree_mailbox_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (SelectedMailBoxId != e.Node.Name)//有改变选中节点
            {
                SelectedMailBoxId = e.Node.Name;
                if (e.Node.Tag != null)
                {
                    var nodeTag = (TreeNodeTag)e.Node.Tag;
                    if (nodeTag.NodeType == MailBoxFolderTreeNodeTypeEnum.MailBox.ToString())
                    {
                        LoadMailBoxFormData((MailBox)nodeTag.Data);
                    }
                }
            }
        }
        private void combo_receiveTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = (SkinComboBox)sender;
            switch (obj.SelectedIndex)
            {
                case 0://全部
                    dataPicker_ReceiveBeginTime.Enabled = false;
                    dataPicker_ReceiveBeginTime.Visible = false;
                    break;
                default://自定义
                    dataPicker_ReceiveBeginTime.Enabled = true;
                    dataPicker_ReceiveBeginTime.Visible = true;
                    break;
            }
        }

        private void chk_ReceiveTimer_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReceiveTimer.Enabled = chk_ReceiveTimer.Checked;
        }
    }
}
