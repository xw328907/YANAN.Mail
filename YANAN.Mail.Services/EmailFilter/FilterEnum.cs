using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YANAN.Mail.Services.EmailFilter
{
    /// <summary>
    /// 过滤条件类型 ，包含，相等，大于 等等
    /// </summary>
    public enum EnumConditionOpration
    {
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes = 1,
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 0,
        /// <summary>
        /// 相等
        /// </summary>
        [Description("相等")]
        Equal = 2,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NoEqual = 3,
        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Contain = 4,
        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        UnContain = 5,
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GreaterThan = 6,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LessThan = 7,
        /// <summary>
        /// 区间，如：大于1 小于5
        /// </summary>
        [Description("区间")]
        Between = 8
    }
}
