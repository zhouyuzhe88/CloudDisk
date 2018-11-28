using Common.Protocol;

namespace Client.Task
{
    class GetUploadInfoTask : CommandTask
    {
        public GetUploadInfoTask(ITaskDelegate taskDelegate, string remoteFileFullPath, string fileSet, long fileLength) : base(taskDelegate)
        {
            UploadRequest request = new UploadRequest();
            request.RemoteFileFullPath = remoteFileFullPath;
            request.FileLength = fileLength;
            request.FileSet = fileSet;
            Request = request;
        }
    }
}