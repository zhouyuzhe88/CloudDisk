using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Common.Util
{
    public static class JsonHelper
    {
        public static T DeserializeFromJson<T>(this string str) where T : class
        {
            T result = null;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(str))))
            {
                try
                {
                    result = ser.ReadObject(ms) as T;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        public static object DeserializeFromJson(this string str, Type t)
        {
            object result = null;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(t);
            using (var ms = new MemoryStream(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(str))))
            {
                try
                {
                    result = ser.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        public static string SerializeToJson(this object obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
            string result = string.Empty;
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, obj);
                ms.Position = 0;
                StreamReader sReader = new StreamReader(ms);
                result = sReader.ReadToEnd();
            }
            return result;
        }

    }
}
