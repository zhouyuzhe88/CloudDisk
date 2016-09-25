using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    public static class HashHelper
    {
        public static string MD5(this string str)
        {
            if(str == null)
            {
                return null;
            }
            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
