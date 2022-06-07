using YANAN.Mail.Utilities.Enums;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    public partial class SettingForm : BaseDialogForm
    {
        public SettingForm(MailSettingTabPageEnum tabPageEnum = MailSettingTabPageEnum.Default)
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler(SettingForm_FormClosed);
            if (tabPageEnum != MailSettingTabPageEnum.Default)
            {
                tabControl.SelectTab((int)tabPageEnum);
            }
            else { tabControl.SelectTab(0); }
            LoadTabPage(tabControl.SelectedIndex);
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            LoadTabPage(e.TabPageIndex);
        }
        private void LoadTabPage(int tabPageIndex)
        {
            var tabPage = tabControl.SelectedTab;
            Form form = null;
            switch (tabPageIndex)
            {
                case 0://邮箱设置
                    form = new MailSettingForm { TopLevel = false, Dock = DockStyle.Fill };
                    break;
                case 1://签名
                    form = new MailSignForm { TopLevel = false, Dock = DockStyle.Fill };
                    break;
                case 2://模板
                    form = new MailTemplateForm { TopLevel = false, Dock = DockStyle.Fill };
                    break;
                case 3://过滤器
                    form = new MailFilterForm { TopLevel = false, Dock = DockStyle.Fill };
                    break;
                case 4://标签
                    form = new MailLabelForm { TopLevel = false, Dock = DockStyle.Fill };
                    break;
                case 5://黑名单
                    form = new MailJudgeForm { TopLevel = false, Dock = DockStyle.Fill };
                    break;
            }
            if (form != null)
            {
                tabPage.Controls.Clear();
                tabPage.Controls.Add(form);
                form.Show();
            }

        }
        private void SettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IList<int> indexs = new List<int> { 0, 4 };//如果是邮箱设置/标签时才刷新主界面左侧树,此处待改进,应改为数据有变更才更新
            if (indexs.Contains(tabControl.SelectedIndex))
                DialogResult = DialogResult.OK;
        }

    }
}
