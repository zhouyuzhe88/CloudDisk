using System.Net.Sockets;

namespace Client.Task
{
    interface ITaskDelegate
    {
        string UserName { get; }

        string Token { get; }

        TcpClient GetTcpClient(TCPClientType type);
    }

    public enum TCPClientType
    {
        command, upload, download
    }
}
