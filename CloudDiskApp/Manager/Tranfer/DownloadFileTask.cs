namespace CloudDiskApp
{
    class DownloadFileTask: TransferTask
    {
        public override void Start()
        {
            base.Start();
            ClientWrapper.Instance.DownloadFile(RemoteFileFullPath, LocalFileFullPath, FileSet, OnStart, OnCompleted, OnDataTransfferd);
        }
    }
}
