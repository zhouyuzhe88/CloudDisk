using Common.Protocol;

namespace Client.Task
{
    class HeartbeatTask : CommandTask
    {
        private const int HeartbeatTimeSpan = 5000;

        internal HeartbeatTask(ITaskDelegate taskDelegate) : base(taskDelegate) {
            Repeat = true;
            RepeatTimeSpan = HeartbeatTimeSpan;
            Request = new HeartbeatRequest();
        }
    }
}
