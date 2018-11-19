using Common.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CloudDiskApp
{
    /// <summary>
    /// FileRow.xaml 的交互逻辑
    /// </summary>
    public partial class FileRow : UserControl
    {
        public FileRow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        public static DependencyProperty MessageProperty = DependencyProperty.Register(
            "CloudFileInfo", typeof(CloudFileInfo), typeof(FileRow));

        public CloudFileInfo CloudFileInfo
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

            DirTextBlock.Text = Convert.ToString(cloudFileInfo.IsDirectory);
        }


    }
}
