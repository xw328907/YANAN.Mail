using System.ComponentModel;

namespace YANAN.Mail.Utilities.Enums
{
    /// <summary>
    /// 查询条件操作类型(等于、小于、大于...)
    /// </summary>
    public enum ConditionOperatorEnum
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal = 0,
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GreaterThan = 1,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LessThan = 2,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterThanOrEqual = 3,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessThanOrEqual = 4,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEqual = 5,
        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Like = 6,
        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        NotLike = 7,
        /// <summary>
        /// 包括
        /// </summary>
        [Description("包括")]
        In = 8,
        /// <summary>
        /// 不包括
        /// </summary>
        [Description("不包括")]
        NotIn = 9,
        /// <summary>
        /// 左包含
        /// </summary>
        [Description("左包含")]
        LeftLike = 10,
        /// <summary>
        /// 左不包含
        /// </summary>
        [Description("左不包含")]
        NotLeftLike = 11,
        /// <summary>
        /// 右包含
        /// </summary>
        [Description("右包含")]
        RightLike = 12,
        /// <summary>
        /// 右不包含
        /// </summary>
        [Description("右不包含")]
        NotRightLike = 13,
        /// <summary>
        /// 文本包含(数据库text类型不能like)
        /// </summary>
        [Description("文本包含")]
        FullTextLike = 14,
        /// <summary>
        /// 文本不包含(数据库text类型不能like)
        /// </summary>
        [Description("等于")]
        NotFullTextLike = 15,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Custom = 99
    }
}
