using System; 

namespace YANAN.Mail.Core.ThreadTask
{
    /// <summary>
    /// The task base.
    /// </summary>
    public abstract class TaskBase : ITask
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets the task state.
        /// </summary>
        public virtual TaskState TaskState { get; set; }

        /// <summary>
        /// The on work begin.
        /// </summary>
        public virtual void OnWorkBegin()
        {
        }

        /// <summary>
        /// The on work error.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public virtual void OnWorkError(Exception exception)
        {
        }

        /// <summary>
        /// The on work completed.
        /// </summary>
        public virtual void OnWorkCompleted()
        {
        }

        /// <summary>
        /// The do work.
        /// </summary>
        public abstract void DoWork();
    }
}
