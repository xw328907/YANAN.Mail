using YANAN.Mail.Core;
using YANAN.Mail.Core.ThreadTask;

namespace YANAN.Mail.Client.Filter
{
    public static class FilterHost
    {
        public static StatusBags StatusBags { get; } = new StatusBags();

        /// <summary>
        /// 过滤服务器线程池
        /// </summary>
        public static TaskPool FilterPool = null;
        /// <summary>
        /// 启动过滤服务
        /// </summary>
        public static void Start()
        {
            FilterPool = TaskPool.Create("Filter", Const.FilterThreadCount);
        }
    }
}
