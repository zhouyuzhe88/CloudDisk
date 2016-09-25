
using Common.Protocol;
using System;

namespace Client.Task
{
    abstract class Task
    {
        protected abstract TCPClientType ClientType { get; }

        protected ITaskDelegate TaskDelegate { get; set; }

        internal Action TaskStartedCallback { get; set; }

        internal Action<Response, bool> TaskCompletedCallback { get; set; }

        internal abstract void Work();

        protected Task(ITaskDelegate taskDelegate)
        {
            TaskDelegate = taskDelegate;
        }
    }
}
