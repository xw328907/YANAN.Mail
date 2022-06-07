using YANAN.Mail.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace YANAN.Mail.Services.EmailFilter
{
    /// <summary>
    /// 相等过滤
    /// </summary>
    internal abstract class EqualFilter : FilterBase
    {
        protected bool IsEqual(object mailComparison, object filterComparison)
        {
            return mailComparison.Equals(filterComparison);
        }
    }
    /// <summary>
    /// 发件人等于
    /// </summary>
    internal class EqualFilter1200 : EqualFilter
    {
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            if (mailMain.Sender.IndexOf(";") > -1)
            {
                var values = mailMain.Sender.Split(';');
                return values.Any(x => x.IndexOf(filterCondition.ConditionValue) > -1);
            }
            string val = mailMain.Sender;
            var e = Regex.Match(val, "<.*?>");//匹配abc<abc@qq.com>中的abc@qq.com
            if (e.Success) val = e.Value;
            return IsEqual(mailMain.Sender, filterCondition.ConditionValue);
        }
    }
    /// <summary>
    /// 收件人等于
    /// </summary>
    internal class EqualFilter1201 : EqualFilter
    {
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            if (mailMain.Receiver.IndexOf(";") > -1)
            {
                var values = mailMain.Receiver.Split(';');
                return values.Any(x => x.IndexOf(filterCondition.ConditionValue) > -1);
            }
            string val = mailMain.Receiver;
            var e = Regex.Match(val, "<.*?>");//匹配abc<abc@qq.com>中的abc@qq.com
            if (e.Success) val = e.Value;
            return IsEqual(val, filterCondition.ConditionValue);
        }
    }
    /// <summary>
    /// 有/无附件
    /// </summary>
    internal class EqualFilter1202 : EqualFilter
    {
        public override bool IsMatch(FilterCondition filterCondition, MailMain mailMain)
        {
            if (filterCondition.ConditionOpration == EnumConditionOpration.Yes)
            {
                return mailMain.AttachCount > 0;
            }
            return mailMain.AttachCount <= 0;
        }
    }

}
