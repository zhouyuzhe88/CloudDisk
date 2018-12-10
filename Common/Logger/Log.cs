using Common.Util;
using System;

namespace Common.Logger
{
    public enum LogLevel
    {
        Error,
        Warn,
        Info,
        Debug,
        Verbose
    }

    public static class Log
    {
        private static ILogger Logger { get; set; }

        private static int DisplayLogLevel { get; set; }

        static Log()
        {
            DisplayLogLevel = Settings.GetIntValue("DisplayLogLevel");
            Logger = new ConsoleLogger();
        }

        internal static string LogLevelStr(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Error:
                    return "E";
                case LogLevel.Warn:
                    return "W";
                case LogLevel.Info:
                    return "I";
                case LogLevel.Debug:
                    return "D";
                case LogLevel.Verbose:
                    return "V";
            }
            throw new NotImplementedException();
        }

        public static void SetDisplayLogLevel(LogLevel logLevel)
        {
            DisplayLogLevel = (int)logLevel;
        }

        public static void SetLogger(ILogger logger)
        {
            Logger = logger;
        }

        public static void V(string format, params object[] arg)
        {
            X(format, arg, LogLevel.Verbose);
        }
        
        public static void D(string format, params object[] arg)
        {
            X(format, arg, LogLevel.Debug);
        }

        public static void I(string format, params object[] arg)
        {
            X(format, arg, LogLevel.Info);
        }

        public static void W(string format, params object[] arg)
        {
            X(format, arg, LogLevel.Warn);
        }

        public static void E(string format, params object[] arg)
        {
            X(format, arg, LogLevel.Error);
        }

        public static void E(Exception e)
        {
            E(e.Message + "\n" + e.StackTrace);
        }

        private static void X(string format, object[] arg, LogLevel logLevel)
        {
            if (DisplayLogLevel < (int)logLevel)
            {
                return;
            }

            string message = string.Format(format, arg);
            LogElement element = new LogElement() { Time = DateTime.Now, LogLevel = logLevel, Message = message };
            Logger?.Log(element);
        }
    }
}
