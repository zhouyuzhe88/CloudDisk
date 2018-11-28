namespace CloudDiskApp
{
    class UploadFileTask : TransferTask
    {
        public override void Start()
        {
            base.Start();
            ClientWrapper.Instance.UploadFile(RemoteFileFullPath, LocalFileFullPath, FileSet, OnStart, OnCompleted, OnDataTransfferd);
        }

        protected override void OnCompleted(bool success)
        {
            base.OnCompleted(success);
            UIController.Instance.ListFiles(Context.Instance.CurrentPath);
        }
    }
}
