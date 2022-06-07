using YANAN.Mail.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Services.EmailFilter
{
    /// <summary>
    /// 大于、小于过滤
    /// </summary>
    internal abstract class OrderFilter : FilterBase
    {

    }

    /// <summary>
    /// 邮件大小(KB)
    /// </summary>
    internal class OrderFilter1300 : OrderFilter
    {
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            if (string.IsNullOrWhiteSpace(filterCondition.ConditionValue)) return true;
            var value = filterCondition.ConditionValue.Split('-');
            if (value.Length < 2) return true;
            int.TryParse(value[1].Trim(), out int maxValue);
            int.TryParse(value[1].Trim(), out int minValue);
            maxValue = maxValue * 1024;//kb转换为字节
            minValue = minValue * 1024;//kb转换为字节
            if (maxValue <= 0)
            {
                return mailMain.MailSize >= minValue;
            }
            if (minValue <= 0)
            {
                return mailMain.MailSize <= maxValue;
            }
            return mailMain.MailSize >= minValue && mailMain.MailSize <= maxValue;
        }
    }
}
