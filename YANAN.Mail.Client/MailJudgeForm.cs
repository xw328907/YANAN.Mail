using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
namespace YANAN.Mail.Client
{
    using CCWin.SkinControl;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;

    /// <summary>
    /// 窗体 - 黑名单/拒收邮箱
    /// </summary>
    public partial class MailJudgeForm : BaseChildForm
    {
        BindingSource bindingSource = new BindingSource();
        BindingList<MailJudge> dgBindData = new BindingList<MailJudge>();
        public MailJudgeForm()
        {
            InitializeComponent();
            toolBtn_delete.Click += new EventHandler(toolBtn_delete_Click);
            dgrid_judge.CellContentClick += new DataGridViewCellEventHandler(dgrid_judge_CellContentClick);
            InitForm();
        }
        private void toolBtn_delete_Click(object sender, EventArgs e)
        {
            var rows = dgrid_judge.Rows;
            IList<int> ids = new List<int>();
            foreach (DataGridViewRow viewRow in rows)
            {
                var cell = viewRow.Cells["checkbox"];
                bool flag = false;
                if (cell.Value != null)
                {
                    bool.TryParse(cell.Value.ToString(), out flag);
                }
                if (flag && viewRow.Cells["MailJudgeId"].Value != null)
                {
                    int.TryParse(viewRow.Cells["MailJudgeId"].Value.ToString(), out int id);
                    if (id > 0)
                        ids.Add(id);
                }
            }
            if (ids.Count > 0)
            {
                var loginInfo = CurrentUserInfo.GetLoginedUserInfo();
                var result = mailService.RemoveMailJudge(loginInfo, ids.ToArray());
                if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
                {
                    var boxIds = result.Data as IList<string>;
                    foreach (var boxid in boxIds)
                    {
                        mailAlert.MailJudgeChange(loginInfo.OCode, boxid);
                    }
                    LoadGridData();
                    MessageBox.Show("删除成功", "提示");
                }
                else
                {
                    MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("请先选择需要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }

        }
        private void dgrid_judge_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgrid = sender as SkinDataGridView;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //获取控件的值
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgrid.Rows[e.RowIndex].Cells["checkbox"];
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
        }
        private void InitForm()
        {
            dgrid_judge.AutoGenerateColumns = false;//不列出数据源所有字段(未再datagridview中定义的)
            LoadGridData();
        }
        private void LoadGridData()
        {
            QueryParameter query = new QueryParameter();
            var result = mailService.GetListSelfMailJudge(CurrentUserInfo.GetLoginedUserInfo(), query);
            if (result == null) result = new EntityList<MailJudge>();
            dgBindData.Clear();
            result.Items.ForEach(x => dgBindData.Add(x));
            bindingSource.DataSource = dgBindData;
            dgrid_judge.DataSource = bindingSource;
            dgrid_judge.ClearSelection();
        }
    }
}
