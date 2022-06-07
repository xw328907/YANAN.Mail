namespace YANAN.Mail.Client
{
    using YANAN.Mail.Entity;
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    public class CurrentUserInfo
    {
        private static LoginedUserInfo userInfo = null;
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public static LoginedUserInfo GetLoginedUserInfo()
        {
            if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.UserId) && !string.IsNullOrWhiteSpace(userInfo.OCode)) return new LoginedUserInfo();
            return userInfo;
        }
        /// <summary>
        /// 是否已登录,true=已登录
        /// </summary>
        public static bool IsLogined
        {
            get
            {
                var loginInfo = GetLoginedUserInfo();
                if (loginInfo != null && !string.IsNullOrWhiteSpace(loginInfo.UserId) && !string.IsNullOrWhiteSpace(loginInfo.OCode)) return true;
                return false;
            }
        }
        /// <summary>
        /// 登录成功记录登录用户信息
        /// </summary>
        /// <param name="loginedUserInfo"></param>
        public static void LoginSuccess(LoginedUserInfo loginedUserInfo)
        {
            userInfo = loginedUserInfo;
        }
    }

}
