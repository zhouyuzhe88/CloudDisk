using Common.Protocol;

namespace Manager.Handler
{
    abstract class Handler
    {
        abstract internal Response Handle(Request request);

        internal bool Check(Request request)
        {
            return ActorGroup.Instance.UserManager.Check(request.UserName, request.Token);
        }
    }
}
