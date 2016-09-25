using Common.Protocol;

namespace Manager.Handler
{
    class HeartbeatHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            if (!Check(request))
            {
                return request.BuildResponse(false);
            }
            HeartbeatRequest hRequest = request as HeartbeatRequest;
            HeartbeatResponse hResponse = request.BuildResponse() as HeartbeatResponse;
            hResponse.Token = ActorGroup.Instance.UserManager.GetToken(request.UserName);
            return hResponse;
        }
    }
}
