using Common.Protocol;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CloudDiskApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private Client.Client Client { get; set; }

        public App()
        {
            Client = new Client.Client();
        }

        internal void SignIn(Action<bool> completedCallback)
        {
            string user = Settings.GetStringValue("currentUser");
            string password = Settings.GetStringValue(string.Format("{0}.{1}", user, "password"));
            Client.Signin(user, password, (response, success) =>
            {
                completedCallback?.Invoke(success);
            });
        }

        internal void ListFiles(string remotepath, Action<List<CloudFileInfo>, bool> completedCallback, string fileSet = "")
        {
            Client.ListFile(remotepath, (response, success) =>
            {
                completedCallback?.Invoke((response as ListResponse)?.Files, success);
            }, fileSet);
        }
    }
}
