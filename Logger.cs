using System;
using System.Collections.Generic;
using System.Text;

namespace SiliconBot
{
    public static class Logger
    {
        public static void Log(string message, LogType type = LogType.INFO)
        {
            Console.WriteLine($"{DateTime.Now} [{type}] - {message}");
        }
    }

    public enum LogType
    {
        INFO,
        WARN,
        ERROR
    }
}
