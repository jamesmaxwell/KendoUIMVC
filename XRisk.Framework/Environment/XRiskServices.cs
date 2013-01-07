using System;
using XRisk.Data;
using XRisk.Security;
using XRisk.UI.Notify;

namespace XRisk.Environment
{
    public class XRiskServices : IXRiskServices
    {
        private readonly IWorkContextAccessor _workContextAccessor;

        public XRiskServices(
            IAuthorizer authorizer,
            INotifier notifier,
            IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
            Authorizer = authorizer;
            Notifier = notifier;
        }

        //public ITransactionManager TransactionManager { get; private set; }
        public IAuthorizer Authorizer { get; private set; }
        public INotifier Notifier { get; private set; }
        public WorkContext WorkContext { get { return _workContextAccessor.GetContext(); } }
    }
}
