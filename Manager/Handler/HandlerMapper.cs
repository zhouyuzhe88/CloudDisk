using Common.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Handler
{
    static class HandlerMapper
    {
        private static Dictionary<Type, Type> RequsetToHandler { get; set; }

        static HandlerMapper()
        {
            RequsetToHandler = new Dictionary<Type, Type>()
            {
                { typeof(EchoRequest),              typeof(EchoHandler)},
                { typeof(SigninRequest),            typeof(SigninHandler)},
                { typeof(HeartbeatRequest),         typeof(HeartbeatHandler)},
                { typeof(UploadRequest),            typeof(UploadHandler)},
                { typeof(DownloadRequest),          typeof(DownloadHandler)},
                { typeof(ListRequest),              typeof(ListHandler)},
                { typeof(CreateFolderRequest),      typeof(CreateFolderHandler)}
            };
        }

        internal static Handler BuildHandler(this Request request)
        {
            if (RequsetToHandler.Keys.Contains(request.GetType()))
            {
                Type handlerType = RequsetToHandler[request.GetType()];
                return Activator.CreateInstance(handlerType) as Handler;
            }
            return null;
        }
    }
}
