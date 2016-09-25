using Common.Protocol;

namespace Client.Task
{
    class GetDownloadInfoTask : CommandTask
    {
        public GetDownloadInfoTask(ITaskDelegate taskDelegate, string fileName, string remotePath, string fileSet = "") : base(taskDelegate)
        {
            DownloadRequest request = new DownloadRequest();
            request.FileName = fileName;
            request.RemotePath = remotePath;
            request.FileSet = fileSet;
            Request = request;
        }
    }
}
