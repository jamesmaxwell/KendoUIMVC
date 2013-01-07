using System;

namespace XRisk.Logging
{
    public interface ILoggerFactory {
        ILogger CreateLogger(string name);
    }
}