using Common.Util;
using System.Collections.Generic;

namespace CloudDiskApp
{
    class Context
    {
        private Context()
        {
            CurrentFileSet = "";
        }

        private static Context instance = new Context();

        internal static Context Instance
        {
            get
            {
                return instance;
            }
        }

        internal string CurrentFileSet { get; set; }

        internal List<string> CurrentPathComponent
        {
            get
            {
                return CurrentPath.GetPathComponents();
            }
        }

        internal string CurrentPath { get; set; }
    }
}
