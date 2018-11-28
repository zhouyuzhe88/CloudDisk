using System.Windows.Controls;

namespace CloudDiskApp
{
    /// <summary>
    /// NavigationView.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
        }

        public void SetPath(string path)
        {
            URLTextBlock.Text = path;
        }
    }
}
