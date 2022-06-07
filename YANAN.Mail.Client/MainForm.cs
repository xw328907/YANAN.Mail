using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    public partial class MainForm : BasePageForm
    {
        public MainForm()
        {
            InitializeComponent();
            MailMainForm mailMainForm = new MailMainForm()
            {
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            panel1.Controls.Clear();
            panel1.Controls.Add(mailMainForm);
            mailMainForm.Show();
            //MessageBox.Show(Handle.ToString());
        }

        private void toolStripMenuItem_show_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void toolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出系统吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                notifyIcon1.Visible = false;
                this.CloseAndExit();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Maximized;
                Activate();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ControlHelper.ExitSystem();
        }
    }
}
