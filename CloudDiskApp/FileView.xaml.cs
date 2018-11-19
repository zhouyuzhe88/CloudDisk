using Common.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// FolderView.xaml 的交互逻辑
    /// </summary>
    public partial class FileView : UserControl
    {
        private List<CloudFileInfo> Files { get; set; }

        private List<string> FileNames {
            get
            {

                if (Files != null)
                {
                    return Files.Select(x => { return x.FilePath; }).ToList();
                }
                List<string> ret = new List<string>();
                return null;
            }
        }

        public FileView()
        {
            InitializeComponent();
            MainList.ItemsSource = Files;
        }

        public void SetFiles(List<CloudFileInfo> files)
        {
            Files = files;
            MainList.Dispatcher.Invoke(() => {
                MainList.ItemsSource = Files;
            });
        }
    }
}
