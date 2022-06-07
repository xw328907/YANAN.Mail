using System;
using System.Collections.Generic;
using System.Linq;

namespace YANAN.Mail.Services
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.IServices;
    using YANAN.Mail.Services.EmailFilter;
    using YANAN.Mail.Utilities;
    using YANAN.Mail.Utilities.Enums;
    using YANAN.Mail.Utilities.Extensions;
    using Titan;

    public partial class MailFilterService : IMailFilterService
    {

        /// <summary>
        /// 获取用户某一邮箱的过滤条件
        /// </summary>
        /// <param name="mailBoxId">邮箱ID</param>
        /// <returns></returns>
        public List<MailFilterCondition> GetListMailFilterCondition(LoginedUserInfo loginInfo, string mailBoxId)
        {
            if (string.IsNullOrWhiteSpace(mailBoxId))
                return new List<MailFilterCondition>();
            MailFilterConditions mailFilterConditions = new MailFilterConditions();
            var query = new QueryExpression() { EntityType = typeof(MailFilterCondition) };
            query.Selects.Add(MailFilterCondition_.ALL);
            query.Wheres.Add(MailFilterCondition_.OCode.TEqual(loginInfo.OCode));
            query.Wheres.Add(MailFilterCondition_.OwnerUID.TEqual(loginInfo.UserId));
            query.Wheres.Add(MailFilterCondition_.MailBoxId.TEqual(mailBoxId));
            query.OrderBys.Add(MailFilterCondition_.SortNumber.PropertyName, OrderType.Asc);
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                db.SelectCollection(mailFilterConditions.Items, query);
            }
            if (mailFilterConditions == null || mailFilterConditions.Items.Count < 1) return new List<MailFilterCondition>();
            return mailFilterConditions.Items;
        }
        /// <summary>
        /// 邮件过滤器保存/新增、修改
        /// </summary>
        /// <param name="filterConditionDto">过滤器数据对象dto</param>
        /// <returns></returns>
        public ResponseResult SaveMailFilterCondition(LoginedUserInfo loginInfo, MailFilterCondition filterCondition)
        {
            if (filterCondition == null) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            bool isAdd = false;
            if (filterCondition.FilterConditionId < 1) isAdd = true;
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.And };
                conditions.Add(MailFilterCondition_.FilterName.TEqual(filterCondition.FilterName));
                conditions.Add(MailFilterCondition_.MailBoxId.TEqual(filterCondition.MailBoxId));
                conditions.Add(MailFilterCondition_.OCode.TEqual(loginInfo.OCode));
                conditions.Add(MailFilterCondition_.OwnerUID.TEqual(loginInfo.UserId));
                if (!isAdd)
                {
                    conditions.Add(MailFilterCondition_.FilterConditionId.TNotEqual(filterCondition.FilterConditionId));
                }
                if (db.Exists<MailFilterCondition>(conditions))
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "规则名称已存在";
                    return result;
                }
                conditions = new ConditionExpressionCollection { ConditionRelation = ConditionRelation.And };
                conditions.Add(MailFilterCondition_.FilterConditions.TEqual(filterCondition.FilterConditions));
                conditions.Add(MailFilterCondition_.FilterEvents.TEqual(filterCondition.FilterEvents));
                conditions.Add(MailFilterCondition_.FilterDoTime.TEqual(filterCondition.FilterDoTime));
                conditions.Add(MailFilterCondition_.ConditionOpertation.TEqual(filterCondition.ConditionOpertation));
                conditions.Add(MailFilterCondition_.IsnoreOther.TEqual(filterCondition.IsnoreOther));
                conditions.Add(MailFilterCondition_.MailBoxId.TEqual(filterCondition.MailBoxId));
                conditions.Add(MailFilterCondition_.OCode.TEqual(loginInfo.OCode));
                conditions.Add(MailFilterCondition_.OwnerUID.TEqual(loginInfo.UserId));
                if (isAdd == false) { conditions.Add(MailFilterCondition_.FilterConditionId.TNotEqual(filterCondition.FilterConditionId)); }
                if (db.Exists<MailFilterCondition>(conditions))
                {
                    result.Code = ResponseCodeEnum.ERROR.ToString();
                    result.Message = "已存在相同规则的过滤器";
                    return result;
                }
                if (isAdd)
                {
                    filterCondition.CreateTime = DateTime.Now;
                    filterCondition.CreateUID = loginInfo.UserId;
                    filterCondition.OCode = loginInfo.OCode;
                    filterCondition.OwnerUID = loginInfo.UserId;
                    if (filterCondition.SortNumber < 1) filterCondition.SortNumber = 1;
                    db.Insert(filterCondition);
                }
                else
                {
                    MailFilterCondition oldFilterCondition = new MailFilterCondition { FilterConditionId = filterCondition.FilterConditionId };
                    bool flag = db.Select(oldFilterCondition, MailFilterCondition_.FilterConditionId, MailFilterCondition_.OwnerUID, MailFilterCondition_.OCode);
                    if (!flag)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.ErrorCode = ResponseErrorCodeEnum.ERROR_NOT_EXIST.ToString();
                        result.Message = ResponseErrorCodeEnum.ERROR_NOT_EXIST.GetDescription();
                        return result;
                    }
                    if (oldFilterCondition.OCode != loginInfo.OCode && oldFilterCondition.OwnerUID != loginInfo.UserId)
                    {
                        result.Code = ResponseCodeEnum.ERROR.ToString();
                        result.ErrorCode = ResponseErrorCodeEnum.NO_AUTHORITY.ToString();
                        result.Message = ResponseErrorCodeEnum.NO_AUTHORITY.GetDescription();
                        return result;
                    }
                    List<PropertyExpression> fields = new List<PropertyExpression> {
                        MailFilterCondition_.ConditionOpertation,MailFilterCondition_.FilterConditions,MailFilterCondition_.FilterDoTime,MailFilterCondition_.FilterEvents
                        ,MailFilterCondition_.FilterName,MailFilterCondition_.IsFilter,MailFilterCondition_.IsnoreOther,MailFilterCondition_.MailBoxId,MailFilterCondition_.SortNumber
                    };
                    db.Update(filterCondition, fields);
                }
                result.Data = filterCondition;
            }
            return result;
        }
        /// <summary>
        /// 过滤器删除
        /// </summary>
        /// <param name="ids">过滤器ID数组</param>
        /// <returns></returns>
        public ResponseResult RemoveMailFilterCondition(LoginedUserInfo loginInfo, int[] ids)
        {
            if (ids.Length < 1) return UtilityHelper.ReturnParameterNull();
            ResponseResult result = new ResponseResult();
            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                ConditionExpressionCollection conditions = new ConditionExpressionCollection
                {
                    ConditionRelation = ConditionRelation.And
                };
                conditions.Add(MailFilterCondition_.FilterConditionId.TIn(string.Join(",", ids)));
                conditions.Add(MailFilterCondition_.OCode.TEqual(loginInfo.OCode));
                conditions.Add(MailFilterCondition_.OwnerUID.TEqual(loginInfo.UserId));
                db.BatchDelete<MailFilterCondition>(conditions);
            }
            return result;
        }
        /// <summary>
        /// 检查邮件标签是否在过滤器中使用
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public bool CheckMailLabelUsedInFilter(LoginedUserInfo loginInfo, IDbSession db, string[] tagId)
        {
            QueryExpression query = new QueryExpression { EntityType = typeof(MailBox) };
            query.Selects.Add(MailBox_.MailAddress);
            query.Selects.Add(MailBox_.NickName);
            query.Selects.Add(MailBox_.SmtpServer);
            query.Selects.Add(MailBox_.MailBoxId);
            query.Wheres.Add(MailBox_.Deleted.TEqual(false));
            query.Wheres.Add(MailBox_.OwnerUID.TEqual(loginInfo.UserId));
            MailBoxs mailBoxs = new MailBoxs();
            db.SelectCollection(mailBoxs.Items, query);
            foreach (var mailBox in mailBoxs.Items)
            {
                List<FilterConditionCollection> filterConditionCollectionList = Filter.LoadConditions(db, mailBox.MailBoxId, mailBox.OCode);
                if (filterConditionCollectionList == null)
                {
                    filterConditionCollectionList = new List<FilterConditionCollection>();
                }
                foreach (var conditiongItem in filterConditionCollectionList)
                {
                    foreach (var eventItem in conditiongItem.FilterEventsObject)
                    {
                        if (eventItem.EventId == (int)MailFilterEventTypeEnum.SetLabel && tagId.Contains(eventItem.EventValue))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 更新过滤器中的邮件标签信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool UpdateTagToFilter(LoginedUserInfo loginInfo, IDbSession db, MailLabel sign)
        {
            QueryExpression query = new QueryExpression { EntityType = typeof(MailBox) };
            query.Selects.Add(MailBox_.MailAddress);
            query.Selects.Add(MailBox_.NickName);
            query.Selects.Add(MailBox_.SmtpServer);
            query.Selects.Add(MailBox_.MailBoxId);
            query.Wheres.Add(MailBox_.Deleted.TEqual(false));
            query.Wheres.Add(MailBox_.OwnerUID.TEqual(loginInfo.UserId));
            MailBoxs mailBoxs = new MailBoxs();
            db.SelectCollection(mailBoxs.Items, query);

            foreach (var mailBox in mailBoxs.Items)
            {
                List<FilterConditionCollection> filterConditionCollectionList = Filter.LoadConditions(db, mailBox.MailBoxId, mailBox.OCode);
                var isUpdate = false;
                if (filterConditionCollectionList == null)
                {
                    filterConditionCollectionList = new List<FilterConditionCollection>();
                }
                foreach (var conditiongItem in filterConditionCollectionList)
                {
                    foreach (var eventItem in conditiongItem.FilterEventsObject)
                    {
                        if (eventItem.EventId == (int)MailFilterEventTypeEnum.SetLabel && eventItem.EventValue == sign.MailLabelId.ToString())
                        {
                            eventItem.EventValueName = sign.MailLabelName + "|" + sign.Color;
                            isUpdate = true;
                        }
                    }
                    if (isUpdate)
                    {
                        conditiongItem.FilterConditions = JsonSerializationHelper.JsonSerialize(conditiongItem.FilterConditionsObject);
                        MailFilterCondition mailFilterCondition = new MailFilterCondition
                        {
                            FilterConditionId = conditiongItem.FilterConditionId,
                            FilterConditions = conditiongItem.FilterConditions
                        };
                        db.Update(mailFilterCondition, MailFilterCondition_.FilterConditions);
                    }
                }
            }
            return true;
        }
        //#endregion
    }
}
