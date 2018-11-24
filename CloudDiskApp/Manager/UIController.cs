using Common.Protocol;
using System.Linq;
using System;
using System.Windows;
using Common.Util;

namespace CloudDiskApp
{
    class UIController: IMainWindowManager, IFileRowManager, IToolBarViewManager
    {
        private UIController() { }

        private static UIController instance = new UIController();

        internal static UIController Instance
        {
            get
            {
                return instance;
            }
        }

        private App App
        {
            get
            {
                return (App)Application.Current;
            }
        }

        internal MainWindow MainWindow
        {
            get
            {
                return App.MainWindow as MainWindow;
            }
        }

        private void DiapatchMain(Action action)
        {
            App.Dispatcher.Invoke(action);
        }

        internal void ListFiles(string path)
        {
            ClientWrapper.Instance.ListFiles(path, (fileList, success) =>
            {
                if (fileList != null && success)
                {
                    Context.Instance.CurrentPath = path;
                    DiapatchMain(() =>
                    {
                        MainWindow.SetFiles(fileList);
                        MainWindow.SetPath(path);
                    });
                }
            });
        }

        #region MainWindow

        public void OnMainWindowLoaded()
        {
            ClientWrapper.Instance.SignIn((signInSuccess) =>
            {
                if (signInSuccess)
                {
                    ListFiles(FileHelper.FileRoot);
                }
            });
        }
        #endregion

        #region FileRow

        public void OnFileRowSelect(CloudFileInfo cloudFileInfo)
        {
            if (cloudFileInfo.IsDirectory)
            {
                ListFiles(Context.Instance.CurrentPath.AppendPath(cloudFileInfo.FilePath));
            }
        }
        #endregion

        #region ToolBarView
        public void OnUpButtonClick()
        {
            var components = Context.Instance.CurrentPathComponent;
            if (components.Count > 0) {
                components.Remove(components.Last());
                ListFiles(components.GetPath());
            }
        }
        #endregion
    }
}
