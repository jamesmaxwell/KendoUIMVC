using System;
using System.Configuration;
using System.IO;
using log4net;
using log4net.Config;

namespace XRisk.Logging
{
    public class Log4netFactory : ILoggerFactory
    {
        private static bool _isFileWatched = false;

        public Log4netFactory()
        {
            if (!_isFileWatched)
            {
                string configFilename = ConfigurationManager.AppSettings["log4net.Config"];
                if (string.IsNullOrWhiteSpace(configFilename))
                {
                    configFilename = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Config/log4net.config");
                }
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilename));
                _isFileWatched = true;
            }
        }
        public ILogger CreateLogger(string name)
        {
            return new Log4netLogger(LogManager.GetLogger(name), this);
        }
    }
}
