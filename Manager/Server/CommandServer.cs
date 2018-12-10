using Common.Logger;
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

                    Log.I("Receive: {0}", request);
                    Response response = request.BuildHandler().Handle(request);
                    Log.I("Send: {0}", response);
                    if (response != null)
                    {
                        string ret = Coder.EncodeResponse(response);
                        stream.WriteString(ret);
                    }
                }
                catch (Exception e)
                {
                    Log.E(e);
                    return;
                }
            }
        }
    }
}
