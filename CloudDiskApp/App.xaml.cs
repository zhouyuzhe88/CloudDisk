using Common.Util;
using System.Windows;

namespace CloudDiskApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public Client.Client Client { get; set; }

        public App()
        {
            Client = new Client.Client();
        }

        private void SignIn()
        {
            string user = Settings.GetStringValue("currentUser");
            string password = Settings.GetStringValue(string.Format("{0}.{1}", user, "password"));
            Client.Signin(user, password, null);
        }
    }
}
