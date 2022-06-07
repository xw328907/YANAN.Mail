using System;
using System.Threading;
using System.Collections.Generic;

namespace YANAN.Mail.Core.ThreadTask
{
    /// <summary>
    /// The task pool.
    /// </summary>
    public class TaskPool
    {

        #region static

        /// <summary>
        /// The dictionary.
        /// </summary>
        private static readonly Dictionary<string, TaskPool> TaskPools = new Dictionary<string, TaskPool>();
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="threadPoolName">
        /// The thread pool name.
        /// </param>
        /// <param name="threadCount">
        /// The thread count.
        /// </param>
        /// <returns>
        /// The <see cref="TaskPool"/>.
        /// </returns>
        public static TaskPool Create(string threadPoolName, int threadCount = 4)
        {
            if (!TaskPools.ContainsKey(threadPoolName))
            {
                TaskPool taskPool = new TaskPool(threadPoolName, threadCount);
                TaskPools.Add(threadPoolName, taskPool);
                return taskPool;
            }

            return TaskPools[threadPoolName];
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="threadPoolName">
        /// The thread pool name.
        /// </param>
        public static void Remove(string threadPoolName)
        {
            if (TaskPools.ContainsKey(threadPoolName))
            {
                TaskPools.Remove(threadPoolName);
            }
        }
        #endregion

        /// <summary>
        /// The lock object.
        /// </summary>
        private readonly object lockObject = new object(); //锁对象

        /// <summary>
        /// The tasks.
        /// </summary>
        private readonly Dictionary<string, ITask> tasks = new Dictionary<string, ITask>();

        /// <summary>
        /// The waiting tasks.
        /// </summary>
        private readonly List<ITask> waitingTasks = new List<ITask>();

        /// <summary>
        /// The threads.
        /// </summary>
        private readonly Thread[] threads;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskPool"/> class.
        /// </summary>
        /// <param name="threadPoolName">
        /// The thread pool name.
        /// </param>
        /// <param name="threadCount">
        /// The thread count.
        /// </param>
        /// <exception cref="ArgumentException">
        /// a
        /// </exception>
        internal TaskPool(string threadPoolName, int threadCount)
        {
            if (threadCount < 1)
            {
                throw new ArgumentException("threadCount必须大于0", "threadCount");
            }

            this.ThreadPoolName = threadPoolName;

            //int threadCount = 4;
            //int num = 0;
            //if (int.TryParse(ConfigurationManager.AppSettings["SendThreadCount"], out num)) threadCount = num;
            this.threads = new Thread[threadCount];

            //Thread thread = new Thread(delegate() { this.ThreadStart(monitorSeconds * 1000); });
        }

        /// <summary>
        /// Gets the thread pool name.
        /// </summary>
        public string ThreadPoolName { get; private set; }


        //test completed thread
        //public List<Thread> completedThreads = new List<Thread>();

        ////开始任务
        //private void ThreadStart(int sleep)
        //{
        //    while (true)
        //    {
        //        StartNextTask();
        //        Thread.Sleep(sleep);
        //    }
        //} 

        /// <summary>
        /// 添加的序列的结尾
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        public void AddToEnd(ITask task)
        {
            bool added = false;
            if (!this.tasks.ContainsKey(task.Key))
            {
                lock (this.lockObject)
                {
                    if (!this.tasks.ContainsKey(task.Key))
                    {
                        task.TaskState = TaskState.Waiting;
                        this.tasks.Add(task.Key, task);
                        this.waitingTasks.Add(task);
                        added = true;
                    }
                }
            }

            if (added)
            {
                this.StartNextTask();
            }
        }

        /// <summary>
        /// 添加的序列的开始（会提升task的执行优选级）
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        public void AddToBegin(ITask task)
        {
            bool added = false;
            lock (this.lockObject)
            {
                if (!this.tasks.ContainsKey(task.Key))
                {
                    task.TaskState = TaskState.Waiting;
                    this.tasks.Add(task.Key, task);
                    this.waitingTasks.Insert(0, task);
                    added = true;
                }
                else
                {
                    if (this.tasks[task.Key].TaskState == TaskState.Waiting)
                    {
                        this.waitingTasks.RemoveAll(p => p.Key == task.Key);
                        this.waitingTasks.Insert(0, task);
                        added = true;
                    }
                }
            }

            if (added)
            {
                this.StartNextTask();
            }
        }

        /// <summary>
        /// The task exists.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool TaskExists(string key)
        {
            return this.tasks.ContainsKey(key);
        }

        public ITask GetTask(string key)
        {
            return this.tasks[key];
        }

        /// <summary>
        /// The thread do work.
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        /// <param name="threadIndex">
        /// The thread index.
        /// </param>
        private void ThreadDoWork(ITask task, int threadIndex)
        {
            task.TaskState = TaskState.Working;

            try
            {
                task.OnWorkBegin();
                task.DoWork();
                task.OnWorkCompleted();
            }
            catch (Exception ex)
            {
                task.OnWorkError(ex);
            }

            task.TaskState = TaskState.Completed; //保持联动，短暂的状态也要保留

            this.StartNextTask(task, threadIndex);
        }

        /// <summary>
        /// The start next task.
        /// </summary>
        /// <param name="completedtTask">
        /// The completedt task.
        /// </param>
        /// <param name="completedThreadIndex">
        /// The completed thread index.
        /// </param>
        private void StartNextTask(ITask completedtTask, int completedThreadIndex)
        {
            lock (this.lockObject)
            {
                if (completedtTask != null)
                {
                    this.tasks.Remove(completedtTask.Key);

                    //completedThreads.Add(threads[completedThreadIndex]);
                    this.threads[completedThreadIndex] = null;
                }

                //找出包括completedThreadIndex在内的空闲线程
                for (int i = 0; i < this.threads.Length && this.waitingTasks.Count > 0; i++)
                {
                    if (i == completedThreadIndex || this.threads[i] == null || this.threads[i].ThreadState == ThreadState.Stopped)
                    {
                        //说明已经找到空闲线程
                        int j = i;
                        ITask task = this.waitingTasks[0];
                        this.waitingTasks.RemoveAt(0);
                        this.threads[j] = new Thread(delegate () { this.ThreadDoWork(task, j); })
                        {
                            IsBackground = true
                        };
                        this.threads[j].Start();
                    }
                }
            }
        }

        /// <summary>
        /// The start next task.
        /// </summary>
        private void StartNextTask()
        {
            this.StartNextTask(null, -1);
        }
    }
}
