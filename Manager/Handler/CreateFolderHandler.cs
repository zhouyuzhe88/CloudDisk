using Common.Protocol;

namespace Manager.Handler
{
    class CreateFolderHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            if (!Check(request))
            {
                return request.BuildResponse(false);
            }
            CreateFolderRequest cRequest = request as CreateFolderRequest;
            if (string.IsNullOrWhiteSpace(cRequest.FullPath))
            {
                return request.BuildResponse(false);
            }

            CreateFolderResponse cResponse = request.BuildResponse() as CreateFolderResponse;
            cResponse.Success = ActorGroup.Instance.FileManager.CreateFolder(request.UserName, cRequest.FileSet, cRequest.FullPath);
            return cResponse;
        }
    }
}
