using System;

namespace Common.Util
{
    public static class Settings
    {
        public static string GetStringValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static int GetIntValue(string key)
        {
            return Convert.ToInt32(GetStringValue(key));
        }
    }
}
