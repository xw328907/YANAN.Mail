using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YANAN.Mail.Utilities
{
    using YANAN.Mail.Utilities.Enums;

    /// <summary>
    /// 查询条件集合
    /// </summary>
    [DataContract, Serializable]
    public class QueryParameter
    {
        /// <summary>
        /// 每页数量， Less than or equal 0 means return all records,if this is too larg,it will be set to 300
        /// </summary>
        [DataMember]
        public int? PageSize { get; set; }

        /// <summary>
        /// 页码，PageIndex is start from 1,if value is Less than or equal 0,will be set to 1
        /// </summary>
        [DataMember]
        public int? PageIndex { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string[] OutPutProPertys { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        [DataMember]
        public List<QueryCondition> ConditionCollection { get; set; }

        /// <summary>
        /// 2级条件
        /// </summary>
        [DataMember]
        public List<ChildQueryCondition> ChildConditionCollection { get; set; }
        /// <summary>
        /// Order By
        /// </summary>
        [DataMember]
        public Dictionary<string, QueryOrderTypes> OrderBys { get; set; }

        [DataMember]
        public string KeyWords { get; set; }

    }
    /// <summary>
    /// 排序方式,倒序/顺序
    /// </summary>
    [DataContract, Serializable]
    public enum QueryOrderTypes
    {
        /// <summary>
        /// 顺序
        /// </summary>
        [EnumMember]
        Asc = 1,
        /// <summary>
        /// 倒序
        /// </summary>
        [EnumMember]
        Desc = 2
    }
    /// <summary>
    /// 条件关系,并且/或
    /// </summary>
    [DataContract, Serializable]
    public enum ERelation
    {
        /// <summary>
        /// 并且
        /// </summary>
        [EnumMember]
        And = 0,
        /// <summary>
        /// 或
        /// </summary>
        [EnumMember]
        Or = 1,
    }
    /// <summary>
    /// 查询条件
    /// </summary>
    [DataContract, Serializable]
    public class QueryCondition
    {
        public QueryCondition()
        {
        }
        public QueryCondition(string propertyName, ConditionOperatorEnum op, string value)
        {
            this.PropertyName = propertyName;
            this.Operator = op;
            this.Value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PropertyName { get; set; }
        /// <summary>
        /// like ,not like,Greater than,less than,equal,less than or equal,Greater than or equal,not equal.....
        /// </summary>
        [DataMember]
        public ConditionOperatorEnum Operator { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
    /// <summary>
    /// 子查询条件
    /// </summary>
    [DataContract, Serializable]
    public class ChildQueryCondition
    {
        [DataMember]
        public ERelation Relation { get; set; }
        [DataMember]
        public List<QueryCondition> ConditionCollection { get; set; }
        [DataMember]
        public List<ChildQueryCondition> ConditionCollection1 { get; set; }
    }

}
