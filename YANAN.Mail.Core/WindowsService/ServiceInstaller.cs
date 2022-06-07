using System;
using System.Runtime.InteropServices;

namespace YANAN.Mail.Core.WindowsService
{
    class ServiceInstaller
    {
        #region DLLImport
        [DllImport("advapi32.dll")]
        public static extern bool ChangeServiceConfig2(IntPtr hService, UInt32 dwInfoLevel, ref String lpInfo);

        [DllImport("advapi32.dll")]
        public static extern IntPtr OpenSCManager(string lpMachineName, string lpSCDB, int scParameter);
        [DllImport("Advapi32.dll")]
        public static extern IntPtr CreateService(IntPtr SC_HANDLE, string lpSvcName, string lpDisplayName,
            int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpPathName,
            string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);
        [DllImport("advapi32.dll")]
        public static extern void CloseServiceHandle(IntPtr SCHANDLE);
        [DllImport("advapi32.dll")]
        public static extern int StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, int dwNumServiceArgs);
        [DllImport("advapi32.dll")]
        public static extern int DeleteService(IntPtr SVHANDLE);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        #endregion DLLImport

        public static bool InstallService(string svcPath, ServiceStartInfo serviceStartInfo)
        {
            #region Constants declaration.
            uint SERVICE_CONFIG_DESCRIPTION = 1;


            int SC_MANAGER_CREATE_SERVICE = 0x0002;
            int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
            //int SERVICE_DEMAND_START = 0x00000003;
            int SERVICE_ERROR_NORMAL = 0x00000001;

            int STANDARD_RIGHTS_REQUIRED = 0xF0000;
            int SERVICE_QUERY_CONFIG = 0x0001;
            int SERVICE_CHANGE_CONFIG = 0x0002;
            int SERVICE_QUERY_STATUS = 0x0004;
            int SERVICE_ENUMERATE_DEPENDENTS = 0x0008;
            int SERVICE_START = 0x0010;
            int SERVICE_STOP = 0x0020;
            int SERVICE_PAUSE_CONTINUE = 0x0040;
            int SERVICE_INTERROGATE = 0x0080;
            int SERVICE_USER_DEFINED_CONTROL = 0x0100;

            int SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
                SERVICE_QUERY_CONFIG |
                SERVICE_CHANGE_CONFIG |
                SERVICE_QUERY_STATUS |
                SERVICE_ENUMERATE_DEPENDENTS |
                SERVICE_START |
                SERVICE_STOP |
                SERVICE_PAUSE_CONTINUE |
                SERVICE_INTERROGATE |
                SERVICE_USER_DEFINED_CONTROL);

            #endregion Constants declaration.

            try
            {
                IntPtr sc_handle = OpenSCManager(null, null, SC_MANAGER_CREATE_SERVICE);

                if (sc_handle.ToInt32() != 0)
                {
                    IntPtr sv_handle = CreateService(sc_handle, serviceStartInfo.ServiceName, serviceStartInfo.DisplayName, SERVICE_ALL_ACCESS, SERVICE_WIN32_OWN_PROCESS, serviceStartInfo.ServiceStartType, SERVICE_ERROR_NORMAL, svcPath, null, 0, serviceStartInfo.Dependencies, serviceStartInfo.ServiceStartAccountName, serviceStartInfo.ServiceStartAccountPassword);

                    if (sv_handle.ToInt32() == 0)
                    {

                        CloseServiceHandle(sc_handle);
                        return false;
                    }
                    else
                    {


                        string desc = new string(' ', 400);
                        desc = serviceStartInfo.Description;
                        if (!string.IsNullOrEmpty(desc))
                        {
                            bool changed = ChangeServiceConfig2(sv_handle, SERVICE_CONFIG_DESCRIPTION, ref desc);
                        }



                        if (serviceStartInfo.StartType == System.ServiceProcess.ServiceStartMode.Automatic)
                        {
                            int i = StartService(sv_handle, 0, null);
                            if (i == 0)
                            {
                                return false;
                            }
                        }
                        CloseServiceHandle(sc_handle);
                        return true;
                    }
                }
                else
                    return false;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool UnInstallService(string svcName)
        {
            int GENERIC_WRITE = 0x40000000;
            IntPtr sc_hndl = OpenSCManager(null, null, GENERIC_WRITE);

            if (sc_hndl.ToInt32() != 0)
            {
                int DELETE = 0x10000;
                IntPtr svc_hndl = OpenService(sc_hndl, svcName, DELETE);
                //Console.WriteLine(svc_hndl.ToInt32());
                if (svc_hndl.ToInt32() != 0)
                {
                    int i = DeleteService(svc_hndl);
                    if (i != 0)
                    {
                        CloseServiceHandle(sc_hndl);
                        return true;
                    }
                    else
                    {
                        CloseServiceHandle(sc_hndl);
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
