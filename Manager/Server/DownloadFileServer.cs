using Common.Protocol;
using Common.Util;
using System;
using System.IO;
using System.Net.Sockets;

namespace Manager.Server
{
    public class DownloadFileServer : Server
    {
        public DownloadFileServer(int port)
            : base(port)
        {
            ServerName = "DownloadFileServer";
        }

        protected override void WorkingMethod(object client)
        {
            SendFile((TcpClient)client);
        }

        private void SendFile(TcpClient client)
        {
            NetworkStream nStream = client.GetStream();
            try
            {
                string fileId = nStream.ReadString();
                CloudFileInfo fileInfo = ActorGroup.Instance.FileManager.GetFileInfo(fileId);
                long fileLen = fileInfo.FileLength;
                using (FileStream fStream = File.OpenRead(fileInfo.FilePath))
                {
                    fStream.TransferDataTo(nStream, fileLen);
                }
                ActorGroup.Instance.FileManager.RemoveFile(fileId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return;
            }
        }
    }
}
