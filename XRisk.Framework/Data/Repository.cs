using XRisk.Logging;
using System.Data;

namespace XRisk.Data
{
    public abstract class Repository<T>
    {
        private readonly IConnectionLocator _connectionLocator;
        private readonly ILogger _logger;

        protected Repository(IConnectionLocator connectionLocator, ILogger logger)
        {
            _connectionLocator = connectionLocator;
            _logger = logger;
        }

        public ILogger Logger { get { return _logger; } }

        protected virtual IConnectionLocator ConnectionLocator
        {
            get { return _connectionLocator; }
        }

        protected virtual IDbConnection DbConnection
        {
            get { return _connectionLocator.For(typeof(T)); }
        }
    }
}