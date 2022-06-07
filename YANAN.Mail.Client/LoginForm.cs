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
    using YANAN.Mail.Entity;
    using YANAN.Mail.IServices;
    using YANAN.Mail.Services;
    public partial class LoginForm : BaseForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginUserInfo loginUserInfo = new LoginUserInfo { UserCode = txtUserCode.Text.Trim(), OCode = txtOCode.Text.Trim(), LoginPwd = txtUserPwd.Text.Trim() };
            //if (string.IsNullOrWhiteSpace(loginUserInfo.OCode))
            //{
            //    MessageBox.Show("公司编号不能为空");
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(loginUserInfo.UserCode))
            //{
            //    MessageBox.Show("用户名不能为空");
            //    return;
            //}
            loginUserInfo =new LoginUserInfo
            {
                OCode = "10000",
                UserId = "4837D4B1-7FA3-4369-A808-A1A049C43CD1",
                UserCode = "1001",
                UserName = "汪志华",
                LoginPwd = "1"
            };
            bool flag = Login(loginUserInfo);
            if (flag)
                DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
            Application.Exit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static bool Login(LoginUserInfo userInfo)
        {
            LoginedUserInfo loginedUserInfo = new LoginedUserInfo
            {
                UserId = userInfo.UserId,
                OCode = userInfo.OCode,
                UserCode = userInfo.UserCode,
                UserName = userInfo.UserName,
                Token = Utilities.UtilityHelper.GetGuid()
            };
            CurrentUserInfo.LoginSuccess(loginedUserInfo);
            return true;
        }

    }
}
