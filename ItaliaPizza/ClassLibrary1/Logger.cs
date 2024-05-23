using System;
using log4net;
using System.Runtime.CompilerServices;

namespace Logs
{
    public class Logger
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}
