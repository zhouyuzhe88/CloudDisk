using Common.Protocol;

namespace Client.Task
{
    class GetUploadInfoTask : CommandTask
    {
        public GetUploadInfoTask(ITaskDelegate taskDelegate, string fileName, string filePath, long fileLength, string fileSet = "") : base(taskDelegate)
        {
            UploadRequest request = new UploadRequest();
            request.FileName = fileName;
            request.FilePath = filePath;
            request.FileLength = fileLength;
            request.FileSet = fileSet;
            Request = request;
        }
    }
}
