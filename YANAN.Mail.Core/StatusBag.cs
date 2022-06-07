using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Core
{
    using YANAN.Mail.Entity;
    public class StatusBag
    {
        public DateTime LatestClientVisitTime { get; set; }
        internal object LockObject = new object();
        public Queue<MailServerStatus> Status { get; set; }

        public StatusBag()
        {
            this.LatestClientVisitTime = DateTime.Now;
            this.Status = new Queue<MailServerStatus>();
        }

    }

    public class StatusBags : Dictionary<string, StatusBag>
    {
        private static int TO_REMOVE_SECONDS = -60;//几秒钟后移除状态

        private object lockObject = new object();

        public List<MailServerStatus> Dequeue(string key)
        {
            if (!ContainsKey(key)) return null;


            bool needRemoveKey = false;

            StatusBag statusBag = base[key];
            List<MailServerStatus> list = new List<MailServerStatus>(statusBag.Status.Count);
            statusBag.LatestClientVisitTime = DateTime.Now;
            if (statusBag.Status.Count > 0)
            {
                //只有count>0才有需要锁住对象
                lock (statusBag.LockObject)
                {
                    while (statusBag.Status.Count > 0)
                    {
                        MailServerStatus status = statusBag.Status.Dequeue();
                        list.Add(status);
                        if (status.StatusCode == StatusCode.Error || status.StatusCode == StatusCode.End)
                        {
                            needRemoveKey = true;
                            break;
                        }
                    }
                }
            }

            if (needRemoveKey)
            {
                lock (this.lockObject)
                {
                    if (ContainsKey(key))
                    {
                        Remove(key);
                    }
                }
            }
            return list;
        }
        public void Enqueue(string key, MailServerStatus status)
        {
            //移除状态必须放在这个方法里
            ClearTimeoutItems();

            if (!ContainsKey(key))
            {
                return;
            }
            StatusBag statusBag = base[key];
            lock (statusBag.LockObject)
            {
                statusBag.Status.Enqueue(status);
            }
        }

        public void RegisterKey(string key)
        {
            if (!ContainsKey(key))
            {
                lock (lockObject)
                {
                    if (!ContainsKey(key)) Add(key, new StatusBag());
                }
            }
            this[key].LatestClientVisitTime = DateTime.Now;
        }

        private void ClearTimeoutItems()
        {
            lock (lockObject)
            {
                List<string> removes = new List<string>();
                foreach (string key in this.Keys)
                {
                    StatusBag statusBag = this[key];
                    if (statusBag.LatestClientVisitTime < DateTime.Now.AddSeconds(TO_REMOVE_SECONDS))
                    {
                        removes.Add(key);
                    }
                }

                if (removes.Count > 0)
                {
                    foreach (string key in removes)
                    {
                        Remove(key);
                    }
                }
            }
        }
    }
}
