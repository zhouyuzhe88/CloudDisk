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

        public abstract string FileName { get; }

        public abstract string DirectoryPath { get; }

        public string RemoteFileFullPath { get; set; }

        public string LocalFileFullPath { get; set; }

        public string FileSet { get; set; }

        public long FileLength { get; set; }

        public string FileLengthString { get { return FileLength.GetFileLengthString(); } }

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
                UIController.Instance.RefreshTransferList();
            }
        }

        protected void OnStart()
        {
            Console.WriteLine("running");
            TranffedLength = 0;
            Status = TaskStatus.Running;
            UIController.Instance.RefreshTransferList();
        }

        protected void OnCompleted(bool success)
        {
            Console.WriteLine("Completed");
            Status = TaskStatus.Completed;
            UIController.Instance.RefreshTransferList();
        }
    }
}
