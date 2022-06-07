using System;
using System.IO;
using System.Windows.Forms;

namespace YANAN.Mail.Client.Controls
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities.Enums;

    public partial class AttachControl : UserControl
    {
        /// <summary>
        /// 当前附件是否查看模式
        /// </summary>
        private MaiAttachContextMenuModeEnum attachViewMode;
        private MailAttach mailAttach;
        /// <summary>
        /// 执行完成回调方法
        /// </summary>
        public event EventHandler<AttachControlCallbackEventArgs> EventCompletedCallback;
        public AttachControl(MailAttach attach = null, MaiAttachContextMenuModeEnum attachView = MaiAttachContextMenuModeEnum.View)
        {
            attachViewMode = attachView;
            mailAttach = attach;
            InitializeComponent();
            if (attach != null)
            {
                //Capture = true;
                toolTip1.ToolTipTitle = attach.FilesName;
                //MouseClick += AttachControl_MouseClick;
                lbl_filename.MouseClick += AttachControl_MouseClick;
                lbl_filesize.MouseClick += AttachControl_MouseClick;
                lbl_filename.Text = attach.FilesName;
                lbl_filesize.Text = !string.IsNullOrWhiteSpace(attach.FilesSize) ? attach.FilesSize : Utilities.UtilityHelper.ConvertFileSize(attach.ActualSize);
            }
        }


        private void lbl_filename_MouseHover(object sender, EventArgs e)
        {
            BackColor = System.Drawing.Color.FromArgb(241, 248, 255);
        }
        private void lbl_filename_MouseLeave(object sender, EventArgs e)
        {
            BackColor = new System.Drawing.Color();
        }

        private void lbl_filesize_MouseHover(object sender, EventArgs e)
        {
            BackColor = System.Drawing.Color.FromArgb(241, 248, 255);
        }

        private void lbl_filesize_MouseLeave(object sender, EventArgs e)
        {
            BackColor = new System.Drawing.Color();
        }

        private void AttachControl_MouseClick(object sender, MouseEventArgs e)
        {
            var obj = (Label)sender;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip() { Name = "contextMenu_mailAttach" };
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                switch (attachViewMode)
                {
                    case MaiAttachContextMenuModeEnum.View:
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "SaveAs",
                            Text = "另存为"
                        };
                        menuItem.Click += new EventHandler(AttachControl_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                        break;
                    case MaiAttachContextMenuModeEnum.Edit:
                        menuItem = new ToolStripMenuItem
                        {
                            Name = "Remove",
                            Text = "删除"
                        };
                        menuItem.Click += new EventHandler(AttachControl_ToolStripMenuItem_Click);
                        contextMenu.Items.Add(menuItem);
                        break;
                }
                if (contextMenu.Items.Count > 0)
                {
                    obj.ContextMenuStrip = contextMenu;
                }
            }
        }
        /// <summary>
        /// 邮箱树节点右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttachControl_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            switch (menuItem.Name.ToLower())
            {
                case "saveas"://另存为
                    if (mailAttach != null && !string.IsNullOrWhiteSpace(mailAttach.FilesPath))
                    {
                        if (File.Exists(mailAttach.FilesPath))
                        {
                            SaveFileDialog sfd = new SaveFileDialog
                            {
                                //设置文件类型 
                                Filter = "文件类型|*." + mailAttach.FilesType + "|所有文件(*.*)|*.*",
                                //设置默认文件类型显示顺序 
                                FilterIndex = 1,
                                //保存对话框是否记忆上次打开的目录 
                                RestoreDirectory = true,
                                //设置默认的文件名
                                FileName = mailAttach.FilesName
                            };
                            //点了保存按钮进入 
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                File.Copy(mailAttach.FilesPath, sfd.FileName, true);
                            }
                        }
                    }
                    break;
                case "remove":
                    if (mailAttach != null && !string.IsNullOrWhiteSpace(mailAttach.FilesPath))
                    {
                        EventCompletedCallback?.Invoke(this, new AttachControlCallbackEventArgs { MailAttach = mailAttach });
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// 附件控件事件执行完后的回调方法参数
    /// </summary>
    public class AttachControlCallbackEventArgs : EventArgs
    {
        /// <summary>
        /// 当前附件数据对象
        /// </summary>
        public MailAttach MailAttach { get; set; }
    }
}
