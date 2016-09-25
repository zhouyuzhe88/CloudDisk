using Common.Protocol;

namespace Client.Task
{
    class SignInTask : CommandTask
    {
        internal SignInTask(ITaskDelegate taskDelegate, string password) : base(taskDelegate)
        {
            SigninRequest request = new SigninRequest();
            request.Password = password;
            Request = request;
        }
    }
}
