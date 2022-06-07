using System.Runtime.Serialization;

namespace YANAN.Mail.Entity
{
    /// <summary>
    /// 收发进度状态
    /// </summary>
    [DataContract]
    public class MailServerStatus
    {
        [DataMember]
        public StatusCode StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public int CompletedCount { get; set; }

        [DataMember]
        public int TaskCount { get; set; }


        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", StatusCode, CompletedCount, TaskCount, Message);
        }
    }
    /// <summary>
    /// 收发进度状态枚举值
    /// </summary>
    [DataContract]
    public enum StatusCode
    {
        [EnumMember]
        Info = 1,

        ///// <summary>
        ///// 中途出现异常则视为警告，是避免过程终止
        ///// </summary>
        //[EnumMember]
        //Warn = 2,

        [EnumMember]
        Progress = 3,
        /// <summary>
        /// 出现错误，应当终止过程
        /// </summary>
        [EnumMember]
        Error = 4,
        /// <summary>
        /// 自然结束
        /// </summary>
        [EnumMember]
        End = 99,
    }
}
