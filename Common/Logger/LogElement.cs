using System;

namespace Common.Logger
{
    public class LogElement
    {
        public DateTime Time { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Message { get; set; }

        internal LogElement() { }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", Log.LogLevelStr(LogLevel), Time.ToString("yyyy/MM/dd HH:mm:ss.fff"), Message);
        }
    }
}
