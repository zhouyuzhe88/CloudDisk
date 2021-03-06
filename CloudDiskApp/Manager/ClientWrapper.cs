﻿using Common.Protocol;
using Common.Util;
using System;
using System.Collections.Generic;

namespace CloudDiskApp
{
    class ClientWrapper
    {
        private static ClientWrapper instance = new ClientWrapper();

        private Client.Client Client { get; set; }

        private ClientWrapper()
        {
            Client = new Client.Client();
        }

        internal static ClientWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        internal void SignIn(Action<bool> completedCallback)
        {
            string user = Settings.GetStringValue("currentUser");
            string password = Settings.GetStringValue(string.Format("{0}.{1}", user, "password"));
            Client.Signin(user, password, (response, success) =>
            {
                completedCallback?.Invoke(success);
            });
        }

        internal void ListFiles(string remotepath, Action<List<CloudFileInfo>, bool> completedCallback, string fileSet = null)
        {
            if (fileSet == null)
            {
                fileSet = Context.Instance.CurrentFileSet;
            }

            Client.ListFile(remotepath, (response, success) =>
            {
                completedCallback?.Invoke((response as ListResponse)?.Files, success);
            }, fileSet);
        }

        internal void CreateFolder(string fullPath, Action<bool> completedCallback, string fileSet = null)
        {
            if (fileSet == null)
            {
                fileSet = Context.Instance.CurrentFileSet;
            }

            Client.CreateFolder(fullPath, fileSet, (response, success) =>
            {
                completedCallback?.Invoke(success);
            });
        }

        internal void DownloadFile(
            string remoteFileFullPath,
            string localFileFullPath,
            string fileSet,
            Action taskStartedCallback,
            Action<bool> taskCompletedCallback,
            Action<int> dataTransferredCallback)
        {
            Client.DownloadFile(remoteFileFullPath, localFileFullPath, fileSet,
                taskStartedCallback,
                (response, success) =>
                {
                    taskCompletedCallback(success);
                }, dataTransferredCallback);
        }

        internal void UploadFile(
            string remoteFileFullPath,
            string localFileFullPath,
            string fileSet,
            Action taskStartedCallback,
            Action<bool> taskCompletedCallback,
            Action<int> dataTransferredCallback)
        {
            Client.UploadFile(remoteFileFullPath, localFileFullPath, fileSet,
                taskStartedCallback,
                (response, success) =>
                {
                    taskCompletedCallback(success);
                }, dataTransferredCallback);
        }
    }
}
