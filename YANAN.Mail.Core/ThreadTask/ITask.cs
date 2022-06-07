using System;

namespace YANAN.Mail.Core.ThreadTask
{
    /// <summary>
    /// The Task interface.
    /// </summary>
    public interface ITask
    {
        //event EventHandler WorkBegin;

        //event EventHandler WorkError;

        //event EventHandler WorkCompleted;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the TaskState.
        /// </summary> 
        TaskState TaskState { get; set; }

        /// <summary>
        /// 要执行的工作
        /// </summary>
        void DoWork();

        /// <summary>
        /// The on work begin.
        /// </summary>
        void OnWorkBegin();

        /// <summary>
        /// The on work error.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void OnWorkError(Exception exception);

        /// <summary>
        /// The on work completed.
        /// </summary>
        void OnWorkCompleted();
    }
}
