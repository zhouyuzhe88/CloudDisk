using Common.Protocol;
using Common.Util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CloudDiskApp
{
    interface IFileRowManager
    {
        void OnFileRowSelect(CloudFileInfo cloudFileInfo);
    }

    /// <summary>
    /// FileRow.xaml 的交互逻辑
    /// </summary>
    public partial class FileRow : UserControl
    {
        private IFileRowManager Manager { get; set; }

        public FileRow()
        {
            Manager = UIController.Instance;
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        public static DependencyProperty MessageProperty = DependencyProperty.Register(
            "FileRowData", typeof(CloudFileInfo), typeof(FileRow));

        public CloudFileInfo FileRowData
        {
            get
            {
                return (CloudFileInfo)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }

        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CloudFileInfo cloudFileInfo = (sender as FrameworkElement).DataContext as CloudFileInfo;
            if (cloudFileInfo == null)
            {
                return;
            }
            
            if (cloudFileInfo.IsDirectory)
            {
                FileIconImage.Source = FileHelper.GetFolderIcon().GetImageSource();
            }
            else
            {
                FileIconImage.Source = FileHelper.GetFileIcon(cloudFileInfo.FilePath).GetImageSource();
                FileLengthTextBlock.Text = cloudFileInfo.FileLength.GetFileLengthString();
            }
        }

        private void FileRowControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Manager.OnFileRowSelect(FileRowData);
        }
    }
}
