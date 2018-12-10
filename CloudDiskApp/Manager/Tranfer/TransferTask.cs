using Common.Logger;
using Common.Util;
using System;

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
                return RemoteFileFullPath.GetLastComponent();
            }
        }

        public string DirectoryPath
        {
            get
            {
                return RemoteFileFullPath.GetDirectoryPath().GetLastComponent();
            }
        }

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

        public virtual void OnDataTransfferd(int size)
        {
            TranffedLength += size;
            DateTime now = DateTime.Now;
            double time = (now - LastSyncedTime).TotalMilliseconds;
            if (time > 1000)
            {
                Speed = ((long)((TranffedLength - LastSyncedLength) / time * 1000)).GetFileLengthString() + " / s";
                LastSyncedLength = TranffedLength;
                LastSyncedTime = now;
                Log.V(Speed);
                UIController.Instance.RefreshTransferList();
            }
        }

        protected virtual void OnStart()
        {
            Log.I("TransferTask Running");
            TranffedLength = 0;
            Status = TaskStatus.Running;
            UIController.Instance.RefreshTransferList();
        }

        protected virtual void OnCompleted(bool success)
        {
            Log.I("TransferTask Completed");
            Status = TaskStatus.Completed;
            UIController.Instance.RefreshTransferList();
        }
    }
}
