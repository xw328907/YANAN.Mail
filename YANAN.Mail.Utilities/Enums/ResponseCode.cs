using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 服务端响应码枚举
    /// </summary>
    public enum ResponseCodeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        SUCCESS,
        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        ERROR,
        /// <summary>
        /// 登陆超时
        /// </summary>
        [Description("登陆超时")]
        LOGOUT
    }
    /// <summary>
    /// 详细错误响应码
    /// </summary>
    public enum ResponseErrorCodeEnum
    {
        /// <summary>
        /// 参数无效
        /// </summary>
        [Description("参数无效")]
        ERROR_PARAMETER,
        /// <summary>
        /// 无权操作
        /// </summary>
        [Description("无权操作")]
        NO_AUTHORITY,
        /// <summary>
        /// 记录不存在
        /// </summary>
        [Description("记录不存在")]
        ERROR_NOT_EXIST,
        /// <summary>
        /// 记录已存在
        /// </summary>
        [Description("记录已存在")]
        ERROR_EXIST
    }
}
