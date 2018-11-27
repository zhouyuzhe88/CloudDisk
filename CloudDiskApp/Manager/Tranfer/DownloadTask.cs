using Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDiskApp
{
    class DownloadTask: TransferTask
    {
        public override void Start()
        {
            ClientWrapper.Instance.DownloadFile(RemoteFileFullPath, LocalFileFullPath, FileSet, OnStart, OnCompleted, OnDataTransfferd);
        }
        public override string FileName
        {
            get
            {
                return RemoteFileFullPath.GetLastComponent();
            }
        }

        public override string DirectoryPath
        {
            get
            {
                return RemoteFileFullPath.GetDirectoryPath().GetLastComponent();
            }
        }
    }
}
