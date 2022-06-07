using System.Windows.Forms;

namespace YANAN.Mail.Client
{

    public partial class ProcessForm : BaseDialogForm
    {
        public ProcessForm()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 设置提示信息
        /// </summary>
        public string MessageInfo
        {
            set { label1.Text = value; }
        }

        /// <summary>
        /// 设置进度条显示值
        /// </summary>
        public int ProcessValue
        {
            set { progressBar1.Value = value; }
        }

        /// <summary>
        /// 设置进度条样式
        /// </summary>
        public ProgressBarStyle ProcessStyle
        {
            set { progressBar1.Style = value; }
        }
    }
}
