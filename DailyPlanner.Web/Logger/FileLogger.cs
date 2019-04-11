using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Web.Logger
{
    public class FileLogger:ILogger
    {
        private string filePath;
        private object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter!=null)
            {
                lock (_lock)
                {
                    File.AppendAllText(filePath,formatter(state,exception)+Environment.NewLine);
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
