using Common.Util;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Client.Task
{
    abstract class DataTask : Task
    {
        internal Action<int> DataTransferredCallback { get; set; }

        protected string DataToken { get; set; }

        protected FileInfo FileInfo { get; set; }

        protected long FileLength { get; set; }

        protected abstract void TransferData(NetworkStream networkStream);

        public DataTask(ITaskDelegate taskDelegate, string dataToken, FileInfo fileInfo, long fileLength) : base(taskDelegate)
        {
            DataToken = dataToken;
            FileInfo = fileInfo;
            FileLength = fileLength;
        }

        internal override void Work()
        {
            new Thread(() =>
            {
                NetworkStream networkStream = null;
                try
                {
                    TaskStartedCallback?.Invoke();
                    FileInfo.Directory.Create();
                    TcpClient client = TaskDelegate.GetTcpClient(ClientType);
                    lock(client)
                    {
                        networkStream = client.GetStream();
                        networkStream.WriteString(DataToken);
                        TransferData(networkStream);
                    }
                    TaskCompletedCallback?.Invoke(null, true);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    TaskCompletedCallback?.Invoke(null, false);
                }
                finally
                {
                    if (networkStream != null)
                    {
                        networkStream.Close();
                    }
                }
            }).Start();
        }
    }
}
