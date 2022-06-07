using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using System.ComponentModel;

    /// <summary>
    /// 弹框 - 邮箱联系人选择
    /// </summary>
    public partial class MailContactSelectForm : BaseDialogForm
    {
        public List<MailContact> SelectedMailContactList = new List<MailContact>();
        public MailContactSelectForm()
        {
            InitializeComponent();
            dgrid_contact.AutoGenerateColumns = false;
            InitControlEvents();
            LoadData(true);
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var rows = dgrid_contact.Rows;
            if (SelectedMailContactList == null) SelectedMailContactList = new List<MailContact>();
            foreach (DataGridViewRow viewRow in rows)
            {
                var cell = viewRow.Cells["checkbox"];
                bool flag = false;
                if (cell.Value != null)
                {
                    bool.TryParse(cell.Value.ToString(), out flag);
                }
                var rowData = dgBindDataMailContact[viewRow.Index];
                int selectDataIndex = SelectedMailContactList.FindIndex(x => x.MailContactId == rowData.MailContactId);
                if (flag)
                {
                    if (selectDataIndex < 0) { SelectedMailContactList.Add(rowData); }
                    else { SelectedMailContactList[selectDataIndex] = rowData; }
                }
                else
                {
                    if (selectDataIndex > 0) { SelectedMailContactList.RemoveAt(selectDataIndex); }
                }
            }
            if (SelectedMailContactList.Count < 1)
            {
                MessageBox.Show("请选择联系人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }
        private void toolBtnRefreshClick(object sender, EventArgs e)
        {
            LoadData(true);
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadData();
            }
        }

        private void dgrid_contact_CellClick(object sender,DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                SetDataGridCheckbox(e.RowIndex);
            }
        }
        private void SetDataGridCheckbox(int rowIndex)
        {
            //获取控件的值
            DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgrid_contact.Rows[rowIndex].Cells["checkbox"];
            bool flag = false;
            if (checkCell.Value != null)
                bool.TryParse(checkCell.Value.ToString(), out flag);
            if (flag == true)
            {
                checkCell.Value = false;
            }
            else
            {
                checkCell.Value = true;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        /// <summary>
        /// 初始化控件事件
        /// </summary>
        private void InitControlEvents()
        {
            txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            toolBtnRefresh.Click += new EventHandler(toolBtnRefreshClick);
            dgrid_contact.CellClick += new DataGridViewCellEventHandler(dgrid_contact_CellClick);
        }
        BindingSource bindingSourceMailContact = new BindingSource();
        BindingList<MailContact> dgBindDataMailContact = new BindingList<MailContact>();
        private void LoadData(bool isReload = false)
        {
            QueryParameter query = new QueryParameter();
            if (isReload == true)
            {
                txtSearch.Text = string.Empty;
                SelectedMailContactList = new List<MailContact>();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    query.KeyWords = txtSearch.Text.Trim();
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
            txtSearch.Focus();
        }
    }
}
