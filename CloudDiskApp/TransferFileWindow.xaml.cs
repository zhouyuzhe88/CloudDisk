using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace CloudDiskApp
{
    /// <summary>
    /// TransferFileWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TransferFileWindow : Window
    {
        private List<TransferTask> TaskList { get; set; }

        public TransferFileWindow()
        {
            InitializeComponent();
        }

        public void SetTaskList(List<TransferTask> taskList)
        {
            TaskList = taskList;
            MainList.Dispatcher.Invoke(() => {
                MainList.ItemsSource = TaskList;
            });
        }

        public void Refresh()
        {
            CollectionViewSource.GetDefaultView(TaskList).Refresh();
        }
        
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
