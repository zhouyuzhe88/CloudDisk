using Common.Protocol;

namespace Client.Task
{
    class ListTask : CommandTask
    {
        public ListTask(ITaskDelegate taskDelegate, string remotePath, string fileSet = "") : base(taskDelegate)
        {
            ListRequest request = new ListRequest();
            request.RemotePath = remotePath;
            request.FileSet = fileSet;
            Request = request;
        }
    }
}
