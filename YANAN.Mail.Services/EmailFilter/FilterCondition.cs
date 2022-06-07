using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YANAN.Mail.Services.EmailFilter
{
    public class FilterCondition
    {
        /// <summary>
        /// 过滤条件Id
        /// </summary>
        public int ConditionId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int ConditionNum { get; set; }

        /// <summary>
        /// 过滤条件名称
        /// </summary>
        public string ConditionName { get; set; }
        /// <summary>
        /// 过滤方式：相等、包含、不包含、大于...真实值
        /// </summary>
        public EnumConditionOpration ConditionOpration { get; set; }
        /// <summary>
        /// 过滤方式：相等、包含、不包含、大于...显示值
        /// </summary>
        public string ConditionOprationName { get; set; }
        /// <summary>
        /// 条件真实值 
        /// </summary>
        public string ConditionValue { get; set; }
        /// <summary>
        /// 条件显示值
        /// </summary>
        public string ConditionValueName { get; set; }
        /// <summary>
        /// 是否区分大小写
        /// </summary>
        public bool? DistinctUpperLower { get; set; }
    }
}
