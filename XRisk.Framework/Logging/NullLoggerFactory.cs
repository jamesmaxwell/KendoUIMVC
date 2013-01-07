using System;

namespace XRisk.Logging
{
    class NullLoggerFactory : ILoggerFactory {
        public ILogger CreateLogger(string name) {
            return NullLogger.Instance;
        }
    }
}