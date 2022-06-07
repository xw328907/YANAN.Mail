using YANAN.Mail.Entity;
using YANAN.Mail.Utilities;
using YANAN.Mail.Utilities.Enums;
using System;
using System.Collections.Generic;
using Titan;

namespace YANAN.Mail.Services.EmailFilter
{
    public static class Filter
    {
        private static Dictionary<int, FilterBase> filters = null;
        static Filter()
        {
            filters = new Dictionary<int, FilterBase>
                          {
                              {1100, new ContainFilter1100()},
                              {1101, new ContainFilter1101()},
                              {1102, new ContainFilter1102()},
                              {1200, new EqualFilter1200()},
                              {1201, new EqualFilter1201()},
                              {1202, new EqualFilter1202()},
                              {1300, new OrderFilter1300()}
                          };
        }

        public static bool IsMatch(FilterConditionCollection filterConditionCollection, MailMain mailMain)
        {
            bool isMatch = true;
            foreach (FilterCondition c in filterConditionCollection.FilterConditionsObject)
            {
                var b = false;
                switch (filterConditionCollection.ConditionOpertation)
                {
                    case 0: //满足所有条件
                        if (!filters[c.ConditionId].IsMatch(c, mailMain))
                        {
                            b = true;
                            isMatch = false;
                        }
                        else
                        {
                            isMatch = true;
                        }
                        break;
                    case 1: //满足任一条件
                        if (filters[c.ConditionId].IsMatch(c, mailMain))
                        {
                            b = true;
                            isMatch = true;
                        }
                        else
                        {
                            isMatch = false;
                        }
                        break;
                    case 2: //匹配所有邮件
                        b = true;
                        return true;
                    default:
                        b = true;
                        return true;
                }
                if (b)
                {
                    break;
                }
            }
            return isMatch;
        }


        public static List<FilterConditionCollection> LoadConditions(IDbSession db, string mailBoxId, string ocode)
        {
            List<FilterConditionCollection> filterConditionCollections = null;
            QueryExpression query = new QueryExpression { EntityType = typeof(MailFilterCondition) };
            query.Selects.Add(MailFilterCondition_.ALL);
            query.Wheres.Add(MailFilterCondition_.MailBoxId.TEqual(mailBoxId));
            query.Wheres.Add(MailFilterCondition_.OCode.TEqual(ocode));
            query.OrderBys.Add(MailFilterCondition_.SortNumber.PropertyName, OrderType.Asc);
            MailFilterConditions mailFilterConditions = new MailFilterConditions();
            db.SelectCollection(mailFilterConditions.Items, query);
            if (mailFilterConditions.Items.Count <= 0 || string.IsNullOrEmpty(mailFilterConditions.Items[0].FilterConditions))
                return null;
            filterConditionCollections = JsonSerializationHelper.JsonDeserialize<List<FilterConditionCollection>>(JsonSerializationHelper.JsonSerialize(mailFilterConditions.Items));
            return filterConditionCollections;
        }


        public static bool Handle(IDbSession db, MailMain mailMain)
        {
            List<FilterConditionCollection> filterConditionCollections = LoadConditions(db, mailMain.MailBoxId, mailMain.OCode);
            return Handle(filterConditionCollections, null, db, mailMain);

        }

        public static bool Handle(List<FilterConditionCollection> filterConditionCollections, int[] ids, IDbSession db, MailMain mailMain)
        {
            bool rtn = false;
            HashSet<int> hash = null;
            if (ids != null) hash = new HashSet<int>(ids);
            if (filterConditionCollections == null)
            {
                return false;
            }
            //Console.WriteLine(string.Format("开始过滤:{0}-----{1}---{2}", mailMain.Subject, FilterConditionCollections.Count, ids.Length));
            foreach (var filterConditionCollection in filterConditionCollections)
            {
                if (hash != null && !hash.Contains(filterConditionCollection.FilterConditionId))
                {
                    continue;
                }
                if (!filterConditionCollection.IsFilter)
                {
                    continue;
                }
                if (mailMain.MailType == 1 && !filterConditionCollection.IsReceiveMailDo)
                {
                    continue;
                }
                if (mailMain.MailType == 2 && !filterConditionCollection.IsSendMailDo)
                {
                    continue;
                }
                bool isMatchAndIsProcess = Filter.IsMatch(filterConditionCollection, mailMain);
                if (isMatchAndIsProcess)
                {
                    if (!rtn) rtn = true;
                    Console.WriteLine(string.Format("满足过滤条件{0}", mailMain.Subject));
                    foreach (var filterEvent in filterConditionCollection.FilterEventsObject)
                    {
                        string value = filterEvent.EventValue;
                        if (string.IsNullOrWhiteSpace(value)) continue;
                        switch (filterEvent.EventId)
                        {
                            case (int)MailFilterEventTypeEnum.MoveToFolder:
                                //移动到
                                MailService.MoveMailMainToFolder(db, string.Empty, value, mailMain.MailMainId);
                                break;
                            case (int)MailFilterEventTypeEnum.SetLabel:
                                //打标签
                                string[] labels = new string[] { value };
                                if (value.IndexOf(',') > -1)
                                {
                                    labels = value.Split(',');
                                }
                                foreach (string val in labels)
                                {
                                    if (string.IsNullOrWhiteSpace(val)) continue;
                                    MailService.MailAddLabel(db, mailMain.OCode, mailMain.OwnerUID, new string[] { mailMain.MailMainId }, val);
                                }
                                break;
                            case (int)MailFilterEventTypeEnum.SetRead://设置为已读
                                MailService.SetMailReadStatus(db, true, new string[] { mailMain.MailMainId });
                                break;
                            case (int)MailFilterEventTypeEnum.Deleted:
                                MailService.RemoveMail(db, mailMain.OCode, new string[] { mailMain.MailMainId });
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (isMatchAndIsProcess && filterConditionCollection.IsnoreOther) //执行过滤后忽略后边的过滤
                {
                    break;
                }
            }
            return rtn;
            //Logger.Write(string.Format("过滤完成:{0}", mailMain.MailFolderId), _logFolder);
        }
    }
}
