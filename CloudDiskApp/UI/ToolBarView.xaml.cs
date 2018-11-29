using System.Windows;
using System.Windows.Controls;

namespace CloudDiskApp
{
    interface IToolBarViewManager
    {
        void OnUpButtonClick();

        void OnAddFileButtonClick();

        void OnAddFolderButtonClick();

        void OnCreateFolderButtonClick();
    }

    /// <summary>
    /// ToolBarView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolBarView : UserControl
    {
        IToolBarViewManager Manager { get; set; }

        public ToolBarView()
        {
            Manager = UIController.Instance;
            InitializeComponent();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.OnUpButtonClick();
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.OnAddFileButtonClick();
        }

        private void CreateFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.OnCreateFolderButtonClick();
        }
    }
}
