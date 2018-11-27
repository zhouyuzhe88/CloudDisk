﻿using Common.Protocol;
using Common.Util;
using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
