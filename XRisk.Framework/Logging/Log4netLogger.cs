using System;
using System.Globalization;

using log4net;
using log4net.Core;
using log4net.Util;

namespace XRisk.Logging
{
    [Serializable]
    public class Log4netLogger : MarshalByRefObject, ILogger
    {
        private static readonly Type declaringType = typeof(Log4netLogger);

        public Log4netLogger(log4net.Core.ILogger logger, Log4netFactory factory)
        {
            Logger = logger;
            Factory = factory;
        }

        internal Log4netLogger()
        {
        }

        internal Log4netLogger(ILog log, Log4netFactory factory)
            : this(log.Logger, factory)
        {
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return IsDebugEnabled;
                case LogLevel.Information:
                    return IsInfoEnabled;
                case LogLevel.Warning:
                    return IsWarnEnabled;
                case LogLevel.Error:
                    return IsWarnEnabled;
                case LogLevel.Fatal:
                    return IsFatalEnabled;
            }
            return false;
        }

        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            log4net.Core.Level log4Level = null;
            switch (level)
            {
                case LogLevel.Debug:
                    log4Level = log4net.Core.Level.Debug;
                    break;
                case LogLevel.Error:
                    log4Level = log4net.Core.Level.Error;
                    break;
                case LogLevel.Fatal:
                    log4Level = log4net.Core.Level.Fatal;
                    break;
                case LogLevel.Information:
                    log4Level = log4net.Core.Level.Info;
                    break;
                case LogLevel.Warning:
                    log4Level = log4net.Core.Level.Warn;
                    break;
                default:
                    log4Level = log4net.Core.Level.Debug;
                    break;
            }
            Logger.Log(declaringType, log4Level, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), exception);
        }

        public bool IsDebugEnabled
        {
            get { return Logger.IsEnabledFor(Level.Debug); }
        }

        public bool IsErrorEnabled
        {
            get { return Logger.IsEnabledFor(Level.Error); }
        }

        public bool IsFatalEnabled
        {
            get { return Logger.IsEnabledFor(Level.Fatal); }
        }

        public bool IsInfoEnabled
        {
            get { return Logger.IsEnabledFor(Level.Info); }
        }

        public bool IsWarnEnabled
        {
            get { return Logger.IsEnabledFor(Level.Warn); }
        }

        protected internal Log4netFactory Factory { get; set; }

        protected internal log4net.Core.ILogger Logger { get; set; }

        public override string ToString()
        {
            return Logger.ToString();
        }
    }
}