using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace YANAN.Mail.Client
{
    using CCWin.SkinControl;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using System.ComponentModel;

    /// <summary>
    /// 窗体 - 邮件系统主界面
    /// </summary>
    public partial class MailMainForm : BaseChildForm
    {
        private bool isHide = false;
        private DateTime? LastConnectTimeFromPb = null;
        /// <summary>
        /// 邮件主窗体
        /// </summary>
        public MailMainForm(bool hide = false)
        {
            isHide = hide;
            InitializeComponent();
            ShowInTaskbar = false;
            InitControlEvent();
            Size = new Size(0, 0);
            Hide();
            FormLoad();
        }
        private void FormLoad()
        {
            Thread thread = new Thread(new ThreadStart(LoadCustomerInfo))
            {
                IsBackground = true
            };
            thread.Start();
            tabControlSelectPage();
            Filter.FilterHost.Start();
            Core.Starter.Start(new Pop3ReceiverLoader(), new ImapReceiverLoader(), new SmtpSenderLoader(), null);
            if (handleIdPb.HasValue)
                SendMessage(handleIdPb.Value, 0, 9906, Handle);
            if (!isHide)
            {
                //WindowState = FormWindowState.Normal;
                Show();
            }
            FormMailMain = this;
            if (handleIdPb.HasValue)
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
                {
                    Enabled = true,
                    Interval = 120000//2分钟
                };
                timer.Tick += new EventHandler(timer_sendUnread_Tick);
                timer.Start();
                System.Windows.Forms.Timer timerHeartBeat = new System.Windows.Forms.Timer
                {
                    Enabled = true,
                    Interval = 1000
                };
                timerHeartBeat.Tick += new EventHandler(timer_HeartBeat_Tick);
                timerHeartBeat.Start();
                LastConnectTimeFromPb = DateTime.Now;
            }
        }
        /// <summary>
        /// 接受PB业务系统发送的消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 9906://显示窗体
                    if (m.LParam != null)
                    {
                        int param = m.LParam.ToInt32();
                        if (param == 1)
                        {
                            Show();
                            //WindowState = FormWindowState.Normal;
                            tabControlSelectPage();
                        }
                        else if (param == 0)
                        {
                            //WindowState = FormWindowState.Minimized;
                            Hide();
                        }
                    }
                    break;
                case 9908://退出系统
                    if (m.LParam != null)
                    {
                        if (m.LParam.ToInt32() == 1)
                        {
                            this.CloseAndExit();
                        }
                    }
                    break;
                case 9909://心跳包
                    if (m.LParam != null)
                    {
                        LastConnectTimeFromPb = DateTime.Now;
                    }
                    break;
            }
        }

        #region  控件事件

        private void LoadCustomerInfo()
        {
            mailService.SyncCustomerContacts(CurrentUserInfo.GetLoginedUserInfo());
        }
        /// <summary>
        /// 让程序不显示在alt+Tab视图窗体中
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt+Tab
                return cp;
            }
        }

        /// <summary>
        /// 左侧tab切换事件(邮箱、客户、联系人)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_mainleft_SelectedIndexChanged(object sender, EventArgs e)
        {
            SkinTabControl tabControl = (SkinTabControl)sender;
            tabControlSelectPage(tabControl.SelectedIndex);
        }
        private void timer_HeartBeat_Tick(object sender, EventArgs e)
        {
            var timer = (System.Windows.Forms.Timer)sender;
            timer.Stop();
            if (LastConnectTimeFromPb.HasValue)
            {
                if (LastConnectTimeFromPb.Value.AddMinutes(3) < DateTime.Now)
                {//超过2分钟未收到PB程序的心跳包则退出应用程序
                    this.CloseAndExit();
                    return;
                }
            }
            timer.Start();
        }
        private void timer_sendUnread_Tick(object sender, EventArgs e)
        {
            var timer = (System.Windows.Forms.Timer)sender;
            timer.Stop();
            SendUnReadCountToPb();
            timer.Start();
        }
        private void toolBtnReceiveClick(object sender, EventArgs e)
        {
            List<string> mailBoxIds = new List<string>();
            void getAllMailBox()
            {
                var nodes = tree_mailbox.Nodes;
                if (nodes.Count > 0)
                {
                    foreach (TreeNode node in nodes)
                    {
                        if (node != null && node.Tag != null)
                        {
                            TreeNodeTag nodeTag = (TreeNodeTag)node.Tag;
                            if (nodeTag.NodeType == MailBoxFolderTreeNodeTypeEnum.MailBox.ToString())
                            {
                                var box = (MailBox)nodeTag.Data;
                                mailBoxIds.Add(box.MailBoxId);
                            }
                        }
                    }
                }
            }
            var selectedNode = tree_mailbox.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                TreeNodeTag nodeTag = (TreeNodeTag)selectedNode.Tag;
                switch (nodeTag.NodeType)
                {
                    case "MailFolder":
                        var folder = (MailFolder)nodeTag.Data;
                        mailBoxIds.Add(folder.MailBoxId);
                        break;
                    case "MailBox":
                        var box = (MailBox)nodeTag.Data;
                        mailBoxIds.Add(box.MailBoxId);
                        break;
                    default:
                        getAllMailBox();
                        break;
                }
            }
            else { getAllMailBox(); }
            if (mailBoxIds.Count < 1)
            {
                return;
            }
            string[] ids = mailBoxIds.ToArray();
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            mailAlert.ReceiveMail(loginInfo.OCode, ids);
            ProcessOperatorPercent percentProcess = new ProcessOperatorPercent();
            int receiveCount = ids.Length, completedCount = 0, receiveMailCount = 0, errorCount = 0; string errorMsg = string.Empty;
            percentProcess.BackgroundWork = new Action<Action<int>>((action) =>
            {
                percentProcess.MessageInfo = "邮件收取中...";
                while (true)
                {
                    var dicStatus = mailAlert.GetMailBoxReceiveStatus(loginInfo.OCode, ids);
                    foreach (var status in dicStatus)
                    {
                        if (status.Value != null)
                        {
                            foreach (var item in status.Value)
                            {
                                percentProcess.MessageInfo = item.Message;

                                completedCount = item.TaskCount > 0 ? item.CompletedCount / item.TaskCount : 1;
                                if (completedCount > 99) completedCount = 99;
                                if (item.StatusCode == StatusCode.End)
                                {
                                    receiveCount -= 1;
                                    receiveMailCount = item.CompletedCount;
                                }
                                else if (item.StatusCode == StatusCode.Error)
                                {
                                    errorCount += item.CompletedCount;
                                    errorMsg += "\r\n" + item.Message;
                                    receiveCount -= 1;
                                }
                            }
                        }
                    }
                    if (receiveCount < 1)
                    {
                        completedCount = 100;
                        if (receiveMailCount > 0)
                        {
                            percentProcess.MessageInfo = "共收取到" + receiveMailCount + "封新邮件";
                        }
                        else { percentProcess.MessageInfo = "没有新邮件"; }
                    }
                    action(completedCount);
                    if (receiveCount < 1) break;
                    Thread.Sleep(50);
                }
            });
            percentProcess.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>((bs, be) =>
                    {
                        if (be.BackGroundException != null)
                        {
                            MessageBox.Show(be.BackGroundException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            if (handleIdPb.HasValue)
                            {
                                SendMessage(handleIdPb.Value, 0, 9907, (IntPtr)receiveMailCount);
                            }
                            if (tabControl_mainleft.SelectedIndex == 0 && receiveMailCount > 0)
                                LoadTreeMailBox();
                        }
                    });
            percentProcess.Start();
        }

        private void toolBtnComposeClick(object sender, EventArgs e)
        {
            string receiver = string.Empty;
            if (tabControl_mainleft.SelectedIndex == 2)//联系人
            {
                if (SelectedRowIndexMailContact > -1)
                {
                    var contact = dgBindDataMailContact[SelectedRowIndexMailContact];
                    receiver += UtilityHelper.GetMailAddressText(contact.EMail, contact.ContactName);
                }
            }
            FormHelper.OpenComposeForm(ComposeActionEnum.Write, string.Empty, receiver);
        }

        private void toolBtnReplyClick(object sender, EventArgs e)
        {
            string mailId = GetSelectedMailId();
            FormHelper.OpenComposeForm(ComposeActionEnum.Reply, mailId);
        }

        private void toolBtnReplyAllClick(object sender, EventArgs e)
        {
            string mailId = GetSelectedMailId();
            FormHelper.OpenComposeForm(ComposeActionEnum.ReplyAll, mailId);
        }

        private void toolBtnForwardClick(object sender, EventArgs e)
        {
            string mailId = GetSelectedMailId();
            FormHelper.OpenComposeForm(ComposeActionEnum.Forward, mailId);
        }

        private void toolBtnMoveClick(object sender, EventArgs e)
        {
            var mailMain = GetSelectedMailData();
            if (mailMain == null)
            {
                MessageBox.Show("请先选择邮件记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            MailActionHelper.MoveMailFolder(mailMain, (folderId) =>
            {
                switch (tabControl_mainleft.SelectedIndex)
                {
                    case 0:
                    case 1:
                        var mailListForm = (MailListForm)splitContainer_left.Panel2.Controls[0];
                        mailListForm.ReloadData();
                        break;
                    case 2:
                        break;
                }
            });
        }

        private void toolBtnDeleteClick(object sender, EventArgs e)
        {
            switch (tabControl_mainleft.SelectedIndex)
            {
                case 0:
                case 1:
                    string mailId = GetSelectedMailId();
                    MailActionHelper.Remove(mailId, (result) =>
                    {
                        var mailListForm = (MailListForm)splitContainer_left.Panel2.Controls[0];
                        mailListForm.ReloadData();
                    });
                    break;
                case 2:
                    if (SelectedRowIndexMailContact > -1)
                    {
                        var data = dgBindDataMailContact[SelectedRowIndexMailContact];
                        var result = mailService.RemoveMailContact(CurrentUserInfo.GetLoginedUserInfo(), new int[] { data.MailContactId });
                        if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                        {
                            LoadGridContact();
                            var form = GetSetRightForm(tabControl_mainleft.SelectedIndex) as MailContactManageForm;
                            form.LoadData();
                            MessageBox.Show("删除成功", "提示");
                        }
                        else
                        {
                            MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择要删除的联系人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnQuitClick(object sender, EventArgs e)
        {
            if (ParentForm is MainForm)
            {
                if (MessageBox.Show("你确定要退出系统吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    this.CloseAndExit();
            }
            else
            {
                Hide();//嵌套不释放
                WindowState = FormWindowState.Minimized;
                if (handleIdPb.HasValue)
                    SendMessage(handleIdPb.Value, 0, 9908, (IntPtr)1);
            }
        }

        /// <summary>
        /// 事件 - (邮箱)设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnSettingClick(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            if (DialogResult.OK == settingForm.ShowDialog())
            {
                if (tabControl_mainleft.SelectedIndex == 0)
                    LoadTreeMailBox();
            }
        }

        /// <summary>
        /// 客户邮件手动归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnArchiveClick(object sender, EventArgs e)
        {
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();

            ProcessOperatorPercent percentProcess = new ProcessOperatorPercent();
            percentProcess.BackgroundWork = new Action<Action<int>>((action) =>
            {
                percentProcess.MessageInfo = "邮件归档中...";
                action(1);
                mailService.SyncCustomerContacts(loginInfo);
                action(5);
                int processNum = 5;
                var contactsList = mailService.GetCustomerContactsList(loginInfo);
                if (contactsList.Count < 1) action(100);
                var groupCustomer = contactsList.GroupBy(x => x.ClientId);
                int percent = 95 / groupCustomer.Count();
                foreach (var group in groupCustomer)
                {
                    var list = group.ToList();
                    if (list.Count > 0)
                    {
                        percentProcess.MessageInfo = "客户[" + list[0].ClientName + "]邮件正在归档...";
                        mailService.ArchiveCustomerMail(loginInfo, list);
                        percentProcess.MessageInfo = "客户[" + list[0].ClientName + "]邮件归档完成...";
                    }
                    processNum += percent;
                    if (processNum > 100) processNum = 99;
                    if (processNum == 100) percentProcess.MessageInfo = "客户邮件归档完成";
                    action(processNum);
                }
                if (processNum < 100) { processNum = 100; percentProcess.MessageInfo = "客户邮件归档完成"; action(processNum); }

            });
            percentProcess.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>((bs, be) =>
            {
                if (be.BackGroundException != null)
                {
                    MessageBox.Show(be.BackGroundException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    tabControlSelectPage(tabControl_mainleft.SelectedIndex);
                }
            });
            percentProcess.Start();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnRefreshClick(object sender, EventArgs e)
        {
            tabControlSelectPage(tabControl_mainleft.SelectedIndex);
        }

        /// <summary>
        /// 邮箱树节点收起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_mailbox_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                e.Node.SelectedImageKey = "collapse.png";
            e.Node.ImageKey = e.Node.SelectedImageKey;
        }

        /// <summary>
        /// 邮箱树节点展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_mailbox_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                e.Node.SelectedImageKey = "expand.png";
            e.Node.ImageKey = e.Node.SelectedImageKey;
        }

        /// <summary>
        /// 事件 - 邮箱树节点鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_mailbox_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeMailBoxNodeLeftMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                TreeMailBoxNodeRightMouseClick(e);
            }

        }

        /// <summary>
        /// 事件 - 客户树节点鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_customer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            TreeNodeClickEvent_Customer(e.Node);
        }

        //邮箱树节点编辑
        private void tree_mailbox_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var node = e.Node;
            TreeNodeTag nodeTag = (TreeNodeTag)node.Tag;//只有自定义文件夹才可编辑
            if (nodeTag.NodeType != MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString())
            {
                node.EndEdit(true);
            }
            else
            {
                var folder = (MailFolder)nodeTag.Data;
                if (folder.MailType != (int)MailFolderEnum.Customize) { node.EndEdit(true); }
            }
        }
        private void tree_mailbox_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string newText = e.Label;//获取新文本
            string oldText = e.Node.Text;//获取原来的文本
            if (!string.IsNullOrWhiteSpace(newText) && !newText.Equals(oldText))//
            {
                var node = e.Node;
                TreeNodeTag nodeTag = (TreeNodeTag)node.Tag;
                var folder = (MailFolder)nodeTag.Data;
                folder.FolderName = newText;
                var result = mailBoxService.AddMailFolder(CurrentUserInfo.GetLoginedUserInfo(), folder);
                if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                {
                    nodeTag.Data = result.Data;
                    e.Node.Tag = nodeTag;
                }
                else
                {
                    MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    e.Node.BeginEdit();
                }
            }
        }

        /// <summary>
        /// 邮箱树节点鼠标左键点击
        /// </summary>
        /// <param name="e"></param>
        private void TreeMailBoxNodeLeftMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;
            var oldSelectedNode = tree_mailbox.SelectedNode;
            //if (oldSelectedNode == e.Node) return;
            TreeNodeClickEvent_MailBox(e.Node);
        }

        /// <summary>
        /// 邮箱树节点鼠标右键点击
        /// </summary>
        /// <param name="e"></param>
        private void TreeMailBoxNodeRightMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;
            tree_mailbox.SelectedNode = e.Node;
            TreeNodeTag nodeTag = (TreeNodeTag)e.Node.Tag;
            ContextMenuStrip contextMenu = new ContextMenuStrip() { Name = "contextMenu_mailBox" };
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem = new ToolStripMenuItem
            {
                Name = "setAllToRead",
                Text = "全部设为已读"
            };
            menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
            contextMenu.Items.Add(menuItem);
            switch (nodeTag.NodeType)
            {
                case "MailFolder":
                    menuItem = new ToolStripMenuItem
                    {
                        Name = "addMailFolder",
                        Text = "新建文件夹"
                    };
                    menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    var folder = (MailFolder)nodeTag.Data;
                    if (folder.MailType == (int)MailFolderEnum.Customize)//自定义文件夹才可重命名及删除
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "renameMailFolder",
                            Text = "重命名"
                        };
                        menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "removeMailFolder",
                            Text = "删除文件夹"
                        };
                        menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    break;
                case "MailBox":
                    contextMenu.Items.Remove(menuItem);
                    menuItem = new ToolStripMenuItem
                    {
                        Name = "addMailFolder",
                        Text = "新建文件夹"
                    };
                    menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    break;
                case "MailLabel":
                    if (e.Node.Name == "mail_label")
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "addMailLabel",
                            Text = "新建标签"
                        };
                        menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    else
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "editMailLabel",
                            Text = "管理标签"
                        };
                        menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "removeMailLabel",
                            Text = "删除标签"
                        };
                        menuItem.Click += new EventHandler(tree_mailbox_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    break;
            }
            if (contextMenu.Items.Count > 0)
            {
                e.Node.ContextMenuStrip = contextMenu;
            }
        }

        /// <summary>
        /// 邮箱树节点右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_mailbox_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var node = tree_mailbox.SelectedNode;
            TreeNodeTag nodeTag = (TreeNodeTag)node.Tag;
            ResponseResult result;
            switch (menuItem.Name)
            {
                case "setAllToRead":
                    result = mailBoxService.SetMailFolderRead(CurrentUserInfo.GetLoginedUserInfo(), node.Name);
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                    {
                        var form = (MailListForm)splitContainer_left.Panel2.Controls[0];
                        form.ReloadData();
                    }
                    break;
                case "addMailFolder":
                    string parentId = string.Empty, boxId = string.Empty;
                    int sort = node.Nodes.Count;
                    if (sort > 0)
                    {
                        var lastTreeNode = node.Nodes[sort - 1];
                        var folder = (MailFolder)((TreeNodeTag)lastTreeNode.Tag).Data;
                        if (sort < folder.Sorting) sort = folder.Sorting + 1;
                    }
                    else { sort += 1; }
                    if (nodeTag.NodeType == MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString())
                    {
                        var folder = (MailFolder)nodeTag.Data; parentId = folder.MailFolderId;
                        boxId = folder.MailBoxId;
                    }
                    else if (nodeTag.NodeType == MailBoxFolderTreeNodeTypeEnum.MailBox.ToString())
                    {
                        var box = (MailBox)nodeTag.Data;
                        boxId = box.MailBoxId;
                    }
                    var addNode = new TreeNode
                    {
                        Name = "newfolder",
                        Text = "新建文件夹",
                        Tag = new TreeNodeTag
                        {
                            NodeType = MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString(),
                            Data = new MailFolder
                            {
                                ParentId = parentId,
                                Sorting = sort,
                                MailBoxId = boxId
                            }
                        },
                        ImageKey = "mail_unread.png",
                        SelectedImageKey = "mail_unread.png"
                    };
                    node.Nodes.Add(addNode);
                    if (!node.IsExpanded)
                        node.Expand();
                    SetMailFolderTreeNodeImageIcon(node, (int)MailFolderEnum.Customize);
                    addNode.BeginEdit();
                    break;
                case "renameMailFolder":
                    node.BeginEdit();
                    break;
                case "removeMailFolder":
                    if (node.Nodes.Count > 0)
                    {
                        MessageBox.Show("无法删除存在下级文件夹的文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (MessageBox.Show("确定要删除此文件夹嘛?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        result = mailBoxService.RemoveMailFolder(CurrentUserInfo.GetLoginedUserInfo(), node.Name);
                        if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                        {
                            var parentNode = node.Parent;

                            tree_mailbox.Nodes.Remove(node);
                            var parentNodeTag = (TreeNodeTag)parentNode.Tag;
                            if (parentNodeTag.NodeType == MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString())
                                SetMailFolderTreeNodeImageIcon(parentNode, ((MailFolder)parentNodeTag.Data).MailType);
                        }
                    }
                    break;
                case "addMailLabel":
                case "editMailLabel":
                    SettingForm settingForm = new SettingForm(MailSettingTabPageEnum.Label);//打开标签设置页
                    if (DialogResult.OK == settingForm.ShowDialog())
                    {
                        LoadTreeMailBox();
                    }
                    break;
                case "removeMailLabel":
                    if (nodeTag.NodeType == MailBoxFolderTreeNodeTypeEnum.MailLabel.ToString() && node.Name.ToLower() != "mail_label")
                    {
                        result = mailService.RemoveMailLabel(CurrentUserInfo.GetLoginedUserInfo(), new string[] { node.Name });
                        if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                        {
                            LoadTreeMailBox();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 联系人搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_contact_search_Search(object sender, EventArgs e)
        {
            LoadGridContact();
        }
        private void txt_contact_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;//不换行
                LoadGridContact();
            }
        }
        /// <summary>
        /// 邮箱联系人鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrid_contact_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = tabControl_mainleft.SelectedIndex;
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex < 0) return;
                SelectedRowIndexMailContact = e.RowIndex;
                Form form = GetSetRightForm(index);
                if (index == 2)
                {
                    var contact = dgBindDataMailContact[e.RowIndex];
                    MailContactManageForm manageForm = form as MailContactManageForm;
                    manageForm.LoadData(contact.MailContactId);
                }
            }
        }

        #endregion 控件事件

        /// <summary>
        /// 窗体初始化时初始化控件事件绑定
        /// </summary>
        private void InitControlEvent()
        {
            toolBtnQuit.Click += new EventHandler(toolBtnQuitClick);
            toolBtnSetting.Click += new EventHandler(toolBtnSettingClick);
            toolStripBtn_delete.Click += new EventHandler(toolBtnDeleteClick);
            toolBtnMove.Click += new EventHandler(toolBtnMoveClick);
            toolBtnForward.Click += new EventHandler(toolBtnForwardClick);
            toolBtnReplyAll.Click += new EventHandler(toolBtnReplyAllClick);
            toolBtnReply.Click += new EventHandler(toolBtnReplyClick);
            toolBtnCompose.Click += new EventHandler(toolBtnComposeClick);
            toolBtnReceive.Click += new EventHandler(toolBtnReceiveClick);
            toolBtnRefresh.Click += new EventHandler(toolBtnRefreshClick);
            toolBtnArchive.Click += new EventHandler(toolBtnArchiveClick);
            tree_customer.NodeMouseClick += new TreeNodeMouseClickEventHandler(tree_customer_NodeMouseClick);
            tabControl_mainleft.SelectedIndexChanged += new EventHandler(tabControl_mainleft_SelectedIndexChanged);
            tree_mailbox.NodeMouseClick += new TreeNodeMouseClickEventHandler(tree_mailbox_NodeMouseClick);
            tree_mailbox.AfterLabelEdit += new NodeLabelEditEventHandler(tree_mailbox_AfterLabelEdit);
            txt_contact_search.SkinTxt.KeyDown += new KeyEventHandler(txt_contact_search_KeyDown);
            txt_contact_search.IconClick += new EventHandler(txt_contact_search_Search);
            dgrid_contact.CellMouseDown += new DataGridViewCellMouseEventHandler(dgrid_contact_CellMouseClick);
        }
        /// <summary>
        /// 左侧邮箱树节点点击执行事件
        /// </summary>
        /// <param name="node"></param>
        private void TreeNodeClickEvent_MailBox(TreeNode node)
        {
            if (tree_mailbox.SelectedNode != node)
                tree_mailbox.SelectedNode = node;
            TreeNodeTag nodeTag = (TreeNodeTag)node.Tag;
            var form = (MailListForm)splitContainer_left.Panel2.Controls[0];//邮箱树节点点击事件必然当前显示的窗体为邮件列表
            switch (nodeTag.NodeType)
            {
                case "MailFolder":
                    var folder = (MailFolder)nodeTag.Data;
                    form.LoadData(folder.MailFolderId, folder.MailType);
                    break;
                case "MailBox":
                    form.LoadData();
                    break;
                case "MailLabel":
                    if (node.Name != "mail_label")
                    {
                        var nodeTagData = (MailLabel)nodeTag.Data;
                        form.LoadData(null, null, ((MailLabel)nodeTag.Data).MailLabelId);
                    }
                    else
                    {
                        form.LoadData(null, null, "all");
                    }
                    break;
                case "Other":
                    switch (node.Name)
                    {
                        case "all_noread_mail":
                            form.LoadData(null, null, null, true);
                            break;
                        case "all_settop_mail":
                            form.LoadData(null, null, null, false, true);
                            break;
                        default:
                            form.LoadData();
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 左侧客户树节点点击执行事件
        /// </summary>
        /// <param name="node"></param>
        private void TreeNodeClickEvent_Customer(TreeNode node)
        {
            if (tree_customer.SelectedNode != node)
                tree_customer.SelectedNode = node;
            if (splitContainer_left.Panel2.Controls[0] is MailListForm)
            {
                var form = splitContainer_left.Panel2.Controls[0] as MailListForm;
                form.LoadDataCustomer(node.Name);
            }
        }

        private void tabControlSelectPage(int index = 0)
        {
            if (index < 0) index = 0;
            tabControl_mainleft.SelectedIndex = index;
            var panel_right = splitContainer_left.Panel2;
            var form = GetSetRightForm(index);
            LoadLeftData(index);
            bool formExist = false;
            if (form.Tag != null) formExist = true;
            toolBtnArchive.Visible = false;
            switch (index)
            {
                case 0://邮箱
                    if (formExist)
                        (form as MailListForm).ReloadData();
                    break;
                case 1://客户
                    toolBtnArchive.Visible = true;
                    if (formExist)
                        (form as MailListForm).ReloadData();
                    break;
                case 2://联系人
                    txt_contact_search.Text = string.Empty;
                    if (formExist)
                        (form as MailContactManageForm).LoadData();
                    break;
            }
        }
        private void LoadLeftData(int? index = null)
        {
            if (index.HasValue == false) index = tabControl_mainleft.SelectedIndex;
            switch (index.Value)
            {
                case 0:
                    LoadTreeMailBox();
                    break;
                case 1:
                    LoadTreeCustomer();
                    break;
                case 2:
                    LoadGridContact();
                    break;
            }
        }
        /// <summary>
        /// 获取(加载)邮箱/客户/联系人切换右侧窗体
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private Form GetSetRightForm(int tabIndex = 0)
        {
            var panel_right = splitContainer_left.Panel2;
            string key;
            Form form = null;
            MailListForm mailListForm;
            switch (tabIndex)
            {
                case 0://邮箱
                    key = "form_maillist_mailbox";
                    if (!panel_right.Controls.ContainsKey(key))
                    {
                        panel_right.Controls.Clear();
                        mailListForm = new MailListForm() { Name = key, Dock = DockStyle.Fill, TopLevel = false, Tag = null };
                        mailListForm.SetMailReadStatusDelegate += SetMailReadCallback;
                        panel_right.Controls.Add(mailListForm);
                        mailListForm.Show();
                    }
                    else
                    {
                        mailListForm = (MailListForm)panel_right.Controls[key];
                        mailListForm.Tag = "exist";
                    }
                    form = mailListForm;
                    break;
                case 1://客户
                    key = "form_maillist_customer";
                    if (!panel_right.Controls.ContainsKey(key))
                    {
                        panel_right.Controls.Clear();
                        mailListForm = new MailListForm() { Name = key, Dock = DockStyle.Fill, TopLevel = false, Tag = null };
                        panel_right.Controls.Add(mailListForm);
                        mailListForm.Show();
                    }
                    else
                    {
                        mailListForm = (MailListForm)panel_right.Controls[key];
                        mailListForm.Tag = "exist";
                    }
                    form = mailListForm;
                    break;
                case 2://联系人
                    key = "form_maillist_contact_manage";
                    MailContactManageForm contactManageForm;
                    if (!panel_right.Controls.ContainsKey(key))
                    {
                        panel_right.Controls.Clear();
                        contactManageForm = new MailContactManageForm(0, this) { Name = key, Dock = DockStyle.Fill, TopLevel = false, Tag = null };
                        panel_right.Controls.Add(contactManageForm);
                        contactManageForm.Show();
                    }
                    else
                    {
                        contactManageForm = (MailContactManageForm)panel_right.Controls[key];
                        contactManageForm.Tag = "exist";
                    }
                    form = contactManageForm;
                    break;
            }
            return form;
        }
        /// <summary>
        /// 获取邮件列表选中的邮件ID
        /// </summary>
        /// <returns></returns>
        private string GetSelectedMailId()
        {
            string mailId = string.Empty;
            switch (tabControl_mainleft.SelectedIndex)
            {
                case 0:
                case 1:
                    if (splitContainer_left.Panel2.Controls.Count > 0)
                    {
                        var mailListForm = (MailListForm)splitContainer_left.Panel2.Controls[0];
                        mailId = mailListForm.SelectedMailMainId;
                    }
                    break;
                case 2:

                    break;
            }
            return mailId;
        }
        /// <summary>
        /// 获取邮件列表选中的行数据
        /// </summary>
        /// <returns></returns>
        private MailMain GetSelectedMailData()
        {
            MailMain mail = null;
            switch (tabControl_mainleft.SelectedIndex)
            {
                case 0:
                case 1:
                    if (splitContainer_left.Panel2.Controls.Count > 0)
                    {
                        var mailListForm = (MailListForm)splitContainer_left.Panel2.Controls[0];
                        mail = mailListForm.SelectedRowData;
                    }
                    break;
                case 2:

                    break;
            }
            return mail;
        }
        /// <summary>
        /// 加载邮箱列表
        /// </summary>
        public void LoadTreeMailBox(bool selectAllUnRead = false)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            int totalUnreadCount = 0;
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            var list = mailBoxService.GetListMailBoxCurrentUser(loginInfo);
            IList<MailFolder> mailFolders = mailBoxService.GetListMailFolderByMailBoxIds(list.Select(x => x.MailBoxId).ToArray());
            Action<IList<MailFolder>, TreeNode> loadMailFolderTreeNodeAction = null;
            TreeNode node;
            //邮箱文件夹加递归
            loadMailFolderTreeNodeAction = ((items, parentNode) =>
            {
                if (items == null || items.Count < 1) return;
                List<MailFolder> childrens = new List<MailFolder>();
                var nodeTag = (TreeNodeTag)parentNode.Tag;
                if (nodeTag.NodeType != MailBoxFolderTreeNodeTypeEnum.MailBox.ToString())
                {
                    childrens = items.Where(x => x.ParentId == parentNode.Name).ToList();
                }
                else
                {
                    childrens = items.Where(x => x.MailBoxId == parentNode.Name && string.IsNullOrWhiteSpace(x.ParentId)).ToList();
                }
                if (childrens != null && childrens.Count() > 0)
                {
                    foreach (var item in childrens)
                    {
                        TreeNode node_child = new TreeNode
                        {
                            Name = item.MailFolderId,
                            Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString(), Data = item },
                            Text = item.FolderName
                        };
                        if (item.UnreadCount > 0)
                        {
                            totalUnreadCount += item.UnreadCount;
                            node_child.Text = node_child.Text + " (" + item.UnreadCount + ")";//<span style='color:#ff0000'></span>
                        }
                        parentNode.Nodes.Add(node_child);
                        loadMailFolderTreeNodeAction(items, node_child);
                        SetMailFolderTreeNodeImageIcon(node_child, item.MailType);
                    }
                }
            });
            int i = 0;
            TreeNode firstNodeMailBox = null;
            foreach (var item in list)
            {
                node = new TreeNode
                {
                    Name = item.MailBoxId,
                    Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailBox.ToString(), Data = item },
                    Text = item.ShowName,
                    ImageKey = "collapse.png"
                };
                node.SelectedImageKey = node.ImageKey;
                nodes.Add(node);
                loadMailFolderTreeNodeAction(mailFolders, node);
                if (i == 0)
                {
                    node.ImageKey = "expand.png";
                    node.Expand();
                    firstNodeMailBox = node;
                }
                i++;
            }
            node = new TreeNode
            {
                Name = "global_custom_folder",
                Tag = new TreeNodeTag { NodeType = string.Empty, Data = null },
                Text = "常用功能",
                ImageKey = "expand.png",
                SelectedImageKey = "expand.png"
            };
            var allUnReadNode = new TreeNode
            {
                Name = "all_noread_mail",
                Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.Other.ToString(), Data = totalUnreadCount },
                Text = "未读邮件" + (totalUnreadCount > 0 ? " (" + totalUnreadCount + ")" : ""),
                ImageKey = "mail_unread.png",
                SelectedImageKey = "mail_unread.png"
            };
            TreeNode[] treeNodes = new TreeNode[] {
               allUnReadNode
                ,new TreeNode{
                     Name = "all_settop_mail",
                    Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.Other.ToString() },
                    Text = "置顶邮件",
                    ImageKey = "mail_set_top.png",
                    SelectedImageKey = "mail_set_top.png"
                }
            };
            node.Nodes.AddRange(treeNodes);
            var labels = mailService.GetListSelfMailLabel(loginInfo, new QueryParameter());
            var labelNode = new TreeNode
            {
                Name = "mail_label",
                Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailLabel.ToString() },
                Text = "标签邮件",
                ImageKey = "mail_label.png",
                SelectedImageKey = "mail_label.png"
            };
            if (!labels.IsNull() && labels.Items.Count > 0)
            {
                labelNode.ImageKey = "collapse.png";
                labelNode.SelectedImageKey = labelNode.ImageKey;
                foreach (var label in labels.Items)
                {
                    labelNode.Nodes.Add(new TreeNode
                    {
                        Name = label.MailLabelId,
                        Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailLabel.ToString(), Data = label },
                        Text = label.MailLabelName,
                        ForeColor = !string.IsNullOrWhiteSpace(label.Color) ? ColorTranslator.FromHtml(label.Color) : SystemColors.Window,
                        ImageKey = "mail_label.png",
                        SelectedImageKey = "mail_label.png"
                    });
                }
            }
            node.Nodes.Add(labelNode);
            node.Expand();
            nodes.Insert(0, node);
            var selectedNode = tree_mailbox.SelectedNode;
            tree_mailbox.Nodes.Clear();
            tree_mailbox.Nodes.AddRange(nodes.ToArray());
            tree_mailbox.LabelEdit = true;
            if (selectAllUnRead == true || (selectedNode == null && totalUnreadCount > 0))
            {
                selectedNode = allUnReadNode;
            }
            else if (selectedNode == null || totalUnreadCount < 1)
            {//如果没有未读邮件则选择第一个邮箱的收件箱
                if (firstNodeMailBox != null && firstNodeMailBox.Nodes.Count > 0)
                {
                    selectedNode = firstNodeMailBox.Nodes[0];
                }
            }
            if (selectedNode != null)
            {
                var findNode = FindNode(tree_mailbox, selectedNode.Name);
                if (findNode != null)
                {
                    tree_mailbox.SelectedNode = findNode;
                    if (findNode.Parent != null && !findNode.Parent.IsExpanded) findNode.Parent.Expand();
                    if (selectedNode.IsExpanded)
                    {
                        if (!findNode.IsExpanded)
                            findNode.Expand();
                    }
                    TreeNodeClickEvent_MailBox(findNode);
                }
            }
            SendUnReadCountToPb(totalUnreadCount);
        }
        /// <summary>
        /// 加载客户列表
        /// </summary>
        public void LoadTreeCustomer()
        {
            var contactsList = mailBoxService.GetListMailCustomer(CurrentUserInfo.GetLoginedUserInfo());
            List<TreeNode> nodes = new List<TreeNode>();
            string mailboxId = string.Empty;
            foreach (var item in contactsList)
            {
                var node = new TreeNode
                {
                    Name = item.MailFolderId.ToString(),
                    Tag = item,
                    Text = item.FolderName
                };
                nodes.Add(node);
            }
            var selectedNode = tree_customer.SelectedNode;
            tree_customer.Nodes.Clear();
            tree_customer.Nodes.AddRange(nodes.ToArray());
            if (selectedNode != null)
            {
                var findNode = FindNode(tree_customer, selectedNode.Name);
                if (findNode != null)
                {
                    if (tree_customer.SelectedNode != findNode)
                        tree_customer.SelectedNode = findNode;
                    if (findNode.Parent != null && !findNode.Parent.IsExpanded) findNode.Parent.Expand();
                    if (selectedNode.IsExpanded)
                    {
                        if (!findNode.IsExpanded)
                            findNode.Expand();
                    }
                    TreeNodeClickEvent_Customer(findNode);
                }
            }
        }
        private int SelectedRowIndexMailContact = -1;
        BindingSource bindingSourceMailContact = new BindingSource();
        BindingList<MailContact> dgBindDataMailContact = new BindingList<MailContact>();
        /// <summary>
        /// 加载联系人列表
        /// </summary>
        public void LoadGridContact(bool isReload = false)
        {
            dgrid_contact.AutoGenerateColumns = false;
            QueryParameter query = new QueryParameter();
            if (isReload == true)
            { txt_contact_search.Text = string.Empty; }
            else
            {
                if (!string.IsNullOrWhiteSpace(txt_contact_search.Text))
                {
                    query.KeyWords = txt_contact_search.Text.Trim();
                }
            }
            var contactList = mailService.GetListSelfMailContact(CurrentUserInfo.GetLoginedUserInfo(), query);
            dgBindDataMailContact.Clear();
            if (contactList != null && contactList.Items.Count > 0)
            {
                contactList.Items.ForEach(x => dgBindDataMailContact.Add(x));
            }
            bindingSourceMailContact.DataSource = dgBindDataMailContact;
            dgrid_contact.DataSource = bindingSourceMailContact;
            dgrid_contact.ClearSelection();
            SelectedRowIndexMailContact = -1;
        }
        /// <summary>
        /// 设置树节点图标
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mailType"></param>
        private void SetMailFolderTreeNodeImageIcon(TreeNode node, int mailType)
        {
            if (node.Nodes.Count > 0)
            {
                node.ImageKey = "collapse.png";
                if (node.IsExpanded)
                { node.ImageKey = "expand.png"; }
            }
            else
            {
                if (node.IsExpanded) node.Collapse();
                switch (mailType)
                {
                    case (int)MailFolderEnum.InBox:
                        node.ImageKey = "mail_receive_box.png";
                        break;
                    case (int)MailFolderEnum.OutBox:
                        node.ImageKey = "mail_send.png";
                        break;
                    case (int)MailFolderEnum.DraftBox:
                        node.ImageKey = "mail_draft.png";
                        break;
                    case (int)MailFolderEnum.TrashBox:
                        node.ImageKey = "mail_rubbish.png";
                        break;
                    case (int)MailFolderEnum.Deleted:
                        node.ImageKey = "mail_deleted.png";
                        break;
                    default:
                        node.ImageKey = "mail_unread.png";
                        break;
                }
            }
            node.SelectedImageKey = node.ImageKey;
        }
        private TreeNode FindNode(TreeView tree, string nodeName)
        {
            foreach (TreeNode tnParent in tree.Nodes)
            {
                TreeNode tnRet = FindNode(tnParent, nodeName);
                if (tnRet != null)
                    return tnRet;
            }
            return null;
        }
        private TreeNode FindNode(TreeNode tnParent, string nodeName)
        {
            if (tnParent == null) return null;
            if (tnParent.Name == nodeName) return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNode(tn, nodeName);
                if (tnRet != null) break;
            }
            return tnRet;
        }
        private void SendUnReadCountToPb(int? unread = null)
        {
            if (handleIdPb.HasValue == false) return;
            if (unread.HasValue == false)
            {
                unread = 0;
                if (tree_mailbox != null)
                {
                    var node = FindNode(tree_mailbox, "all_noread_mail");
                    if (node != null && node.Tag != null && !string.IsNullOrWhiteSpace(node.Tag.ToString()))
                    {
                        unread = int.Parse(((TreeNodeTag)node.Tag).Data.ToString());
                    }
                }
            }
            SendMessage(handleIdPb.Value, 0, 9909, (IntPtr)unread.Value);
        }
        private void SetMailReadCallback(bool read, int num)
        {
            if (tabControl_mainleft.SelectedIndex == 0)
            {
                LoadTreeMailBox();
            }
        }
    }
}
