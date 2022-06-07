using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace YANAN.Mail.Core.WindowsService
{
  public  class ServiceStarter
    {
        public const string INSTALL_COMMAND = "/install";
        public const string REMOVE_COMMAND = "/remove";
        public const string RUN_AS_SERVICE_COMMAND = "/service";
        public const string RUN_AS_CONSOLE_COMMAND = "/console";


        private static string startupFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
        public static void Start(Action<string[]> startAction, Action stopAction, ServiceStartInfo serviceStartInfo, string[] args)
        {
            if (startAction == null)
            {
                throw new ArgumentNullException("startAction");
            }
            if (stopAction == null)
            {
                throw new ArgumentNullException("stopAction");
            }

            if (serviceStartInfo == null)
            {
                throw new ArgumentNullException("serviceStartInfo");
            }

            string command = "";
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                command = RUN_AS_CONSOLE_COMMAND;
            }
            else
            {
                command = args[0];
            }

            command = (command + "").Trim();
            if (command == INSTALL_COMMAND)
            {
                InstallService(serviceStartInfo);
            }
            else if (command == REMOVE_COMMAND)
            {
                RemoveService(serviceStartInfo);
            }
            else if (command == RUN_AS_SERVICE_COMMAND)
            {
                StartAsService(startAction, stopAction, args);
            }
            else
            {
                //RUN_AS_CONSOLE_COMMAND
                StartAsConsole(startAction, stopAction, args);
            }
        }

        /// <summary>
        /// 安装服务
        /// </summary> 
        private static void InstallService(ServiceStartInfo serviceStartInfo)
        {
            bool bInstalledOk = ServiceInstaller.InstallService(startupFileName + " " + RUN_AS_SERVICE_COMMAND, serviceStartInfo);
            if (bInstalledOk)
            {
                StringBuilder sbNotice = new StringBuilder();
                sbNotice.AppendFormat("{0} Installed Successful.", serviceStartInfo.ServiceName);

                Console.WriteLine(sbNotice.ToString());
            }
            else
            {
                StringBuilder sbNotice = new StringBuilder();
                sbNotice.AppendFormat("{0} Installed Failure.", serviceStartInfo.ServiceName);

                Console.WriteLine(sbNotice.ToString());
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// 删除服务
        /// </summary> 
        private static void RemoveService(ServiceStartInfo serviceStartInfo)
        {
            Console.WriteLine("If service is running,please stop it first.\r\n");
            bool bUninstalledOk = ServiceInstaller.UnInstallService(serviceStartInfo.ServiceName);
            if (bUninstalledOk)
            {
                StringBuilder sbNotice = new StringBuilder();
                sbNotice.AppendFormat("{0} Removed Successful.", serviceStartInfo.ServiceName);

                Console.WriteLine(sbNotice.ToString());
            }
            else
            {
                StringBuilder sbNotice = new StringBuilder();
                sbNotice.AppendFormat("{0} Removed Failure.", serviceStartInfo.ServiceName);

                Console.WriteLine(sbNotice.ToString());
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// 以服务运行
        /// </summary>
        private static void StartAsService(Action<string[]> startAction, Action stopAction, string[] args)
        {
            ServiceCore service = new ServiceCore();
            service.StartAction = startAction;
            service.StopAction = stopAction;
            ServiceBase.Run(service);
        }

        /// <summary>
        /// 以控制台运行
        /// </summary>
        private static void StartAsConsole(Action<string[]> startAction, Action stopAction, string[] args)
        {
            StringBuilder sbNotice = new StringBuilder();
            sbNotice.AppendFormat("{0}\r\n", startupFileName);
            sbNotice.AppendFormat("{0}\tTo Install The Service.May need to run as administrator.\r\n", INSTALL_COMMAND);
            sbNotice.AppendFormat("{0}\tTo Remove The Service.May need to run as administrator.\r\n", REMOVE_COMMAND);
            sbNotice.AppendFormat("{0}\tTo Run As A Console App For Debugging.\r\n", RUN_AS_CONSOLE_COMMAND);
            sbNotice.AppendFormat("{0}\tTo Run As A Service.\r\n", RUN_AS_SERVICE_COMMAND);
            sbNotice.AppendFormat("Default command is {0}.\r\n", RUN_AS_CONSOLE_COMMAND);
            sbNotice.AppendFormat("Press quit to exit.\r\n");
            sbNotice.Append(new string('-', 70));
            Console.WriteLine(sbNotice.ToString());

            startAction(args);

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "quit") break;
            }
            stopAction();
        }
    }
}
