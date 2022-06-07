
namespace YANAN.Mail.Entity
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class LoginedUserInfo
    {
        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string OCode { get; set; }
        /// <summary>
        /// 用户ID,guid
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录时分配的token值,用于校验当前用户的真实有效性
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 用户头像URL
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginUserInfo
    {
        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string OCode { get; set; }
        /// <summary>
        /// 用户ID,guid，用于客户端嵌套直接调用
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }

    }
}
