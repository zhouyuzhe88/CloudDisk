using System;
using System.Configuration;

namespace Common.Util
{
    public static class Settings
    {
        public static string GetStringValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static int GetIntValue(string key)
        {
            return Convert.ToInt32(GetStringValue(key));
        }

        public static void Set(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);;
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
