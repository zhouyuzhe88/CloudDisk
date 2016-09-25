using Common.Protocol;

namespace Manager.Handler
{
    class EchoHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            EchoRequest eRequest = request as EchoRequest;
            EchoResponse response = request.BuildResponse() as EchoResponse;
            response.Content = eRequest.Content;
            return response;
        }
    }
}
