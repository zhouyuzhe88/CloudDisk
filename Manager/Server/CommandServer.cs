using Common.Protocol;
using Common.Util;
using Manager.Handler;
using System;
using System.Net.Sockets;

namespace Manager.Server
{
    public class CommandServer : Server
    {
        public CommandServer(int port)
            : base(port)
        {
            ServerName = "CommandServer";
        }

        protected override void WorkingMethod(object client)
        {
            HandleCommand((TcpClient)client);
        }

        private void HandleCommand(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                try
                {
                    string data = stream.ReadString();
                    Request request = Coder.DecodeRequest(data);

                    Response response = request.BuildHandler().Handle(request);

                    if (response != null)
                    {
                        string ret = Coder.EncodeResponse(response);
                        stream.WriteString(ret);
                    }
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
}
