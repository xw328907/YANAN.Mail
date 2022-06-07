using CCWin.SkinControl;
using YANAN.Mail.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using System.ComponentModel;

    /// <summary>
    /// 邮件列表窗体(含邮件查看页)
    /// </summary>
    public partial class MailListForm : BaseChildForm
    {
        BindingSource bindingSource = new BindingSource();
        BindingList<MailMain> dgBindData = new BindingList<MailMain>();
        private MailSearchDto searchDto = null;
        private int selectedRowIndex = -1;

        /// <summary>
        /// 获取选中的邮件ID,未选中返回空
        /// </summary>
        public string SelectedMailMainId
        {
            get
            {
                string val = string.Empty;
                if (selectedRowIndex > -1)
                {
                    var row = dgrid_maillist.Rows[selectedRowIndex];
                    val = row.Cells["MailMainId"].Value.ToString();
                }
                return val;
            }
        }

        /// <summary>
        /// 获取选中的邮件数据，未选中返回NULL
        /// </summary>
        public MailMain SelectedRowData
        {
            get
            {
                MailMain mail = null;
                if (selectedRowIndex > -1)
                {
                    mail = dgBindData[selectedRowIndex];
                }
                return mail;
            }
        }

        public MailListForm()
        {
            InitializeComponent();
            InitControlEvent();
        }

        public void ReloadData()
        {
            LoadDataInfo();
        }

        public void LoadData()
        {
            searchDto = null;
            LoadDataInfo();
        }

        /// <summary>
        /// 左侧邮箱树节点加载
        /// </summary>
        public void LoadData(string mailFolderId, int? mailFolderTypeId = null, string mailLabelId = "", bool allUnRead = false, bool isTop = false)
        {
            searchDto = new MailSearchDto();
            if (!string.IsNullOrWhiteSpace(mailFolderId))
            {
                searchDto.MailFolderId = mailFolderId;
            }
            if (!string.IsNullOrWhiteSpace(mailLabelId))
            {
                if (mailLabelId != "all")
                    searchDto.MailLabelId = mailLabelId;
                else searchDto.IsLabelMail = true;
                searchDto.MailFolderSourceTableCode = MailFolderSourceTableEnum.MailLabel.ToString();
            }
            if (allUnRead) searchDto.UnRead = true;
            if (isTop) searchDto.IsTopMail = true;
            if (mailFolderTypeId.HasValue)
            {
                searchDto.MailFolderTypeCode = mailFolderTypeId.Value;
            }
            searchDto.Deleted = false;
            switch (mailFolderTypeId)
            {
                case (int)MailFolderEnum.DraftBox:
                case (int)MailFolderEnum.OutBox:
                    if (dgrid_maillist.Columns["SendName"] != null)
                    {
                        dgrid_maillist.Columns["SendName"].HeaderText = "收件人";
                        dgrid_maillist.Columns["SendName"].DataPropertyName = "ReceiveName";
                        dgrid_maillist.Columns["SendName"].Name = "ReceiveName";
                    }
                    if (dgrid_maillist.Columns["Viewed"] != null)
                    {
                        dgrid_maillist.Columns["Viewed"].HeaderText = "状态";
                        dgrid_maillist.Columns["Viewed"].DataPropertyName = "MailState";
                        dgrid_maillist.Columns["Viewed"].Name = "MailState";
                    }
                    break;
                default:
                    if (mailFolderTypeId == (int)MailFolderEnum.Deleted)
                    {
                        searchDto.Deleted = true;
                    }
                    if (dgrid_maillist.Columns["MailState"] != null)
                    {
                        dgrid_maillist.Columns["MailState"].HeaderText = "已读";
                        dgrid_maillist.Columns["MailState"].DataPropertyName = "Viewed";
                        dgrid_maillist.Columns["MailState"].Name = "Viewed";
                    }
                    if (dgrid_maillist.Columns["ReceiveName"] != null)
                    {
                        dgrid_maillist.Columns["ReceiveName"].HeaderText = "发件人";
                        dgrid_maillist.Columns["ReceiveName"].DataPropertyName = "SendName";
                        dgrid_maillist.Columns["ReceiveName"].Name = "SendName";
                    }
                    break;
            }
            LoadDataInfo();
        }
        public void LoadDataCustomer(string customerId)
        {
            searchDto = new MailSearchDto() { MailFolderId = customerId, MailFolderSourceTableCode = MailFolderSourceTableEnum.Customer.ToString(), Deleted = false };
            LoadDataInfo();
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadDataInfo()
        {
            var result = mailService.GetListMailMain(CurrentUserInfo.GetLoginedUserInfo(), searchDto);
            if (result == null) result = new EntityList<MailMain> { Items = new List<MailMain>(), TotalCount = 0 };
            dgrid_maillist.AutoGenerateColumns = false;//不列出数据源所有字段(未再datagridview中定义的)
            dgBindData.Clear();
            result.Items.ForEach(x => dgBindData.Add(x));
            bindingSource.DataSource = dgBindData;
            dgrid_maillist.DataSource = bindingSource;
            dgrid_maillist.ClearSelection();
            selectedRowIndex = -1;
            splitContainer1.Panel2.Controls.Clear();
        }

        /// <summary>
        /// 初始化页面控件事件
        /// </summary>
        private void InitControlEvent()
        {
            dgrid_maillist.CellMouseDown += new DataGridViewCellMouseEventHandler(dgrid_maillist_CellMouseClick);
            dgrid_maillist.CellFormatting += new DataGridViewCellFormattingEventHandler(dgrid_maillist_CellFormatting);
        }

        /// <summary>
        /// 邮件列表单元格数据格式化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrid_maillist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgrid = (SkinDataGridView)sender;
            var column = dgrid.Columns[e.ColumnIndex];
            string field = column.DataPropertyName;
            if (e.Value == null) return;
            string value = e.Value.ToString();
            switch (field)
            {
                case "Viewed":
                    SetRowFontStyleBold(e.RowIndex, value.ToLower() != "true");
                    if (value.ToLower() != "true")
                    {
                        e.Value = Properties.Resources.unread;
                    }
                    else
                    {
                        e.Value = Properties.Resources.read;
                    }
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "Importance":
                    if (int.Parse(value) >= 3)
                        e.Value = Properties.Resources.mail_importance;
                    else
                        e.Value = Properties.Resources.transparent;
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "AttachCount":
                    if (int.Parse(value) > 0)
                        e.Value = Properties.Resources.mail_attach;
                    else
                        e.Value = Properties.Resources.transparent;
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "IsTop":
                    if (value.ToLower() == "true")
                        e.Value = Properties.Resources.mail_set_top;
                    else
                        e.Value = Properties.Resources.transparent;
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "MailSizeString":
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case "MailState":
                    if (e.Value != null)
                    {
                        if (Enum.TryParse(e.Value.ToString(), out MailStateEnum stateEnum))
                        {
                            switch (stateEnum)
                            {
                                case MailStateEnum.DRAFT:
                                    e.Value = Properties.Resources.maildraft;
                                    break;
                                case MailStateEnum.SENDING:
                                    e.Value = Properties.Resources.sending;
                                    break;
                                case MailStateEnum.SEND_FAIL:
                                case MailStateEnum.SEND_FAIL_END:
                                    e.Value = Properties.Resources.sendfail;
                                    break;
                                case MailStateEnum.SEND_SUCCESS:
                                    e.Value = Properties.Resources.sendsuccess;
                                    break;
                                default:
                                    e.Value = Properties.Resources.waitsend;
                                    break;
                            }
                        }
                    }
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
            }
        }

        /// <summary>
        /// 邮件列表鼠标单击(左键、右键)事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrid_maillist_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                case MouseButtons.Right:
                    DataGridView dgv = sender as DataGridView;
                    if (e.RowIndex > -1)
                    {
                        dgv.ClearSelection();
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    selectedRowIndex = e.RowIndex;
                    SetMailReadState(true);
                    if (e.Button == MouseButtons.Right)
                    {
                        var row = dgv.Rows[e.RowIndex];
                        SetMailContextMenu(row);
                    }
                    LoadMailViewPanel(SelectedMailMainId);
                    break;
            }
        }

        /// <summary>
        /// 设置邮件邮件菜单
        /// </summary>
        /// <param name="row"></param>
        private void SetMailContextMenu(DataGridViewRow row)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip() { Name = "contextMenu_mail" };
            ToolStripMenuItem menuItem = null;
            int folderTypeCode = searchDto != null ? searchDto.MailFolderTypeCode : 0;
            var rowData = dgBindData[row.Index];
            switch (folderTypeCode)
            {
                case (int)MailFolderEnum.DraftBox:
                    menuItem = new ToolStripMenuItem
                    {
                        Name = "edit",
                        Text = "编辑"
                    };
                    menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    break;
                case (int)MailFolderEnum.Deleted:
                    //menuItem = new ToolStripMenuItem
                    //{
                    //    Name = "recovery",
                    //    Text = "恢复"
                    //};
                    //menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                    //contextMenu.Items.Add(menuItem);
                    break;
                default:
                    #region 默认邮件右键操作菜单

                    menuItem = new ToolStripMenuItem
                    {
                        Name = "reply",
                        Text = "回复"
                    };
                    menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    menuItem = new ToolStripMenuItem
                    {
                        Name = "replyAll",
                        Text = "回复全部"
                    };
                    menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    menuItem = new ToolStripMenuItem
                    {
                        Name = "forward",
                        Text = "转发"
                    };
                    menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    menuItem = new ToolStripMenuItem
                    {
                        Name = "forwardAttach",
                        Text = "以附件转发"
                    };
                    menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                    contextMenu.Items.Add(menuItem);
                    contextMenu.Items.Add(new ToolStripSeparator());//分割线
                    if (rowData.Viewed != true)
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "setRead",
                            Text = "标记已读"
                        };
                        menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    else
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "setUnRead",
                            Text = "标记未读"
                        };
                        menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    //if (row.Cells["IsTop"].ToString().ToLower() != "true")
                    if (rowData.IsTop != true)
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "setTop",
                            Text = "邮件置顶"
                        };
                        menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    else
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "setUnTop",
                            Text = "取消邮件置顶"
                        };
                        menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    string mailBoxId = rowData.MailBoxId;
                    if (!string.IsNullOrWhiteSpace(mailBoxId.ToString()))
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "moveToFolder",
                            Text = "移动到..."
                        };
                        menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }

                    var labels = mailService.GetListSelfMailLabel(CurrentUserInfo.GetLoginedUserInfo(), new QueryParameter());
                    if (!labels.IsNull() && labels.Items.Count > 0)
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "moveToFolder",
                            Text = "标签"
                        };
                        ToolStripMenuItem childMenuItem = new ToolStripMenuItem
                        {
                            Name = "clearMailLabel",
                            Text = "清除标签"
                        };
                        childMenuItem.Click += new EventHandler(dgrid_maillist_ToolStripLabelMenuItem_Click);
                        menuItem.DropDownItems.Add(childMenuItem);
                        List<MailLabel> rowLabelInfo = JsonSerializationHelper.JsonDeserialize<List<MailLabel>>(rowData.LabelInfo) ?? new List<MailLabel>();
                        foreach (var label in labels.Items)
                        {
                            bool labelExist = rowLabelInfo.Any(x => x.MailLabelId == label.MailLabelId);

                            childMenuItem = new ToolStripMenuItem
                            {
                                Name = label.MailLabelId,
                                Text = (labelExist ? "√" : "") + label.MailLabelName,
                                Tag = labelExist
                            };
                            childMenuItem.Click += new EventHandler(dgrid_maillist_ToolStripLabelMenuItem_Click);
                            menuItem.DropDownItems.Add(childMenuItem);
                        }

                        contextMenu.Items.Add(menuItem);
                    }
                    contextMenu.Items.Add(menuItem);
                    if (rowData.MailType == 1)//收件有加入黑名单功能
                    {
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "addBlacklist",
                            Text = "加入黑名单"
                        };
                        menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                    }
                    #endregion 默认邮件右键操作菜单
                    break;
            }
            menuItem = new ToolStripMenuItem
            {
                Name = "remove",
                Text = "删除"
            };
            menuItem.Click += new EventHandler(dgrid_maillist_ToolStripMenuItem_Click);
            contextMenu.Items.Add(menuItem);
            contextMenu.Show(MousePosition.X, MousePosition.Y);
        }

        /// <summary>
        /// 邮件右键菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrid_maillist_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            if (string.IsNullOrWhiteSpace(SelectedMailMainId))
            {
                MessageBox.Show("请先选择邮件记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            ResponseResult result;
            switch (menuItem.Name)
            {
                case "reply":
                    FormHelper.OpenComposeForm(ComposeActionEnum.Reply, SelectedMailMainId);
                    break;
                case "replyAll":
                    FormHelper.OpenComposeForm(ComposeActionEnum.ReplyAll, SelectedMailMainId);
                    break;
                case "forward":
                    FormHelper.OpenComposeForm(ComposeActionEnum.Forward, SelectedMailMainId);
                    break;
                case "forwardAttach":
                    FormHelper.OpenComposeForm(ComposeActionEnum.ForwardAsAttach, SelectedMailMainId);
                    break;
                case "setRead":
                    SetMailReadState(true);
                    break;
                case "setUnRead":
                    SetMailReadState(false);
                    break;
                case "setTop":
                    result = mailService.SetMailTopStatus(true, new string[] { SelectedMailMainId });
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString() && selectedRowIndex > -1)
                    {
                        ReloadData();
                    }
                    break;
                case "setUnTop":
                    result = mailService.SetMailTopStatus(false, new string[] { SelectedMailMainId });
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString() && selectedRowIndex > -1)
                    {
                        ReloadData();
                    }
                    break;
                case "remove":
                    MailActionHelper.Remove(SelectedMailMainId, (actionArg) =>
                    {
                        if (selectedRowIndex > -1)
                            dgBindData.RemoveAt(selectedRowIndex);
                    });
                    break;
                case "edit"://草稿重新编辑
                    FormHelper.OpenComposeForm(ComposeActionEnum.Write, SelectedMailMainId);
                    break;
                case "moveToFolder":
                    var mailMain = SelectedRowData;
                    if (mailMain == null)
                    {
                        MessageBox.Show("请先选择邮件记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    MailActionHelper.MoveMailFolder(mailMain, (folderId) =>
                    {
                        ReloadData();
                    });
                    break;
                case "recovery":
                    //result = mailService.re(CurrentUserInfo.GetLoginedUserInfo(), new string[] { SelectedMailMainId });
                    //MessageBox.Show(result.Message);
                    break;
                case "addBlacklist":
                    var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
                    var rowData = dgBindData[selectedRowIndex];
                    if (rowData != null)
                    {
                        var emails = UtilityHelper.GetMailReceiverGroup(rowData.Sender);
                        if (emails.Count < 1) return;
                        MailJudge judge = new MailJudge
                        {
                            MailAddress = emails.Keys.First(),
                            MailBoxId = rowData.MailBoxId,
                            MessageID = rowData.MessageId,
                            OperateType = 2
                        };
                        result = mailService.SaveMailJudge(loginInfo, judge);
                        if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                        {
                            MessageBox.Show("加入黑名单成功", "提示");
                            judge = result.Data as MailJudge;
                            if (judge.MailJudgeId > 0 && !string.IsNullOrWhiteSpace(judge.MailBoxId))
                            {
                                mailAlert.MailJudgeChange(loginInfo.OCode, judge.MailBoxId);
                            }
                        }
                        else
                        {
                            MessageBox.Show("加入黑名单失败:" + result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// 邮件右键标签菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrid_maillist_ToolStripLabelMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            if (string.IsNullOrWhiteSpace(SelectedMailMainId))
            {
                MessageBox.Show("请先选择邮件记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            ResponseResult result;
            var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
            switch (menuItem.Name.ToLower())
            {
                //case "addmaillabel":
                //    SettingForm labelForm = new SettingForm(MailSettingTabPageEnum.Label);

                //    break;
                case "clearmaillabel":
                    result = mailService.MailRemoveLabel(loginInfo, new string[] { SelectedMailMainId }, string.Empty);
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                    {
                        ReloadData();
                    }
                    else
                    {
                        MessageBox.Show("清除标签失败:" + result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    break;
                default:
                    var labelId = menuItem.Name;
                    bool flagExist = false;
                    if (menuItem.Tag != null) bool.TryParse(menuItem.Tag.ToString(), out flagExist);
                    if (flagExist == true)
                        result = mailService.MailRemoveLabel(loginInfo, new string[] { SelectedMailMainId }, labelId);
                    else
                        result = mailService.MailAddLabel(loginInfo, new string[] { SelectedMailMainId }, menuItem.Name);
                    if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                    {
                        ReloadData();
                    }
                    else
                    {
                        MessageBox.Show("设置标签失败:" + result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    break;

            }
        }

        private void SetMailReadState(bool read = true)
        {
            if (selectedRowIndex > -1)
            {
                var data = dgBindData[selectedRowIndex];
                if (data.Viewed == read) return;
                var result = mailService.SetMailReadStatus(new string[] { SelectedMailMainId }, read);
                if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                {
                    data.Viewed = read;
                    dgBindData[selectedRowIndex] = data;
                    SetMailReadStatusDelegate?.Invoke(read);
                }
            }
        }

        /// <summary>
        /// 设置行是否加粗，已读/未读状态
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="bold"></param>
        private void SetRowFontStyleBold(int rowIndex, bool bold = true)
        {
            using (Font font = new Font(dgrid_maillist.DefaultCellStyle.Font.FontFamily, dgrid_maillist.DefaultCellStyle.Font.Size, bold ? FontStyle.Bold : FontStyle.Regular))
            {
                dgrid_maillist.Rows[rowIndex].DefaultCellStyle.Font = font;
            }
        }

        /// <summary>
        /// 加载列表上邮件查看栏
        /// </summary>
        private void LoadMailViewPanel(string mailId)
        {
            if (string.IsNullOrWhiteSpace(mailId)) { splitContainer1.Panel2.Controls.Clear(); return; }
            string key = "form_mailView_includ";
            MailViewDetailForm viewDetail;
            if (splitContainer1.Panel2.Controls.Count > 1 || !splitContainer1.Panel2.Controls.ContainsKey(key))
            {
                splitContainer1.Panel2.Controls.Clear();
                viewDetail = new MailViewDetailForm(mailId) { Name = key, Dock = DockStyle.Fill, TopLevel = false };
                splitContainer1.Panel2.Controls.Add(viewDetail);
                viewDetail.Show();
            }
            else
            {
                viewDetail = (MailViewDetailForm)splitContainer1.Panel2.Controls[key];
                viewDetail.Reload(mailId);
            }

        }
        
        /// <summary>
        /// 设置邮件已读/未读成功后的回调委托
        /// </summary>
        /// <param name="read"></param>
        /// <param name="num"></param>
        public delegate void SetMailReadStatusHandler(bool read, int num = 1);
        /// <summary>
        ///  设置邮件已读/未读成功后的事件
        /// </summary>
        public event SetMailReadStatusHandler SetMailReadStatusDelegate;
    }
}
