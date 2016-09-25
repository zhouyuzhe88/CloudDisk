using Common.Protocol;

namespace Manager.Handler
{
    class ListHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            if (!Check(request))
            {
                return request.BuildResponse(false);
            }
            ListRequest lRequest = request as ListRequest;
            if (string.IsNullOrWhiteSpace(lRequest.RemotePath))
            {
                return request.BuildResponse(false);
            }

            ListResponse lResponse = request.BuildResponse() as ListResponse;
            lResponse.Files = ActorGroup.Instance.FileManager.ListFiles(request.UserName, lRequest.FileSet, lRequest.RemotePath);
            lResponse.Success = lResponse.Files != null;
            return lResponse;
        }
    }
}
