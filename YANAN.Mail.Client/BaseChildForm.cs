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
    /// <summary>
    /// 系统内tab页或者嵌套打开的窗体基类，不含title和关闭
    /// </summary>
    public partial class BaseChildForm : BaseForm
    {
        public BaseChildForm()
        {
            InitializeComponent();
        }
    }
}
