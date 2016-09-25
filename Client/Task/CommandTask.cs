using Common.Protocol;
using Common.Util;
using System.Net.Sockets;
using System.Threading;

namespace Client.Task
{
    abstract class CommandTask: Task
    {
        protected override TCPClientType ClientType
        {
            get
            {
                return TCPClientType.command;
            }
        }

        protected CommandTask(ITaskDelegate taskDelegate) : base(taskDelegate) { }
        
        protected bool Repeat { get; set; }

        protected int RepeatTimeSpan { get; set; }

        protected Request Request { get; set; }

        internal override void Work()
        {
            new Thread(() =>
            {
                do
                {
                    try
                    {
                        TrySendCommand();
                    }
                    catch
                    {
                        TaskCompletedCallback.Invoke(null, false);
                    }

                    if (Repeat)
                    {
                        Thread.Sleep(RepeatTimeSpan);
                    }
                }
                while (Repeat);
            }).Start();
        }

        private void TrySendCommand()
        {
            TaskStartedCallback?.Invoke();
            
            Request.UserName = TaskDelegate.UserName;
            Request.Token = TaskDelegate.Token;
            Request.SetDateTime();

            string requestStr = Coder.EncodeRequest(Request);
            string responseStr;

            TcpClient client = TaskDelegate.GetTcpClient(ClientType);
            lock (client)
            {
                client.GetStream().WriteString(requestStr);
                responseStr = client.GetStream().ReadString();
            }
            Response response = Coder.DecodeResponse(responseStr);
            TaskCompletedCallback?.Invoke(response, response.Success);
        }
    }
}
