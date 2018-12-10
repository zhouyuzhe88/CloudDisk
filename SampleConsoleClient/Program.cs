using System;
using Common.Protocol;
using Common.Util;
using Common.Logger;

namespace SampleConsoleClient
{
    class Program
    {
        static Client.Client client { get; set; }

        static void Main(string[] args)
        {
            client = new Client.Client();
            client.Signin("zyz", Settings.GetStringValue("zyz.password"), OnSignInCompleted);
        }

        private static void OnSignInCompleted(Response response, bool success)
        {
            if (!success)
            {
                return;
            }

            client.ListFile("\\", OnListCompleted);
            // UploadFile("D:\\x.mkv", "\\");
            // DownloadFile("x.mkv", "\\", "D:\\x.mkv");
        }

        private static void UploadFile(string remoteFileFullPath, string localFileFullPath)
        {
            DateTime startTime = DateTime.Now, lastTime = DateTime.Now;
            int byteCnt = 0;
            long fileLength = 0;
            double timeCnt = 0;
            client.UploadFile(remoteFileFullPath, localFileFullPath, "", () =>
            {
                Log.I("start upload {0}", remoteFileFullPath);
            },
            (response, success) =>
            {
                OnDataTransCompleted(startTime, fileLength);
            },
            (length) =>
            {
                OnDataTrans(length, ref lastTime, ref byteCnt, ref fileLength, ref timeCnt);
            });
        }

        private static void DownloadFile(string remoteFileFullPath, string localFileFullPath)
        {
            DateTime startTime = DateTime.Now, lastTime = DateTime.Now;
            int byteCnt = 0;
            long fileLength = 0;
            double timeCnt = 0;
            client.DownloadFile(remoteFileFullPath, localFileFullPath, "", () =>
            {
                Log.I("start download {0}", localFileFullPath);
            },
            (r, s) =>
            {
                OnDataTransCompleted(startTime, fileLength);
            },
            (length) =>
            {
                OnDataTrans(length, ref lastTime, ref byteCnt, ref fileLength, ref timeCnt);
            });
        }

        private static void OnListCompleted(Response response, bool success)
        {
            if (!success)
            {
                return;
            }

            ListResponse lRespons = response as ListResponse;
            foreach (CloudFileInfo info in lRespons.Files)
            {
                Log.I("{0} {1} {2}", info.FilePath, info.FileLength, info.IsDirectory);
            }
        }

        private static void OnDataTransCompleted(DateTime startTime, long fileLength)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan total = endTime - startTime;
            Log.I("Finish, use {0} s, average speed = {1}MB/s", total.TotalMilliseconds / 1000.0, GetSpeed(fileLength, total.TotalMilliseconds));
        }

        private static void OnDataTrans(int dataLength, ref DateTime lastTime, ref int byteCnt, ref long fileLength, ref double timeCnt)
        {
            DateTime tmp = DateTime.Now;
            TimeSpan dt = tmp - lastTime;
            lastTime = tmp;
            byteCnt += dataLength;
            fileLength += dataLength;
            timeCnt += dt.TotalMilliseconds;
            if (timeCnt > 1000)
            {
                Log.I("{0} KB transfered speed = {1} MB/s", byteCnt / 1000.0, GetSpeed(byteCnt, timeCnt));
                byteCnt = 0;
                timeCnt = 0;
            }
        }

        // Get speed, MB/ S
        private static double GetSpeed(long byteCnt, double ms)
        {
            return byteCnt / ms * 1000 / 1024.0 / 1024.0;
        }
    }
}
