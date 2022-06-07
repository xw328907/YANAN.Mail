using YANAN.Mail.Entity;
using YANAN.Mail.Services.EmailFilter;
using YANAN.Mail.Utilities;
using YANAN.Mail.Utilities.Enums;
using YANAN.Mail.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    /// <summary>
    /// 窗体 - 邮箱过滤器设置
    /// </summary>
    public partial class MailFilterForm : BaseChildForm
    {
        BindingSource bindingSource = new BindingSource();
        BindingList<MailFilterCondition> dgBindData = new BindingList<MailFilterCondition>();
        private int SelectPrimaryId = 0;
        private int SelectRowIndex = -1;
        public MailFilterForm()
        {
            InitializeComponent();
            InitControlsEvent();
            InitFrom();
        }
        private void InitControlsEvent()
        {
            toolBtn_add.Click += new EventHandler(toolBtn_add_Click);
            combo_mailbox.SelectedIndexChanged += new EventHandler(combo_mailbox_SelectedIndexChanged);
            dgrid_filter.CellClick += new DataGridViewCellEventHandler(dg_label_CellClick);
            toolBtn_delete.Click += new EventHandler(toolBtn_delete_Click);
            btn_save.Click += new EventHandler(btn_save_Click);
            radio_event_delete.CheckedChanged += new EventHandler(radio_event_delete_CheckedChanged);
            radio_event_normal.CheckedChanged += new EventHandler(radio_event_normal_CheckedChanged);
        }
        private void InitFrom()
        {
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
            combo_mailbox.Bind(mailboxList);
            if (defaultMailBox != null && !string.IsNullOrWhiteSpace(defaultMailBox.MailBoxId))
                combo_mailbox.SelectedValue = defaultMailBox.MailBoxId;

            #endregion 邮箱数据源绑定

        }
        private void dg_label_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRowIndex = e.RowIndex;
            MailFilterCondition entity = null;
            if (e.RowIndex > -1)
            {
                DataGridView dgv = sender as DataGridView;
                entity = dgBindData[e.RowIndex];
            }
            LoadData(entity);
        }
        private void tree_filter_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (SelectPrimaryId.ToString() != e.Node.Name)//有改变选中节点
            {
                int.TryParse(e.Node.Name, out SelectPrimaryId);
                if (e.Node.Tag != null)
                {
                    var nodeTag = (MailFilterCondition)e.Node.Tag;
                    LoadData(nodeTag);
                }
            }
        }
        // 彻底删除
        private void radio_event_delete_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as RadioButton;
            if (checkBox.Checked)
            {
                chk_move_folder.Checked = false;
                chk_move_folder.Enabled = false;
                chk_set_label.Checked = false;
                chk_set_label.Enabled = false;
                chk_set_mail_read.Checked = false;
                chk_set_mail_read.Enabled = false;
            }
        }
        private void radio_event_normal_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as RadioButton;
            if (checkBox.Checked)
            {
                chk_move_folder.Enabled = true;
                chk_set_label.Enabled = true;
                chk_set_mail_read.Enabled = true;
            }
        }
        private void combo_mailbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
            InitControlDataSource();
            LoadData();
        }
        //添加
        private void toolBtn_add_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        //删除
        private void toolBtn_delete_Click(object sender, EventArgs e)
        {
            if (SelectRowIndex < 0)
            {
                MessageBox.Show("请选择要删除的过滤器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            var data = dgBindData[SelectRowIndex];
            if (MessageBox.Show("确定删除过滤器" + data.FilterName + "?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                return;
            var result = mailFilterService.RemoveMailFilterCondition(CurrentUserInfo.GetLoginedUserInfo(), new int[] { SelectPrimaryId });
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                dgBindData.RemoveAt(SelectRowIndex);
                LoadData();
                MessageBox.Show("删除成功", "提示");
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
            MailFilterCondition entity = GetFormData();
            if (string.IsNullOrWhiteSpace(entity.FilterName))
            {
                MessageBox.Show("规则名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (entity.FilterDoTime < 0)
            {
                MessageBox.Show("请选择执行时机", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (entity.ConditionOpertation < 0)
            {
                MessageBox.Show("请选择执行条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(entity.FilterConditions))
            {
                MessageBox.Show("请设置过滤条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(entity.FilterEvents))
            {
                MessageBox.Show("请设置过滤执行操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            var result = mailFilterService.SaveMailFilterCondition(loginInfo, entity);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                LoadGridData();
                MessageBox.Show("保存成功", "提示");
            }
            else
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>
        /// 窗体表单的一些下拉数据源等初始化
        /// </summary>
        private void InitControlDataSource()
        {
            string mailboxId = string.Empty;
            if (combo_mailbox.SelectedItem != null)
                mailboxId = ((ListItem)combo_mailbox.SelectedItem).Value;
            if (string.IsNullOrWhiteSpace(mailboxId))
                return;
            IList<ListItem> comboList;
            comboList = new List<ListItem> {
                new ListItem(EnumConditionOpration.Equal.GetDescription(), ((int)EnumConditionOpration.Equal).ToString())
                , new ListItem(EnumConditionOpration.NoEqual.GetDescription(), ((int)EnumConditionOpration.NoEqual).ToString())
                , new ListItem(EnumConditionOpration.Contain.GetDescription(), ((int)EnumConditionOpration.Contain).ToString())
                , new ListItem(EnumConditionOpration.UnContain.GetDescription(), ((int)EnumConditionOpration.UnContain).ToString())
            };
            combo_receive_operate.Bind(comboList);
            combo_send_operate.Bind(comboList);
            comboList = new List<ListItem> {
                new ListItem(EnumConditionOpration.Contain.GetDescription(), ((int)EnumConditionOpration.Contain).ToString())
                , new ListItem(EnumConditionOpration.UnContain.GetDescription(), ((int)EnumConditionOpration.UnContain).ToString())
            };
            combo_subject_operate.Bind(comboList);
            comboList = new List<ListItem> {
                new ListItem("带附件", ((int)EnumConditionOpration.Yes).ToString())
                , new ListItem("不带附件", ((int)EnumConditionOpration.No).ToString()) };
            combo_attach.Bind(comboList);
            var folders = mailBoxService.GetListMailFolderByMailBoxIds(new string[] { mailboxId });
            if (folders != null && folders.Count > 0)
            {
                comboList = new List<ListItem>();
                foreach (var folder in folders)
                {
                    comboList.Add(new ListItem { Text = folder.FolderName, Value = folder.MailFolderId });
                }
                combo_move_folder.Bind(comboList);
            }
            var labels = mailService.GetListSelfMailLabel(CurrentUserInfo.GetLoginedUserInfo(), new Utilities.QueryParameter());
            if (labels != null && labels.Items.Count > 0)
            {
                comboList = new List<ListItem>();
                foreach (var label in labels.Items)
                {
                    comboList.Add(new ListItem { Text = label.MailLabelName, Value = label.MailLabelId });
                }
                combo_mail_label.Bind(comboList);
            }
        }
        /// <summary>
        /// 窗体表单的初始化
        /// </summary>
        private void InitFormData()
        {
            txt_FilterName.Text = string.Empty;
            chk_FilterDoTime_receive.Checked = false;
            chk_FilterDoTime_send.Checked = false;
            chk_stop_other.Checked = false;
            chk_IsFilter.Checked = false;
            chk_send.Checked = false;
            combo_send_operate.SelectedIndex = -1;
            txt_send_value.Text = string.Empty;
            chk_send_UpperLower.Checked = false;
            chk_receive.Checked = false;
            combo_receive_operate.SelectedIndex = -1;
            txt_receive_value.Text = string.Empty;
            chk_receive_UpperLower.Checked = false;
            chk_subject.Checked = false;
            combo_subject_operate.SelectedIndex = -1;
            txt_subject_value.Text = string.Empty;
            chk_subject_UpperLower.Checked = false;
            chk_attach.Checked = false;
            combo_attach.SelectedIndex = -1;
            chk_mailsize.Checked = false;
            txt_mailsize_min.Value = 0;
            txt_mailsize_max.Value = 0;
            chk_move_folder.Checked = false;
            combo_move_folder.SelectedIndex = -1;
            chk_set_label.Checked = false;
            combo_mail_label.SelectedIndex = -1;
            chk_set_mail_read.Checked = false;
            radio_event_delete.Checked = false;
        }
        private void LoadData(MailFilterCondition mailFilterCondition = null)
        {
            if (mailFilterCondition == null) { mailFilterCondition = new MailFilterCondition(); }
            InitFormData();
            SelectPrimaryId = mailFilterCondition.FilterConditionId;
            if (SelectPrimaryId < 1) SelectRowIndex = -1;
            txt_FilterName.Text = mailFilterCondition.FilterName;
            chk_FilterDoTime_receive.Checked = false;
            chk_FilterDoTime_send.Checked = false;
            switch (mailFilterCondition.FilterDoTime)
            {
                case 2:
                    chk_FilterDoTime_receive.Checked = true;
                    chk_FilterDoTime_send.Checked = true;
                    break;
                case 1:
                    chk_FilterDoTime_send.Checked = true;
                    break;
                case 0:
                    chk_FilterDoTime_receive.Checked = true;
                    break;
            }
            if (mailFilterCondition.ConditionOpertation == 0)
            {
                radio_condtion_all.Checked = true;
            }
            else
            {
                radio_condition_any.Checked = true;
            }
            chk_stop_other.Checked = mailFilterCondition.IsnoreOther;
            chk_IsFilter.Checked = mailFilterCondition.IsFilter;
            chk_send.Checked = false;
            IList<FilterCondition> filterConditions = JsonSerializationHelper.JsonDeserialize<IList<FilterCondition>>(mailFilterCondition.FilterConditions);
            if (filterConditions != null && filterConditions.Count > 0)
            {
                foreach (var condition in filterConditions)
                {
                    switch (condition.ConditionId)
                    {
                        case 1100://发件人地址
                            chk_send.Checked = true;
                            combo_send_operate.SelectedValue = ((int)condition.ConditionOpration).ToString();
                            txt_send_value.Text = condition.ConditionValue;
                            chk_send_UpperLower.Checked = condition.DistinctUpperLower == true;
                            break;
                        case 1101://收件人地址
                            chk_receive.Checked = true;
                            combo_receive_operate.SelectedValue = ((int)condition.ConditionOpration).ToString();
                            txt_receive_value.Text = condition.ConditionValue;
                            chk_receive_UpperLower.Checked = condition.DistinctUpperLower == true;
                            break;
                        case 1102://主题
                            chk_subject.Checked = true;
                            combo_subject_operate.SelectedValue = ((int)condition.ConditionOpration).ToString();
                            txt_subject_value.Text = condition.ConditionValue;
                            chk_subject_UpperLower.Checked = condition.DistinctUpperLower == true;
                            break;
                        case 1202://带附件
                            chk_attach.Checked = true;
                            combo_attach.SelectedValue = ((int)condition.ConditionOpration).ToString();
                            break;
                        case 1300://邮件大小
                            chk_mailsize.Checked = true;
                            var val = condition.ConditionValue.Split('-');
                            if (!string.IsNullOrWhiteSpace(val[0]))
                                txt_mailsize_min.Value = decimal.Parse(val[0]);
                            if (!string.IsNullOrWhiteSpace(val[1]))
                                txt_mailsize_max.Value = decimal.Parse(val[1]);
                            break;
                    }
                }
            }

            IList<FilterEvent> filterEvents = JsonSerializationHelper.JsonDeserialize<IList<FilterEvent>>(mailFilterCondition.FilterEvents);
            if (filterEvents != null && filterEvents.Count > 0)
            {
                foreach (var e in filterEvents)
                {
                    switch (e.EventId)
                    {
                        case 1000://移动至
                            chk_move_folder.Checked = true;
                            combo_move_folder.SelectedValue = e.EventValue;
                            break;
                        case 1100://设置标签
                            chk_set_label.Checked = true;
                            combo_mail_label.SelectedValue = e.EventValue;
                            break;
                        case 1200://将邮件设为已读
                            chk_set_mail_read.Checked = true;
                            break;
                        case 1300://彻底删除
                            radio_event_delete.Checked = true;
                            break;
                    }
                }
            }
        }
        private MailFilterCondition GetFormData()
        {
            MailFilterCondition mailFilterCondition = new MailFilterCondition
            {
                FilterConditionId = SelectPrimaryId,
                FilterName = txt_FilterName.Text.Trim(),
                IsFilter = chk_IsFilter.Checked,
                IsnoreOther = chk_stop_other.Checked,
                SortNumber = dgBindData.Count + 1
            };
            if (combo_mailbox.SelectedItem != null)
            {
                mailFilterCondition.MailBoxId = (combo_mailbox.SelectedItem as ListItem).Value;
            }
            if (chk_FilterDoTime_receive.Checked && chk_FilterDoTime_send.Checked)
            {
                mailFilterCondition.FilterDoTime = 2;
            }
            else if (chk_FilterDoTime_receive.Checked)
            {
                mailFilterCondition.FilterDoTime = 0;
            }
            else if (chk_FilterDoTime_send.Checked)
            {
                mailFilterCondition.FilterDoTime = 1;
            }
            else
            {
                mailFilterCondition.FilterDoTime = -1;
            }
            if (radio_condtion_all.Checked)
            {
                mailFilterCondition.ConditionOpertation = 0;
            }
            else if (radio_condition_any.Checked)
            {
                mailFilterCondition.ConditionOpertation = 1;
            }
            else { mailFilterCondition.ConditionOpertation = -1; }
            IList<FilterCondition> filterConditions = new List<FilterCondition>();
            ListItem listItem;
            if (chk_receive.Checked)
            {
                listItem = combo_receive_operate.SelectedItem as ListItem;
                Enum.TryParse(listItem.Value, out EnumConditionOpration conditionOpration);
                filterConditions.Add(new FilterCondition
                {
                    ConditionId = 1101,
                    ConditionName = chk_receive.Text,
                    ConditionNum = 1,
                    ConditionOpration = conditionOpration,
                    ConditionOprationName = listItem.Text,
                    ConditionValue = txt_receive_value.Text.Trim(),
                    DistinctUpperLower = chk_receive_UpperLower.Checked
                });
            }
            if (chk_send.Checked)
            {
                listItem = combo_send_operate.SelectedItem as ListItem;
                Enum.TryParse(listItem.Value, out EnumConditionOpration conditionOpration);
                filterConditions.Add(new FilterCondition
                {
                    ConditionId = 1100,
                    ConditionName = chk_send.Text,
                    ConditionNum = 2,
                    ConditionOpration = conditionOpration,
                    ConditionOprationName = listItem.Text,
                    ConditionValue = txt_send_value.Text.Trim(),
                    DistinctUpperLower = chk_send_UpperLower.Checked
                });
            }
            if (chk_subject.Checked)
            {
                listItem = combo_subject_operate.SelectedItem as ListItem;
                Enum.TryParse(listItem.Value, out EnumConditionOpration conditionOpration);
                filterConditions.Add(new FilterCondition
                {
                    ConditionId = 1102,
                    ConditionName = chk_subject.Text,
                    ConditionNum = 3,
                    ConditionOpration = conditionOpration,
                    ConditionOprationName = listItem.Text,
                    ConditionValue = txt_subject_value.Text.Trim(),
                    DistinctUpperLower = chk_subject_UpperLower.Checked
                });
            }
            if (chk_mailsize.Checked)
            {
                filterConditions.Add(new FilterCondition
                {
                    ConditionId = 1300,
                    ConditionName = chk_mailsize.Text,
                    ConditionNum = 4,
                    ConditionOpration = EnumConditionOpration.Between,
                    ConditionOprationName = EnumConditionOpration.Between.GetDescription(),
                    ConditionValue = txt_mailsize_min.Value + "-" + txt_mailsize_max.Value
                });
            }
            if (chk_attach.Checked)
            {
                listItem = combo_attach.SelectedItem as ListItem;
                Enum.TryParse(listItem.Value, out EnumConditionOpration conditionOpration);
                filterConditions.Add(new FilterCondition
                {
                    ConditionId = 1202,
                    ConditionName = chk_attach.Text,
                    ConditionNum = 5,
                    ConditionOpration = conditionOpration,
                    ConditionOprationName = conditionOpration.GetDescription()
                });
            }
            if (filterConditions.Count > 0)
            {
                mailFilterCondition.FilterConditions = JsonSerializationHelper.JsonSerialize(filterConditions);
            }
            IList<FilterEvent> filterEvents = new List<FilterEvent>();
            if (radio_event_normal.Checked)
            {
                if (chk_move_folder.Checked)
                {
                    listItem = combo_move_folder.SelectedItem as ListItem;
                    filterEvents.Add(new FilterEvent
                    {
                        EventId = 1000,
                        EventNum = 1,
                        EventName = chk_move_folder.Text,
                        EventValue = listItem.Value,
                        EventValueName = listItem.Text
                    });
                }
                if (chk_set_label.Checked)
                {
                    listItem = combo_mail_label.SelectedItem as ListItem;
                    filterEvents.Add(new FilterEvent
                    {
                        EventId = 1100,
                        EventNum = 2,
                        EventName = chk_set_label.Text,
                        EventValue = listItem.Value,
                        EventValueName = listItem.Text
                    });
                }
                if (chk_set_mail_read.Checked)
                {
                    filterEvents.Add(new FilterEvent
                    {
                        EventId = 1200,
                        EventNum = 3,
                        EventName = chk_set_mail_read.Text
                    });
                }
            }
            else if (radio_event_delete.Checked)
            {
                filterEvents.Add(new FilterEvent
                {
                    EventId = 1300,
                    EventNum = 1,
                    EventName = radio_event_delete.Text
                });
            }

            if (filterEvents.Count > 0)
            {
                mailFilterCondition.FilterEvents = JsonSerializationHelper.JsonSerialize(filterEvents);
            }
            return mailFilterCondition;
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        public void LoadGridData()
        {
            string mailboxId = string.Empty;
            if (combo_mailbox.SelectedItem != null)
                mailboxId = ((ListItem)combo_mailbox.SelectedItem).Value;
            dgBindData.Clear();
            if (!string.IsNullOrWhiteSpace(mailboxId))
            {
                var list = mailFilterService.GetListMailFilterCondition(CurrentUserInfo.GetLoginedUserInfo(), mailboxId);
                list.ForEach(x => dgBindData.Add(x));
            }
            bindingSource.DataSource = dgBindData;
            dgrid_filter.DataSource = bindingSource;
            dgrid_filter.ClearSelection();
            SelectRowIndex = -1;
            SelectPrimaryId = 0;
        }
    }
}
