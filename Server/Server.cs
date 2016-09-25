﻿using Common.Util;
using Manager.Server;
using System;
using System.Threading;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            new CommandServer(Settings.GetIntValue("CommandServerPort")).Start();
            new DownloadFileServer(Settings.GetIntValue("DownloadServerPort")).Start();
            new UploadFileServer(Settings.GetIntValue("UpdateServerPort")).Start();
        }
    }
}
