using Common.Protocol;

namespace Manager.Handler
{
    class SigninHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            SigninRequest sRequest = request as SigninRequest;
            bool success = ActorGroup.Instance.UserManager.Signin(sRequest.UserName, sRequest.Password);
            SigninResponse response = sRequest.BuildResponse(success) as SigninResponse;
            if (success)
            {
                response.Token = ActorGroup.Instance.UserManager.GetToken(request.UserName);
            }
            return response;
        }
    }
}
