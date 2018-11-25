using Common.Protocol;
using System.Linq;
using System;
using System.Windows;
using Common.Util;
using System.Collections.Generic;
using Microsoft.Win32;

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
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = FileHelper.SystemDownloadPath;
                saveFileDialog.FileName = cloudFileInfo.FilePath;

                if (saveFileDialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    DownloadTask task = new DownloadTask();
                    task.LocalPath = saveFileDialog.FileName;
                    task.FileName = saveFileDialog.FileName.GetPathComponents().Last();
                    task.RemotePath = Context.Instance.CurrentPath.AppendPath(cloudFileInfo.FilePath);
                    TransferManager.Instance.AddTask(task);
                }
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

        #region FileTransfer
        public void UpdateTransferList(List<TransferTask> list)
        {
            MainWindow.UpdateTransferList(list);
        }
        #endregion
    }
}
