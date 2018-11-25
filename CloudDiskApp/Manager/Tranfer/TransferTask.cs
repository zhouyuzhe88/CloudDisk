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
            Finished
        }

        public string FileName { get; set; }

        public string RemotePath { get; set; }

        public string LocalPath { get; set; }

        public long FileSize { get; set; }

        public long TranffedSize { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime StartTime { get; set; }

        public string Speed { get; private set; }

        protected DateTime LastSyncedTime { get; set; }

        protected long LastSyncedSize { get; set; }

        public void Start()
        {
            StartTime = DateTime.Now;
            LastSyncedTime = DateTime.Now;
        }

        public void OnDataTransfferd(int size)
        {
            TranffedSize += size;
            DateTime now = DateTime.Now;
            double time = (now - LastSyncedTime).TotalMilliseconds;
            if (time > 1000)
            {
                Speed = ((long)((TranffedSize - LastSyncedSize) / time * 1000)).GetFileSize() + " / s";
                LastSyncedSize = TranffedSize;
                LastSyncedTime = now;
            }
        }

    }
}
