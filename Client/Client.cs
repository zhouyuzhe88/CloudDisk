using Client.Task;
using Common.Util;
using System;
using System.Net.Sockets;

namespace Client
{
    public partial class Client: ITaskDelegate
    {
        public string UserName { get; private set; }

        public string Token { get; private set; }

        private string ServerIp { get; set; }

        private int CommandPort { get; set; }

        private int DownloadPort { get; set; }

        private int UploadPort { get; set; }

        private TcpClient CommandClient { get; set; }
        
        public Client()
        {
            ServerIp = Settings.GetStringValue("ServerIP");
            CommandPort = Settings.GetIntValue("CommandServerPort");
            DownloadPort = Settings.GetIntValue("DownloadServerPort");
            UploadPort = Settings.GetIntValue("UploadServerPort");

            CommandClient = new TcpClient();
            try
            {
                CommandClient.Connect(ServerIp, CommandPort);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public TcpClient GetTcpClient(TCPClientType type)
        {
            switch(type)
            {
                case TCPClientType.command:
                    return CommandClient;
                case TCPClientType.download:
                    TcpClient downloadClient = new TcpClient();
                    downloadClient.Connect(Settings.GetStringValue("ServerIP"), Settings.GetIntValue("DownloadServerPort"));
                    return downloadClient;
                case TCPClientType.upload:
                    TcpClient uploadClient = new TcpClient();
                    uploadClient.Connect(Settings.GetStringValue("ServerIP"), Settings.GetIntValue("UploadServerPort"));
                    return uploadClient;
            }
            return null;
        }
    }
}
