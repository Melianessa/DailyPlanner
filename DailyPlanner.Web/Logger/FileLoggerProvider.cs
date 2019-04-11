using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DailyPlanner.Web.Logger
{
    public class FileLoggerProvider:ILoggerProvider
    {
        private string path;

        public FileLoggerProvider(string _path)
        {
            path = _path;
        }
        public void Dispose()
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path);
        }

    }

}
