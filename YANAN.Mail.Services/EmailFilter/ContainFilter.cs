using YANAN.Mail.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Titan;

namespace YANAN.Mail.Services.EmailFilter
{
    /// <summary>
    /// 包含、不包含过滤
    /// </summary>
    internal abstract class ContainFilter : FilterBase
    {
        /// <summary>
        /// 判断邮件中的字段和设定条件中的是否匹配
        /// </summary>
        /// <param name="mailComparison"></param>
        /// <param name="filterComparison"></param>
        /// <param name="enumConditionOpration">条件类型：包含/不包含</param>
        /// <param name="distinctUpperLower">区分大小写，默认不区分</param>
        protected bool IsContain(string mailComparison, string filterComparison, EnumConditionOpration enumConditionOpration, bool distinctUpperLower = false)
        {
            string mailComparison2 = mailComparison + "";
            filterComparison = filterComparison + "";
            if (distinctUpperLower == false)
            {
                filterComparison = filterComparison.ToLower();
                mailComparison2 = mailComparison2.ToLower();
            }
            if (enumConditionOpration == EnumConditionOpration.Contain)
            {
                return mailComparison2.Contains(filterComparison);
            }
            return !mailComparison2.Contains(filterComparison);
        }
    }
    /// <summary>
    /// 发件人地址包含
    /// </summary>
    internal class ContainFilter1100 : ContainFilter
    {
        /// <summary>
        ///  发件人地址包含/不包含
        /// </summary>
        /// <param name="filterCondition">过滤条件对象</param>
        /// <param name="mailMain">邮件信息对象</param>
        /// <returns></returns>
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            return IsContain(mailMain.Sender, filterCondition.ConditionValue, filterCondition.ConditionOpration);
        }
    }
    /// <summary>
    /// 收件人地址
    /// </summary>
    internal class ContainFilter1101 : ContainFilter
    {
        /// <summary>
        /// 收件人地址包含/不包含
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="mailMain"></param>
        /// <returns></returns>
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            return IsContain(mailMain.Receiver, filterCondition.ConditionValue, filterCondition.ConditionOpration);
        }
    }
    /// <summary>
    /// 主题
    /// </summary>
    internal class ContainFilter1102 : ContainFilter
    {
        /// <summary>
        /// 邮件主题包含/不包含
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="mailMain"></param>
        /// <returns></returns>
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            return IsContain(mailMain.Subject, filterCondition.ConditionValue, filterCondition.ConditionOpration, filterCondition.DistinctUpperLower == true);
        }
    }

}
