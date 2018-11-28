using Common.Protocol;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CloudDiskApp
{
    /// <summary>
    /// FolderView.xaml 的交互逻辑
    /// </summary>
    public partial class FileView : UserControl
    {
        private List<CloudFileInfo> FileList { get; set; }

        public FileView()
        {
            InitializeComponent();
        }

        public void SetFiles(List<CloudFileInfo> fileList)
        {
            FileList = fileList;
            MainList.Dispatcher.Invoke(() => {
                MainList.ItemsSource = fileList;
            });
        }
    }
}
