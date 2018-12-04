using Common.Util;
using System.IO;
using System.Net.Sockets;

namespace Client.Task
{
    class UploadFileTask : DataTask
    {
        public UploadFileTask(ITaskDelegate taskDelegate, string dataToken, FileInfo fileInfo, long fileLength)
            : base(taskDelegate, dataToken, fileInfo, fileLength) { }

        protected override TCPClientType ClientType
        {
            get
            {
                return TCPClientType.upload;
            }
        }

        protected override void TransferData(NetworkStream networkStream)
        {
            using (FileStream fileStream = FileInfo.OpenRead())
            {
                fileStream.TransferDataTo(networkStream, FileLength, CryptoType.Encrypt, DataTransferredCallback);
            }
        }
    }
}
