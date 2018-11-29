using Common.Protocol;

namespace Client.Task
{
    class CreateFolderTask : CommandTask
    {
        public CreateFolderTask(ITaskDelegate taskDelegate, string fullPath, string fileSet = "") : base(taskDelegate)
        {
            CreateFolderRequest request = new CreateFolderRequest();
            request.FullPath = fullPath;
            request.FileSet = fileSet;
            Request = request;
        }
    }
}
