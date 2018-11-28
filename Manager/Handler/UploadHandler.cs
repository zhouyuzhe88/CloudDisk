using Common.Protocol;

namespace Manager.Handler
{
    class UploadHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            if (!Check(request))
            {
                return request.BuildResponse(false);
            }
            UploadRequest uRequest = request as UploadRequest;
            if (string.IsNullOrWhiteSpace(uRequest.RemoteFileFullPath) || uRequest.FileLength <= 0)
            {
                return request.BuildResponse(false);
            }
            string fileId = ActorGroup.Instance.FileManager.AddUploadFile(request.UserName,
                uRequest.FileSet, uRequest.RemoteFileFullPath, uRequest.FileLength);
            UploadResponse uResponse = request.BuildResponse() as UploadResponse;
            uResponse.UploadToken = fileId;
            return uResponse;
        }
    }
}
