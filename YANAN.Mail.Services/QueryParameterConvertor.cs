using System;
using System.Collections.Generic;

namespace YANAN.Mail.Services
{
    using Titan;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    /// <summary>
    /// 
    /// </summary>
    public class QueryParameterConvertor
    {
        private static ConditionOperator ToTitanConditionOperator(QueryCondition queryCondition)
        {
            ConditionOperator o;
            switch (queryCondition.Operator)
            {
                case ConditionOperatorEnum.Custom: o = ConditionOperator.Custom; break;
                case ConditionOperatorEnum.Equal: o = ConditionOperator.Equal; break;
                case ConditionOperatorEnum.GreaterThan: o = ConditionOperator.GreaterThan; break;
                case ConditionOperatorEnum.GreaterThanOrEqual: o = ConditionOperator.GreaterThanOrEqual; break;
                case ConditionOperatorEnum.In: o = ConditionOperator.In; break;
                case ConditionOperatorEnum.LeftLike: o = ConditionOperator.LeftLike; break;
                case ConditionOperatorEnum.NotLeftLike: o = ConditionOperator.NotLeftLike; break;
                case ConditionOperatorEnum.LessThan: o = ConditionOperator.LessThan; break;
                case ConditionOperatorEnum.LessThanOrEqual: o = ConditionOperator.LessThanOrEqual; break;
                case ConditionOperatorEnum.Like: o = ConditionOperator.Like; break;
                case ConditionOperatorEnum.NotEqual: o = ConditionOperator.NotEqual; break;
                case ConditionOperatorEnum.NotIn: o = ConditionOperator.NotIn; break;
                case ConditionOperatorEnum.NotLike: o = ConditionOperator.NotLike; break;
                case ConditionOperatorEnum.RightLike: o = ConditionOperator.RightLike; break;
                case ConditionOperatorEnum.NotRightLike: o = ConditionOperator.NotRightLike; break;
                default: throw new Exception(string.Format("查询条件包含无效操作符{0}", queryCondition.Operator));
            }
            return o;
        }

        public static ConditionExpressionCollection RecurseCondition(ChildQueryCondition cqc)
        {
            ConditionExpressionCollection ccp = new ConditionExpressionCollection
            {
                ConditionRelation = cqc.Relation ==
                                                ERelation.Or ? ConditionRelation.Or : ConditionRelation.And
            };
            if (cqc.ConditionCollection != null)
            {
                foreach (var c in cqc.ConditionCollection)
                {
                    ccp.Add(c.PropertyName, ToTitanConditionOperator(c), c.Value);
                }
            }
            if (cqc.ConditionCollection1 != null)
            {
                foreach (var cqc1 in cqc.ConditionCollection1)
                {
                    ccp.Add(RecurseCondition(cqc1));
                }
            }
            return ccp;
        }

        public static QueryExpression ToQueryExpression(QueryParameter queryParameter)
        {

            QueryExpression opt = new QueryExpression();
            if (queryParameter != null)
            {
                if (queryParameter.ConditionCollection != null)
                {
                    foreach (QueryCondition c in queryParameter.ConditionCollection)
                    {
                        opt.Wheres.Add(c.PropertyName, ToTitanConditionOperator(c), c.Value);
                    }
                }
                if (queryParameter.ChildConditionCollection != null)//2级条件
                {
                    foreach (ChildQueryCondition c in queryParameter.ChildConditionCollection)
                    {
                        opt.Wheres.Add(RecurseCondition(c));
                    }
                }

                if (queryParameter.OrderBys != null)
                {
                    foreach (KeyValuePair<string, QueryOrderTypes> ord in queryParameter.OrderBys)
                    {
                        opt.OrderBys.Add(ord.Key, ord.Value == QueryOrderTypes.Asc ? OrderType.Asc : OrderType.Desc);
                    }
                }
                ////select field 暂不由处理
                //if (queryParameter.OutPutProPertys != null && queryParameter.OutPutProPertys.Length > 0)
                //{
                //    foreach (string OutPutProPerty in queryParameter.OutPutProPertys)
                //    {
                //        opt.Selects.Add(OutPutProPerty);
                //    }
                //}
                //else
                //{
                //    opt.Selects.Add("*");
                //}
                if (!opt.PageSize.HasValue)
                    opt.PageSize = queryParameter.PageSize;
                if (!opt.PageIndex.HasValue)
                    opt.PageIndex = queryParameter.PageIndex;
                if (!opt.PageIndex.HasValue || opt.PageIndex < 1)
                {
                    opt.PageIndex = 1;
                }
            }
            return opt;
        }
    }
}
