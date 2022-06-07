using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANAN.Mail.Core.ThreadTask
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum TaskState
    {
        Waiting=0,//等待中
        Working=1,//正在执行
        Completed=2,//已完成//需要保留，因为在外部这个实例可能一直存在
        //KeyExist=3,//添加了相同的键，导致相同的任务
        //NotFound=4//为找到任务:任务本身不存在，任务已经完成
    }
}
