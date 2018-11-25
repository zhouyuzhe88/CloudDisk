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
    /// TransferFileRow.xaml 的交互逻辑
    /// </summary>
    public partial class TransferFileRow : UserControl
    {
        public TransferFileRow()
        {
            InitializeComponent();
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
    }
}
