using System;
using System.Collections.Generic;
using System.Linq;

namespace ChiaLogReader.App.App
{
    public class App
    {
        private string _chiaLogPath;

        public App(string logPath)
        {
            _chiaLogPath = logPath;
        }

        public void Run()
        {
            var lines = GetParsedLines();

            foreach (Severity severity in (Severity[])Enum.GetValues(typeof(Severity)))
            {
                Console.WriteLine($"{severity} : {lines.Count(l => l.Severity == severity)}");
            }

            Console.WriteLine();

            foreach (MessageType messageType in (MessageType[])Enum.GetValues(typeof(MessageType)))
            {
                Console.WriteLine($"{messageType} : {lines.Count(l => l.MessageType == messageType)}");
            }

            Console.WriteLine();

            foreach (var line in lines.Where(l=>l.MessageType == MessageType.unknown))
            {
                Console.WriteLine(line.OriginalMessage);
            }
        }

        private List<LogLine> GetParsedLines()
        {
            return System.IO.File.ReadAllLines(_chiaLogPath)
                                    .Select(l => new LogLine(l))
                                    .ToList();
        }
    }
}
