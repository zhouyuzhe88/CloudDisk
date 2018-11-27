using Common.Protocol;

namespace Client.Task
{
    class GetDownloadInfoTask : CommandTask
    {
        public GetDownloadInfoTask(ITaskDelegate taskDelegate, string remoteFullPath, string fileSet = "") : base(taskDelegate)
        {
            DownloadRequest request = new DownloadRequest();
            request.RemoteFileFullPath = remoteFullPath;
            request.FileSet = fileSet;
            Request = request;
        }
    }
}
