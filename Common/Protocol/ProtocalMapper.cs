using System;
using System.Collections.Generic;

namespace Common.Protocol
{
    public static class ProtocalMapper
    {
        private static Dictionary<Type, Type> RequsetToResponse { get; set; }
        private static Dictionary<string, Type> NameToResponse { get; set; }
        private static Dictionary<string, Type> NameToRequest { get; set; }

        public static Type GetResponseType(Type requestType)
        {
            if (RequsetToResponse.ContainsKey(requestType))
            {
                return RequsetToResponse[requestType];
            }
            return null;
        }

        public static Type GetResponseType(this Request request)
        {
            return GetResponseType(request.GetType());
        }

        public static Response BuildResponse(this Request request, bool success = true)
        {
            Type responseType = request.GetResponseType();
            if(responseType == null)
            {
                return null;
            }
            Response response = Activator.CreateInstance(responseType) as Response;
            response.RequestId = request.RequestId;
            response.SetDateTime();
            response.Success = success;
            return response;
        }

        public static Type GetResponseType(string responseName)
        {
            if (NameToResponse.ContainsKey(responseName))
            {
                return NameToResponse[responseName];
            }
            return null;
        }

        public static Type GetRequestType(string requestName)
        {
            if (NameToRequest.ContainsKey(requestName))
            {
                return NameToRequest[requestName];
            }
            return null;
        }

        static ProtocalMapper()
        {
            RequsetToResponse = new Dictionary<Type, Type>()
            {
                { typeof(Request),                  typeof(Response)},
                { typeof(EchoRequest),              typeof(EchoResponse)},
                { typeof(SigninRequest),            typeof(SigninResponse)},
                { typeof(HeartbeatRequest),         typeof(HeartbeatResponse)},
                { typeof(UploadRequest),            typeof(UploadResponse)},
                { typeof(DownloadRequest),          typeof(DownloadResponse)},
                { typeof(ListRequest),              typeof(ListResponse)},
                { typeof(CreateFolderRequest),      typeof(CreateFolderResponse)},
            };

            NameToRequest = new Dictionary<string, Type>()
            {
                { new EchoRequest().Name,               typeof(EchoRequest) },
                { new SigninRequest().Name,             typeof(SigninRequest) },
                { new HeartbeatRequest().Name,          typeof(HeartbeatRequest) },
                { new UploadRequest().Name,             typeof(UploadRequest) },
                { new DownloadRequest().Name,           typeof(DownloadRequest) },
                { new ListRequest().Name,               typeof(ListRequest) },
                { new CreateFolderRequest().Name,       typeof(CreateFolderRequest) },
            };

            NameToResponse = new Dictionary<string, Type>()
            {
                { new EchoResponse().Name,              typeof(EchoResponse) },
                { new SigninResponse().Name,            typeof(SigninResponse) },
                { new HeartbeatResponse().Name,         typeof(HeartbeatResponse) },
                { new UploadResponse().Name,            typeof(UploadResponse) },
                { new DownloadResponse().Name,          typeof(DownloadResponse) },
                { new ListResponse().Name,              typeof(ListResponse) },
                { new CreateFolderResponse().Name,      typeof(CreateFolderResponse) },
            };
        }
    }
}
