using Client.Task;
using Common.Protocol;
using System;
using System.IO;

namespace Client
{
    public partial class Client
    {
        public void ListFile(string remotePath, Action<Response, bool> taskCompletedCallback, string fileSet = "")
        {
            ListTask listTask = new ListTask(this, remotePath, fileSet);
            listTask.TaskCompletedCallback = taskCompletedCallback;
            listTask.Work();
        }

        public void CreateFolder(string fullPath, string fileSet, Action<Response, bool> taskCompletedCallback)
        {
            CreateFolderTask createFolderTask = new CreateFolderTask(this, fullPath, fileSet);
            createFolderTask.TaskCompletedCallback = taskCompletedCallback;
            createFolderTask.Work();
        }

        public void DownloadFile(
            string remoteFileFullPath,
            string localFileFullPath,
            string fileSet,
            Action taskStartedCallback,
            Action<Response, bool> taskCompletedCallback,
            Action<int> dataTransferredCallback)
        {
            GetDownloadInfoTask getDownloadInfoTask = new GetDownloadInfoTask(this, remoteFileFullPath, fileSet);
            getDownloadInfoTask.TaskCompletedCallback = (response, success) =>
            {
                if (!response.Success)
                {
                    taskCompletedCallback(null, false);
                }
                DownloadResponse dResponse = response as DownloadResponse;
                // TODO: handle fail

                DownloadFileTask downloadFileTask = new DownloadFileTask(this, dResponse.DownloadToken, new FileInfo(localFileFullPath), dResponse.FileLength);
                downloadFileTask.TaskCompletedCallback = taskCompletedCallback;
                downloadFileTask.TaskStartedCallback = taskStartedCallback;
                downloadFileTask.DataTransferredCallback = dataTransferredCallback;

                downloadFileTask.Work();
            };
            getDownloadInfoTask.Work();
        }

        public void UploadFile(
            string remoteFileFullPath,
            string localFileFullPath,
            string fileSet,
            Action taskStartedCallback,
            Action<Response, bool> taskCompletedCallback,
            Action<int> dataTransferredCallback)
        {
            FileInfo fileInfo = new FileInfo(localFileFullPath);
            GetUploadInfoTask getUploadInfoTask = new GetUploadInfoTask(this, remoteFileFullPath, fileSet, fileInfo.Length);
            getUploadInfoTask.TaskCompletedCallback = (response, success) =>
            {
                if (!response.Success)
                {
                    taskCompletedCallback(null, false);
                }
                UploadResponse uResponse = response as UploadResponse;

                UploadFileTask uploadFileTask = new UploadFileTask(this, uResponse.UploadToken, fileInfo, fileInfo.Length);
                uploadFileTask.TaskCompletedCallback = taskCompletedCallback;
                uploadFileTask.TaskStartedCallback = taskStartedCallback;
                uploadFileTask.DataTransferredCallback = dataTransferredCallback;

                uploadFileTask.Work();
            };
            getUploadInfoTask.Work();
        }
    }
}
