using Common.Logger;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Manager.Server
{
    public abstract class Server
    {
        protected string ServerName { get; set; }

        private int Port { get; set; }

        private Thread WorkingThread { get; set; }

        protected abstract void WorkingMethod(object socket);

        protected Server(int port)
        {
            Port = port;
            WorkingThread = new Thread(ListenToSocket);
        }

        public void Start()
        {
            WorkingThread.Start();
        }

        private void ListenToSocket()
        {
            Log.I("{0} started", ServerName);
            TcpListener listener = null;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                listener = new TcpListener(endPoint);
                listener.Start();
                while (true)
                {
                    TcpClient remoteClient;
                    try
                    {
                        remoteClient = listener.AcceptTcpClient();
                        Log.I("New client connect to {0}", ServerName);
                        Thread thread = new Thread(WorkingMethod);
                        thread.Start(remoteClient);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.E(e);
                return;
            }
            finally
            {
                listener.Stop();
                Log.I("{0} ended", ServerName);
            }
        }
    }
}
