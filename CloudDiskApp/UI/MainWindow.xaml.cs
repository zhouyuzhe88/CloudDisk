using Common.Protocol;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CloudDiskApp
{
    interface IMainWindowManager
    {
        void OnMainWindowLoaded();
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainWindowManager Manager { get; set; }

        private FolderPickerView FolderPickerView { get; set; }
        private FileView FileView { get; set; }
        private NavigationView NavigationView { get; set; }
        private ToolBarView ToolBarView { get; set; }

        private TransferFileWindow TransferFileWindow { get; set; }


        public MainWindow()
        {
            Manager = UIController.Instance;
            InitializeComponent();
            FolderPickerContainer.Children.Add(FolderPickerView = new FolderPickerView());
            FileContainer.Children.Add(FileView = new FileView());
            NavigationContainer.Children.Add(NavigationView = new NavigationView());
            ToolBarContainer.Children.Add(ToolBarView = new ToolBarView());
            TransferFileWindow = new TransferFileWindow();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            Manager.OnMainWindowLoaded();
        }

        public void SetFiles(List<CloudFileInfo> fileList)
        {
            FileView.SetFiles(fileList);
        }

        public void SetPath(string path)
        {
            NavigationView.SetPath(path);
        }

        public void UpdateTransferList(List<TransferTask> list)
        {
            TransferFileWindow.SetTaskList(list);
            TransferFileWindow.Show();
        }

        public void RefreshTransferList()
        {
            TransferFileWindow.Refresh();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
