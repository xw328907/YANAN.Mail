using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace YANAN.Mail.Core.WindowsService
{
    partial class ServiceCore : ServiceBase
    {
        public Action<string[]> StartAction { get; set; }
        public Action StopAction { get; set; }
        public ServiceCore()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            StartAction(args);
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            StopAction();
        }
    }
}
