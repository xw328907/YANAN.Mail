using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YANAN.Mail.Services.EmailFilter
{
    public class FilterEvent
    {
        /// <summary>
        /// 过滤执行动作（方式）
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int EventNum { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        ///真实值
        /// </summary>
        public string EventValue { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        public string EventValueName { get; set; }

    }
}
