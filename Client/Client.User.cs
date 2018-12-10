using Client.Task;
using Common.Logger;
using Common.Protocol;
using System;

namespace Client
{
    public partial class Client
    {
        public void Signin(string userName, string password, Action<Response, bool> taskCompletedCallback)
        {
            UserName = userName;
            SignInTask signInTask = new SignInTask(this, password);
            signInTask.TaskCompletedCallback = (response, success) =>
            {
                OnSignInCompleted(response, success);
                taskCompletedCallback?.Invoke(response, success);
            };
            signInTask.Work();
        }

        private void OnSignInCompleted(Response response, bool success)
        {
            if (success)
            {
                SigninResponse sResponse = response as SigninResponse;
                Token = sResponse.Token;
                Log.I("Sign in success");
                SendHeartbeat();
            }
            else
            {
                Log.E("Sign in fail");
            }
        }

        private void SendHeartbeat()
        {
            HeartbeatTask heartbeatTask = new HeartbeatTask(this);
            heartbeatTask.TaskCompletedCallback = OnHartbeatCompeled;
            heartbeatTask.Work();
        }

        private void OnHartbeatCompeled(Response response, bool success)
        {
            if (success)
            {
                Log.V("Heartbeat");
                HeartbeatResponse hResponse = response as HeartbeatResponse;
                if (hResponse.Token != Token)
                {
                    Token = hResponse.Token;
                    Log.I("Update token {0}", Token);
                }
            }
        }
    }
}
