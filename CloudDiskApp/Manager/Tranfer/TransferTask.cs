using Common.Util;
using System;
using System.Linq;

namespace CloudDiskApp
{
    public abstract class TransferTask
    {
        public enum TaskStatus
        {
            Pending,
            Running,
            Completed
        }

        public string FileName
        {
            get
            {
                return RemoteFileFullPath.GetPathComponents().Last();
            }
        }

        public string RemoteFileFullPath { get; set; }

        public string LocalFileFullPath { get; set; }

        public string FileSet { get; set; }

        public long FileLength { get; set; }

        public long TranffedLength { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime StartTime { get; set; }

        public string Speed { get; private set; }

        protected DateTime LastSyncedTime { get; set; }

        protected long LastSyncedLength { get; set; }

        public virtual void Start()
        {
            StartTime = DateTime.Now;
            LastSyncedTime = DateTime.Now;
        }

        public void OnDataTransfferd(int size)
        {
            TranffedLength += size;
            DateTime now = DateTime.Now;
            double time = (now - LastSyncedTime).TotalMilliseconds;
            if (time > 1000)
            {
                Speed = ((long)((TranffedLength - LastSyncedLength) / time * 1000)).GetFileLengthString() + " / s";
                LastSyncedLength = TranffedLength;
                LastSyncedTime = now;
                Console.WriteLine(Speed);
            }
            UIController.Instance.RefreshTransferList();
        }

        protected void OnStart()
        {
            Console.WriteLine("running");
            Status = TaskStatus.Running;
        }

        protected void OnCompleted(bool success)
        {
            Console.WriteLine("Completed");
            Status = TaskStatus.Completed;
        }
    }
}
