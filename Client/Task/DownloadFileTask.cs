using Common.Util;
using System.IO;
using System.Net.Sockets;

namespace Client.Task
{
    class DownloadFileTask : DataTask
    {
        public DownloadFileTask(ITaskDelegate taskDelegate, string dataToken, FileInfo fileInfo, long fileLength)
            : base(taskDelegate, dataToken, fileInfo, fileLength) { }

        protected override TCPClientType ClientType
        {
            get
            {
                return TCPClientType.download;
            }
        }

        protected override void TransferData(NetworkStream networkStream)
        {
            using (FileStream fileStream = File.OpenWrite(FileInfo.FullName))
            {
                networkStream.TransferDataTo(fileStream, FileLength, DataTransferredCallback);
            }
        }
    }
}
