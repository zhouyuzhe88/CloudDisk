using System;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Util
{
    public static class StreamHelper
    {
        private static int bufferSize = Settings.GetIntValue("BufferSize");
        private const int intSize = sizeof(int);
        private const int longSize = sizeof(long);

        public static int ReadInt(this Stream stream)
        {
            byte[] buffer = new byte[intSize];
            stream.Read(buffer, 0, intSize);
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
        }

        public static void WriteInt(this Stream stream, int num)
        {
            byte[] buffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num));
            stream.Write(buffer, 0, intSize);
        }

        public static long ReadLong(this Stream stream)
        {
            byte[] buffer = new byte[longSize];
            stream.Read(buffer, 0, longSize);
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
        }

        public static void WriteLong(this Stream stream, long num)
        {
            byte[] buffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num));
            stream.Write(buffer, 0, longSize);
        }

        public static string ReadString(this Stream stream)
        {
            int length = stream.ReadInt();
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            return Encoding.Default.GetString(buffer, 0, length);
        }

        public static void WriteString(this Stream stream, string str)
        {
            byte[] buffer = Encoding.Default.GetBytes(str);
            stream.WriteInt(buffer.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public static void TransferDataTo(this Stream streamFrom, Stream streamTo, long length, Action<int> dataTransferredCallback = null)
        {
            byte[] buffer = new byte[bufferSize];
            int transferedOneTime = 0;
            for (long transfered = 0; transfered < length; transfered += transferedOneTime)
            {
                transferedOneTime = streamFrom.Read(buffer, 0, bufferSize);
                streamTo.Write(buffer, 0, transferedOneTime);
                dataTransferredCallback?.Invoke(transferedOneTime);
            }
        }
    }
}
