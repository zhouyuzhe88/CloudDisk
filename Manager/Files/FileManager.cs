using Common.Util;
using Common.Protocol;
using System;
using System.Collections.Generic;
using System.IO;

namespace Manager.Files
{
    class FileManager
    {
        private Dictionary<string, CloudFileInfo> Files { get; set; }

        internal FileManager()
        {
            Files = new Dictionary<string, CloudFileInfo>();
        }

        internal List<CloudFileInfo> ListFiles(string userName, string fileSet, string filePath)
        {
            string fullPath = BuildFilePath(userName, fileSet, filePath);
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
            if (!directoryInfo.Exists)
            {
                return null;
            }

            List<CloudFileInfo> result = new List<CloudFileInfo>();
            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                result.Add(new CloudFileInfo(dir.Name, 0, true));
            }
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                result.Add(new CloudFileInfo(file.Name, file.Length, false));
            }
            return result;
        }

        internal string AddUploadFile(string userName, string fileSet, string remoteFileFullPath, long fileLength)
        {
            string fullPath = BuildFilePath(userName, fileSet, remoteFileFullPath);
            new FileInfo(fullPath).Directory.Create();
            string fileId = Guid.NewGuid().ToString();
            CloudFileInfo fileInfo = new CloudFileInfo(fullPath, fileLength, false);
            lock (Files)
            {
                Files[fileId] = fileInfo;
            }
            return fileId;
        }

        internal string AddDownloadFile(string userName, string fileSet, string filePath)
        {
            string fullPath = BuildFilePath(userName, fileSet, filePath);
            FileInfo fileInfo = new FileInfo(fullPath);
            if(!fileInfo.Exists)
            {
                return null;
            }
            string fileId = Guid.NewGuid().ToString();
            CloudFileInfo cFileInfo = new CloudFileInfo(fullPath, fileInfo.Length, false);
            lock (Files)
            {
                Files[fileId] = cFileInfo;
            }
            return fileId;
        }

        internal CloudFileInfo GetFileInfo(string fileId)
        {
            if (Files.ContainsKey(fileId))
            {
                return Files[fileId];
            }
            return null;
        }

        internal void RemoveFile(string fileId)
        {
            lock (Files)
            {
                Files.Remove(fileId);
            }
        }

        private string BuildFilePath(string userName, string setName, string filePath)
        {
            List<string> pathComponents = new List<string>();
            pathComponents.Add(userName);
            if (string.IsNullOrWhiteSpace(setName))
            {
                pathComponents.AddRange(Settings.GetStringValue("UnclassifiedPath").GetPathComponents());
            }
            else
            {
                pathComponents.AddRange(Settings.GetStringValue("ClassifiedPath").GetPathComponents());
                pathComponents.AddRange(setName.GetPathComponents());
            }
            pathComponents.AddRange(filePath.GetPathComponents());
            return Settings.GetStringValue("RootPath") + pathComponents.GetPath();
        }
    }
}
