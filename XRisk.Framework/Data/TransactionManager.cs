using System;
using System.Transactions;
using XRisk.Logging;

namespace XRisk.Data
{
    public interface ITransactionManager : IDisposable, IDependency
    {
        void Demand();
        void Cancel();
    }

    public class TransactionManager : ITransactionManager
    {
        private TransactionScope _scope;
        private bool _cancelled;
        private readonly ILogger _logger;

        public TransactionManager(ILogger logger)
        {
            _logger = logger;
        }

        public ILogger Logger { get { return _logger; } }

        void ITransactionManager.Demand()
        {
            if (_scope == null)
            {
                Logger.Debug("Creating transaction on Demand");
                _scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted
                    });
            }
        }

        void ITransactionManager.Cancel()
        {
            Logger.Debug("Transaction cancelled flag set");
            _cancelled = true;
        }

        void IDisposable.Dispose()
        {
            if (_scope != null)
            {
                if (!_cancelled)
                {
                    Logger.Debug("Marking transaction as complete");
                    _scope.Complete();
                }

                Logger.Debug("Final work for transaction being performed");
                try
                {
                    _scope.Dispose();
                }
                catch
                {
                    // swallowing the exception
                }
                Logger.Debug("Transaction disposed");
            }
        }
    }
}
