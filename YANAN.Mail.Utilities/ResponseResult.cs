
namespace YANAN.Mail.Utilities
{
    using YANAN.Mail.Utilities.Enums;
    using YANAN.Mail.Utilities.Extensions;

    /// <summary>
    /// 输出返回json对象
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 输出返回json对象，初始化Code为SUCCESS
        /// </summary>
        public ResponseResult()
        {
            Code = ResponseCodeEnum.SUCCESS.ToString();
            Message = ResponseCodeEnum.SUCCESS.GetDescription();
        }
        /// <summary>
        /// 返回的状态值,详见枚举ResponseCode
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误详细码,当Code=ERROR时才会有值
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 消息提示
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据对象
        /// </summary>
        public object Data { get; set; }
    }
}
