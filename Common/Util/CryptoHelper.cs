using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Common.Util
{
    public enum CryptoType
    {
        None,
        Encrypt,
        Decrypt
    }

    public static class CryptoHelper
    {
        private static byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };

        private static DESCryptoServiceProvider DES()
        {
            byte[] sKey = Encoding.ASCII.GetBytes("12345678");
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = sKey;
            des.IV = sKey;
            return des;
        }

        private static string ToHexString(this byte[] input)
        {
            if (input.Length < 10)
            {
                return BitConverter.ToString(input, 0, input.Length).Replace("-", "");
            }
            else
            {
                string prefix = BitConverter.ToString(input, 0, 10).Replace("-", "");
                string suffix = BitConverter.ToString(input, input.Length - 10, 10).Replace("-", "");
                return string.Format("{0} - {1}", prefix, suffix);
            }
        }

        public static byte[] EncryptDES(this byte[] input)
        {
            // Console.WriteLine("[{0}] Encrypt start: {1}", input.Length, input.ToHexString());
            byte[] result = input.CryptoData(input.Length, CryptoType.Encrypt);
            // Console.WriteLine("[{0}] Encrypt end:   {1}", result.Length, result.ToHexString());
            return result;
        }

        public static byte[] DecryptDES(this byte[] input)
        {
            // Console.WriteLine("[{0}] Decrypt start: {1}", input.Length, input.ToHexString());
            byte[] result = input.CryptoData(input.Length, CryptoType.Decrypt);
            // Console.WriteLine("[{0}] Decrypt end:   {1}", result.Length, result.ToHexString());
            return result;
        }
        
        public static ICryptoTransform GetDESCryptoTransform(CryptoType type)
        {
            switch(type)
            {
                case CryptoType.Decrypt:
                    return DES().CreateDecryptor();
                case CryptoType.Encrypt:
                    return DES().CreateEncryptor();
                default:
                    return null;
            }
        }

        private static byte[] CryptoData(this byte[] input, int length, CryptoType type)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetDESCryptoTransform(type), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(input, 0, length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                }
                return memoryStream.ToArray();
            }
        }
    }
}
