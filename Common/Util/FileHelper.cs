using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Common.Util
{
    public static class FileHelper
    {
        private const string FileSeparator = "\\";

        public const string FileRoot = "\\";

        public static List<string> GetPathComponents(this string path)
        {
            return path.Split(FileSeparator[0]).Where(x => x != "").ToList();
        }

        public static string GetPath(this List<string> pathComponents)
        {
            return FileSeparator + string.Join(FileSeparator, pathComponents.Where(x => x != "" && x != FileSeparator));
        }

        public static string AppendPath(this string path, string subPath)
        {
            return (path + FileSeparator + subPath).GetCleanPath();
        }

        public static string GetCleanPath(this string path)
        {
            return path.GetPathComponents().GetPath();
        }

        public static string GetFileSize(this long byteCnt)
        {
            if (byteCnt == 0)
            {
                return "0";
            }

            string[] suffix = { "B", "KB", "MB", "GB", "TB" };
            double cnt = byteCnt;
            int suffixIndex = 0;
            for (int i = 0; cnt > 1024 && i < suffix.Length - 1; ++i)
            {
                suffixIndex = i + 1;
                cnt /= 1024;
            }
            return string.Format("{0:0.00} {1}", cnt, suffix[suffixIndex]);
        }

        public static Icon GetFileIcon(string name, IconReader.IconSize size = IconReader.IconSize.Large)
        {
            return IconReader.GetFileIcon(name, size, false);
        }

        public static Icon GetFolderIcon(IconReader.IconSize size = IconReader.IconSize.Large)
        {
            return IconReader.GetFolderIcon(size, IconReader.FolderType.Closed);
        }

        public static ImageSource GetImageSource(this Icon icon)
        {
            return Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
