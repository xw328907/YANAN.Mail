using YANAN.Mail.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Services.EmailFilter
{
    internal abstract class FilterBase
    {
        public abstract bool IsMatch(FilterCondition filterCondition,MailMain mailMain);
    }
}
