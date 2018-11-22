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

        internal string CurrentPath { get; set; }
    }
}
