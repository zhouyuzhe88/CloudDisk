using Common.Protocol;
using Common.Util;
using System;
using System.IO;
using System.Net.Sockets;

namespace Manager.Server
{
    public class UploadFileServer : Server
    {
        public UploadFileServer(int port)
            : base(port)
        {
            ServerName = "UploadFileServer";
        }

        protected override void WorkingMethod(object client)
        {
            RecieveFile((TcpClient)client);
        }

        private void RecieveFile(TcpClient client)
        {
            NetworkStream nStream = client.GetStream();
            try
            {
                string fileId = nStream.ReadString();
                CloudFileInfo fileInfo = ActorGroup.Instance.FileManager.GetFileInfo(fileId);
                long fileLen = fileInfo.FileLength;
                using (FileStream fStream = File.OpenWrite(fileInfo.FilePath))
                {
                    nStream.TransferDataTo(fStream, fileLen, CryptoType.Decrypt);
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
