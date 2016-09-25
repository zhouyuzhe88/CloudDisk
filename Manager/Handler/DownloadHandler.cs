using Common.Protocol;
using Manager.Files;

namespace Manager.Handler
{
    class DownloadHandler : Handler
    {
        internal override Response Handle(Request request)
        {
            if (!Check(request))
            {
                return request.BuildResponse(false);
            }
            DownloadRequest dRequest = request as DownloadRequest;
            if (string.IsNullOrWhiteSpace(dRequest.FileName))
            {
                return request.BuildResponse(false);
            }
            string fileId = ActorGroup.Instance.FileManager.AddDownloadFile(request.UserName,
                dRequest.FileSet, dRequest.RemotePath, dRequest.FileName);
            if (string.IsNullOrWhiteSpace(fileId))
            {
                return request.BuildResponse(false);
            }
            DownloadResponse dResponse = request.BuildResponse() as DownloadResponse;
            CloudFileInfo fileInfo = ActorGroup.Instance.FileManager.GetFileInfo(fileId);
            dResponse.DownloadToken = fileId;
            dResponse.FileLength = fileInfo.FileLength;
            return dResponse;
        }
    }
}
