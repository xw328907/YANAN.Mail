using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;

    /// <summary>
    /// 邮件标签管理窗口
    /// </summary>
    public partial class MailLabelForm : BaseChildForm
    {
        BindingSource bindingSource = new BindingSource();
        BindingList<MailLabel> dgBindData = new BindingList<MailLabel>();
        private string SelectLabelId = string.Empty;
        private int SelectRowIndex = -1;
        public MailLabelForm()
        {
            InitializeComponent();
            InitControlsEvent();
            InitFrom();
        }
        private void InitControlsEvent()
        {
            btn_colorSelect.Click += new EventHandler(btn_colorSelect_Click);
            toolBtn_add.Click += new EventHandler(toolBtn_add_Click);
            toolBtn_delete.Click += new EventHandler(toolBtn_delete_Click);
            btn_save.Click += new EventHandler(btn_save_Click);
            dg_label.CellClick += new DataGridViewCellEventHandler(dg_label_CellClick);
            dg_label.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dg_label_RowPrePaint);
        }
        private void InitFrom()
        {
            dg_label.AutoGenerateColumns = false;//不列出数据源所有字段(未再datagridview中定义的)
            InitAddForm();
            LoadGrid();
        }
        private void btn_colorSelect_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog { AllowFullOpen = true };
            if (DialogResult.OK == colorDialog.ShowDialog())
            {
                lbl_Color.BackColor = colorDialog.Color;
                lbl_Color.Text = ColorTranslator.ToHtml(colorDialog.Color);
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
            if (SelectRowIndex < 0)
            {
                MessageBox.Show("请选择要删除的标签", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            var data = dgBindData[SelectRowIndex];
            if (MessageBox.Show("确定删除标签" + data.MailLabelName + "?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                return;
            var result = mailService.RemoveMailLabel(CurrentUserInfo.GetLoginedUserInfo(), new string[] { data.MailLabelId });
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                dgBindData.RemoveAt(SelectRowIndex);
                InitAddForm();
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
            MailLabel mailLabel = new MailLabel
            {
                MailLabelName = txt_MailLabelName.Text.Trim(),
                MailLabelId = SelectLabelId,
                Color = lbl_Color.Text,
                Memo = txt_Memo.Text.Trim()
            };
            if (string.IsNullOrWhiteSpace(mailLabel.MailLabelName))
            {
                MessageBox.Show("标签名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrWhiteSpace(mailLabel.Color))
            {
                MessageBox.Show("请选择标签颜色", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            var result = mailService.SaveMailLabel(loginInfo, mailLabel);
            if (result.Code == ResponseCodeEnum.SUCCESS.ToString())
            {
                MessageBox.Show("保存成功", "提示");
                var label = result.Data as MailLabel;
                if (string.IsNullOrWhiteSpace(SelectLabelId))
                {
                    dgBindData.Add(label);
                    InitAddForm();
                }
                else
                {
                    dgBindData[SelectRowIndex] = label;
                }
            }
            else
            {
                MessageBox.Show("保存失败:" + result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dg_label_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            DataGridViewRow dgr = dgv.Rows[e.RowIndex];
            try
            {
                var val = dgr.Cells["Color"].Value;
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    dgr.Cells["ColorDisplay"].Style.BackColor = ColorTranslator.FromHtml(val.ToString());
                    dgr.Cells["ColorDisplay"].Style.SelectionBackColor = ColorTranslator.FromHtml(val.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dg_label_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRowIndex = e.RowIndex;
            MailLabel label = new MailLabel();
            if (e.RowIndex > -1)
            {
                DataGridView dgv = sender as DataGridView;
                label = dgBindData[e.RowIndex];
                DataGridViewRow dgr = dgv.Rows[e.RowIndex];
                dgr.Cells["ColorDisplay"].Style.SelectionBackColor = ColorTranslator.FromHtml(label.Color);
            }
            LoadMailSignData(label);
        }
        /// <summary>
        /// 初始化添加窗口数据
        /// </summary>
        private void InitAddForm()
        {
            dg_label.ClearSelection();
            SelectRowIndex = -1;
            SelectLabelId = string.Empty;
            txt_MailLabelName.Text = string.Empty;
            txt_Memo.Text = string.Empty;
            lbl_Color.BackColor = SystemColors.Window;
            lbl_Color.Text = string.Empty;
        }
        private void LoadMailSignData(MailLabel label)
        {
            SelectLabelId = label.MailLabelId;
            txt_MailLabelName.Text = label.MailLabelName;
            lbl_Color.Text = label.Color;
            lbl_Color.BackColor = !string.IsNullOrWhiteSpace(label.Color) ? ColorTranslator.FromHtml(label.Color) : SystemColors.Window;
            txt_Memo.Text = label.Memo;
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        public void LoadGrid()
        {
            QueryParameter query = new QueryParameter();
            var result = mailService.GetListSelfMailLabel(CurrentUserInfo.GetLoginedUserInfo(), query);
            if (result == null) result = new EntityList<MailLabel>();
            dgBindData.Clear();
            result.Items.ForEach(x => dgBindData.Add(x));
            bindingSource.DataSource = dgBindData;
            dg_label.DataSource = bindingSource;
            InitAddForm();
        }
    }
}
