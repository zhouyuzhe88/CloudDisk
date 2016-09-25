﻿using Client.Task;
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

        public void DownloadFile(
            string fileName,
            string remotePath,
            string localName,
            Action taskStartedCallback,
            Action<Response, bool> taskCompletedCallback,
            Action<int> dataTransferredCallback,
            string fileSet = "")
        {
            GetDownloadInfoTask getDownloadInfoTask = new GetDownloadInfoTask(this, fileName, remotePath, fileSet);
            getDownloadInfoTask.TaskCompletedCallback = (response, success) =>
            {
                if (!response.Success)
                {
                    taskCompletedCallback(null, false);
                }
                DownloadResponse dResponse = response as DownloadResponse;

                DownloadFileTask downloadFileTask = new DownloadFileTask(this, dResponse.DownloadToken, new FileInfo(localName), dResponse.FileLength);
                downloadFileTask.TaskCompletedCallback = taskCompletedCallback;
                downloadFileTask.TaskStartedCallback = taskStartedCallback;
                downloadFileTask.DataTransferredCallback = dataTransferredCallback;

                downloadFileTask.Work();
            };
            getDownloadInfoTask.Work();
        }

        public void UploadFile(
            string fileName,
            string remotePath,
            Action taskStartedCallback,
            Action<Response, bool> taskCompletedCallback,
            Action<int> dataTransferredCallback,
            string fileSet = "")
        {
            FileInfo fileInfo = new FileInfo(fileName);
            GetUploadInfoTask getUploadInfoTask = new GetUploadInfoTask(this, fileInfo.Name, remotePath, fileInfo.Length, fileSet);
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