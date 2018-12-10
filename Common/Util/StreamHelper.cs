using Common.Logger;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Common.Util
{
    public static class StreamHelper
    {
        private static int bufferSize = Settings.GetIntValue("BufferSize");
        private const int intSize = sizeof(int);

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

        public static string ReadString(this Stream stream, CryptoType cryptoType = CryptoType.Decrypt)
        {
            int length = stream.ReadInt();
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            if (cryptoType == CryptoType.Decrypt)
            {
                buffer = buffer.DecryptDES();
            }
            return Encoding.Default.GetString(buffer, 0, buffer.Length);
        }

        public static void WriteString(this Stream stream, string str, CryptoType cryptoType = CryptoType.Encrypt)
        {
            byte[] buffer = Encoding.Default.GetBytes(str);
            if (cryptoType == CryptoType.Encrypt)
            {
                buffer = buffer.EncryptDES();
            }
            stream.WriteInt(buffer.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public static void TransferDataTo(this Stream streamFrom, Stream streamTo, long length, CryptoType cryptoType, Action<int> dataTransferredCallback = null)
        {
            byte[] buffer = new byte[bufferSize];
            int transferedOneTime = 0;
            Stream targetStream = cryptoType == CryptoType.None ? streamTo : new CryptoStream(streamTo, CryptoHelper.GetDESCryptoTransform(cryptoType), CryptoStreamMode.Write);
            try
            {
                while ((transferedOneTime = streamFrom.Read(buffer, 0, bufferSize)) != 0)
                {
                    targetStream.Write(buffer, 0, transferedOneTime);
                    dataTransferredCallback?.Invoke(transferedOneTime);
                }
                (targetStream as CryptoStream)?.FlushFinalBlock();
            }
            catch (Exception e)
            {
                Log.E(e);
            }
            finally
            {
                targetStream.Close();
                streamTo.Close();
                streamFrom.Close();
            }
        }
    }
}
