using System.Collections.Generic;

namespace YANAN.Mail.Services.EmailFilter
{
    using YANAN.Mail.Utilities;

    /// <summary>
    /// 过滤器条件,等于数据库表实体MailFilterCondition
    /// </summary>
    public class FilterConditionCollection
    {
        /// <summary>
        /// 过滤器条件ID,
        /// </summary>
        public int FilterConditionId { get; set; }

        /// <summary>
        /// 过滤规则名称,
        /// </summary>
        public string FilterName { get; set; }

        /// <summary>
        /// 顺序号,顺序号,顺序
        /// </summary>
        public int SortNumber { get; set; }

        /// <summary>
        /// 邮箱编号ID,
        /// </summary>
        public string MailBoxId { get; set; }

        /// <summary>
        /// 过滤执行时机,过滤执行时机,收件时执行还是发送执行等, 0=收取时执行(默认值)、1=发件时执行、2=收取、发件都执行
        /// </summary>
        public int FilterDoTime { get; set; }

        /// <summary>
        /// 过滤执行条件,过滤执行条件，0=满足所有条件、1=满足任一条件
        /// </summary>
        public int ConditionOpertation { get; set; }

        /// <summary>
        /// 过滤条件规则,过滤条件,条件对象（json 字符串）
        /// </summary>
        public string FilterConditions { get; set; }

        /// <summary>
        /// 执行动作,执行动作,条件对象（json 字符串）
        /// </summary>
        public string FilterEvents { get; set; }

        /// <summary>
        /// 停止处理其他规则,
        /// </summary>
        public bool IsnoreOther { get; set; }

        /// <summary>
        /// 是否启用,是否启用过滤，true启用；false 未启用
        /// </summary>
        public bool IsFilter { get; set; }

        /// <summary>
        /// <summary>
        /// 是否在收取邮件时执行
        /// </summary>
        public bool IsReceiveMailDo { get { return FilterDoTime == 0 || FilterDoTime == 2; } }

        /// <summary>
        /// 是否在发送邮件时执行
        /// </summary>
        public bool IsSendMailDo { get { return FilterDoTime == 1 || FilterDoTime == 2; } }
        /// <summary>
        /// 条件集合
        /// </summary>
        public List<FilterCondition> FilterConditionsObject
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FilterConditions)) return new List<FilterCondition>();
                try
                {
                    return JsonSerializationHelper.JsonDeserialize<List<FilterCondition>>(FilterConditions);
                }
                catch
                {
                    return new List<FilterCondition>();
                }
            }
        }

        /// <summary>
        /// 执行动作
        /// </summary>
        public List<FilterEvent> FilterEventsObject
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FilterEvents)) return new List<FilterEvent>();
                try
                {
                    return JsonSerializationHelper.JsonDeserialize<List<FilterEvent>>(FilterEvents);
                }
                catch
                {
                    return new List<FilterEvent>();
                }
            }
        }
    }
}
