using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigmaBotCore
{
    public static class Logger
    {
        public static bool IsRunning { get; private set; } = false;
        private static List<string> _log = new List<string>();
        private static DateTime _start;
        public static void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _start = DateTime.Now;
            }
        }
        public static void Stop()
        {
            if (IsRunning)
            {
                Log("Printing logs...", "Logger");
                IsRunning = false;
                PrintLog();
                _log.Clear();
            }
        }

        public static void Log(string message, string channel, LogOrigin origin = LogOrigin.SYS, LogType type = LogType.INF)
        {
            if (!IsRunning) return;
            string str = $"{DateTime.Now} [{type}\\{origin}@{channel}] - {message}";
            _log.Add(str);
            Console.WriteLine(str);
        }

        public static void PrintLog()
        {
            if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");

            string fileName = $"{_start:yyyy-MM-dd HH-mm-ss}.log";
            using (StreamWriter write = new StreamWriter(@"logs\" + fileName))
            {
                foreach (var line in _log) write.WriteLine(line);
            }
        }
    }

    public enum LogType
    {
        INF,
        WRN,
        ERR
    }

    public enum LogOrigin
    {
        MSG,
        CMD,
        SYS
    }
}
