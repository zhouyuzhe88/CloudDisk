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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private FolderPickerView FolderPickerView { get; set; }
        private FileView FileView { get; set; }
        private NavigationView NavigationView { get; set; }
        private ToolBarView ToolBarView { get; set; }

        private App App
        {
            get
            {
                return (App)Application.Current;
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            FolderPickerContainer.Children.Add(FolderPickerView = new FolderPickerView());
            FileContainer.Children.Add(FileView = new FileView());
            NavigationContainer.Children.Add(NavigationView = new NavigationView());
            ToolBarContainer.Children.Add(ToolBarView = new ToolBarView());
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            App.SignIn((signInSuccess) =>
            {
                if (signInSuccess)
                {
                    App.ListFiles("\\", (files, listSuccess) =>
                    {
                        if (files != null && listSuccess)
                        {
                            FileView.SetFiles(files);
                        }
                    });
                }
            });
        }
    }
}
