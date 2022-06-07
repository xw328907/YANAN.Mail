using System;
using System.Linq;
using System.Collections.Generic;

namespace YANAN.Mail.Client.Filter
{
    using Titan;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Services;
    using YANAN.Mail.Services.EmailFilter;
    using YANAN.Mail.Core.ThreadTask;
    using YANAN.Mail.Utilities.Enums;

    public class FilterWorker : TaskBase
    {
        /// <summary>
        /// 公司编码
        /// </summary>
        public string OCode { get; set; }
        /// <summary>
        /// 待过滤目录ID编号数组
        /// </summary>
        public string[] MailFolderIds { get; set; }
        /// <summary>
        /// 过滤器条件表ID编号数组
        /// 一个邮局下有多个过滤规则，可以只指定若干个过滤规则执行
        /// </summary>
        public int[] FilterCollectionIds { get; set; }
        /// <summary>
        /// 邮箱ID编号
        /// </summary>
        public string MailBoxId { get; set; }

        /// <summary>
        /// 当前过滤步骤
        /// </summary>
        public FilterStep CurrentFilterStep;
        /// <summary>
        /// 当前异常
        /// </summary>
        public Exception CurrentException;
        /// <summary>
        /// 任务数
        /// </summary>
        public int TaskCount;
        /// <summary>
        /// 完成数
        /// </summary>
        public int CompletedCount;
        /// <summary>
        /// 已匹配数
        /// </summary>
        public int MatchedCount;

        public override void DoWork()
        {

            CurrentFilterStep = FilterStep.Prepare;
            Working();

            #region 加载任务数
            List<string> mailMainIds = new List<string>();
            List<FilterConditionCollection> filterConditionCollections = null;

            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                filterConditionCollections = Filter.LoadConditions(db, MailBoxId, OCode);
                if (MailFolderIds != null && MailFolderIds.Length > 0)
                { mailMainIds = LoadTasks(db, MailFolderIds); }
            }
            TaskCount = mailMainIds.Count;
            #endregion

            #region 循环每个任务
            foreach (string mailMainId in mailMainIds)
            {
                //lock (Helper.CompanyCaches[OCode].DbLock)
                {
                    using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
                    {
                        try
                        {
                            MailMain mailMain = new MailMain { MailMainId = mailMainId };
                            var selectFields = new List<PropertyExpression> { MailMain_.MailBoxId, MailMain_.MailMainId, MailMain_.MailType, MailMain_.OwnerUID
                                , MailMain_.OCode, MailMain_.CreateUID,MailMain_.Deleted,MailMain_.Viewed,MailMain_.AttachCount,MailMain_.MailSize,MailMain_.Subject
                                ,MailMain_.Sender,MailMain_.Receiver,MailMain_.Cc };
                            bool flag = db.Select(mailMain, selectFields);
                            if (!flag) continue;
                            db.BeginTransaction();
                            bool matched = Filter.Handle(filterConditionCollections, FilterCollectionIds, db, mailMain);
                            if (matched)
                            {
                                MatchedCount++;
                            }
                            db.Commit();
                        }
                        catch (Exception ex)
                        {
                            db.Rollback();
                            CurrentException = ex;
                            Error();
                        }
                    }
                }

                CompletedCount++;
                CurrentFilterStep = FilterStep.Filting;
                Working();
            }

            #endregion

            CurrentFilterStep = FilterStep.End;
            Working();
        }

        private List<string> LoadTasks(IDbSession db, string[] mailFolderIds)
        {
            List<string> list = new List<string>();
            MailMainFolders objs = new MailMainFolders();
            QueryExpression query = new QueryExpression { EntityType = typeof(MailMainFolder) };
            query.Selects.Add(MailMainFolder_.MailMainId);
            query.Wheres.Add(MailMainFolder_.MailFolderId.TIn("'" + string.Join("','", mailFolderIds) + "'"));
            db.SelectCollection(objs.Items, query);
            if (objs != null && objs.Items.Count > 0)
            {
                objs.Items.ForEach(item => { list.Add(item.MailMainId); });
            }
            return list;
        }

        private void Error()
        {
            MailServerStatus status = new MailServerStatus
            {
                StatusCode = StatusCode.Error,
                Message = string.Format("过滤错误，错误详情为：{0}", CurrentException.Message)
            };

            FilterHost.StatusBags.Enqueue(Key, status);
            Console.WriteLine(Key + "\t" + status.ToString());
            Core.Logger.WriteError(CurrentException);
        }
        private void Working()
        {
            MailServerStatus status = new MailServerStatus();
            if (CurrentFilterStep == FilterStep.Prepare)
            {
                status.StatusCode = StatusCode.Info;
                status.Message = "正在加载待过滤邮件";
            }
            else if (CurrentFilterStep == FilterStep.Filting)
            {
                status.StatusCode = StatusCode.Progress;
                status.Message = string.Format("正在过滤 {0}/{1}", CompletedCount, TaskCount);
                status.CompletedCount = CompletedCount;
                status.TaskCount = TaskCount;
            }
            else if (CurrentFilterStep == FilterStep.End)
            {
                status.StatusCode = StatusCode.End;
                status.Message = "过滤完成";
                status.CompletedCount = MatchedCount;
                status.TaskCount = TaskCount;
            }
            if (!string.IsNullOrWhiteSpace(status.Message))
            {
                FilterHost.StatusBags.Enqueue(Key, status);
                Console.WriteLine(Key + "\t" + status.ToString());
            }
        }

        public override void OnWorkError(Exception exception)
        {
            Core.Logger.WriteError(CurrentException);
            base.OnWorkError(exception);
        }
    }
}
