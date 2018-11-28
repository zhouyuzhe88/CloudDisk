using Common.Util;
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
    interface ITransferFileRowManager
    {
        void RefreshTransferList();
    }

    /// <summary>
    /// TransferFileRow.xaml 的交互逻辑
    /// </summary>
    public partial class TransferFileRow : UserControl
    {
        public TransferFileRow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            Loaded += TransferFileRow_Loaded;
        }

        private void TransferFileRow_Loaded(object sender, RoutedEventArgs e)
        {
            double persetage = 1.0 * TransferFileRowData.TranffedLength / TransferFileRowData.FileLength;
            ColorGrid.Width = FirstGrid.ActualWidth * persetage;
        }

        public static DependencyProperty MessageProperty = DependencyProperty.Register(
            "TransferFileRowData", typeof(TransferTask), typeof(TransferFileRow));

        public TransferTask TransferFileRowData
        {
            get
            {
                return (TransferTask)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TransferTask task = (sender as FrameworkElement).DataContext as TransferTask;
            if (task == null)
            {
                return;
            }
            FileIconImage.Source = FileHelper.GetFileIcon(task.FileName).GetImageSource();

            switch (task.Status)
            {
                case TransferTask.TaskStatus.Pending:
                    StatusTextBlock.Text = "Pending";
                    break;
                case TransferTask.TaskStatus.Running:
                    double persetage = 1.0 * task.TranffedLength / task.FileLength;
                    StatusTextBlock.Text = string.Format("{0:0.00}% ({1})", 100.0 * persetage, task.Speed);
                    break;
                case TransferTask.TaskStatus.Completed:
                    StatusTextBlock.Text = "Completed";
                    break;
            }
        }

        private void TransferFileRowControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TransferFileRowData.Start();
        }
    }
}
