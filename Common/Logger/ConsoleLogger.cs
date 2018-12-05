using System;

namespace Common.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(LogElement element)
        {
            Console.WriteLine(element);
        }
    }
}
