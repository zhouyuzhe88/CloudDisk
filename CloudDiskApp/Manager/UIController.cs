using Common.Protocol;
using System.Linq;
using System;
using System.Windows;
using Common.Util;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;

namespace CloudDiskApp
{
    class UIController: IMainWindowManager, IFileRowManager, IToolBarViewManager, ITransferFileRowManager
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
                    DownloadFileTask task = new DownloadFileTask();
                    task.LocalFileFullPath = saveFileDialog.FileName;
                    task.RemoteFileFullPath = Context.Instance.CurrentPath.AppendPath(task.LocalFileFullPath.GetLastComponent());
                    task.FileLength = cloudFileInfo.FileLength;
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
        public void OnCreateFolderButtonClick()
        {
            string currentPath = Context.Instance.CurrentPath;
            CreateFolderDialog dialog = new CreateFolderDialog(currentPath.GetLastComponent());
            if(dialog.ShowDialog() == true)
            {
                string fullPath = currentPath.AppendPath(dialog.FolderName);
                ClientWrapper.Instance.CreateFolder(fullPath, success =>
                {
                    if (success)
                    {
                        ListFiles(Context.Instance.CurrentPath);
                    }
                });
            }
        }

        public void OnAddFileButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true && openFileDialog.FileNames != null)
            {
                foreach(string fileName in openFileDialog.FileNames)
                {
                    UploadFileTask task = new UploadFileTask();
                    task.LocalFileFullPath = fileName;
                    task.RemoteFileFullPath = Context.Instance.CurrentPath.AppendPath(task.LocalFileFullPath.GetLastComponent());
                    task.FileLength = new FileInfo(fileName).Length;
                    TransferManager.Instance.AddTask(task);
                }
            }
        }

        public void OnAddFolderButtonClick()
        {
            System.Windows.Forms.FolderBrowserDialog dailog = new System.Windows.Forms.FolderBrowserDialog();
            dailog.ShowDialog();
            Console.WriteLine(dailog.SelectedPath);
        }
        #endregion

        #region FileTransfer
        public void UpdateTransferList(List<TransferTask> list)
        {
            MainWindow.UpdateTransferList(list);
            MainWindow.RefreshTransferList();
        }
        #endregion

        #region TransferFileRow
        public void RefreshTransferList()
        {
            DiapatchMain(() =>
            {
                MainWindow.RefreshTransferList();
            });
        }
        #endregion
    }
}
