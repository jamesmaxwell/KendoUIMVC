using System;
using XRisk.Data;
using XRisk.Security;
using XRisk.UI.Notify;

namespace XRisk.Environment
{
    public class XRiskServices : IXRiskServices
    {

        public XRiskServices(
            IAuthorizer authorizer,
            INotifier notifier)
        {
            Authorizer = authorizer;
            Notifier = notifier;
        }

        //public ITransactionManager TransactionManager { get; private set; }
        public IAuthorizer Authorizer { get; private set; }
        public INotifier Notifier { get; private set; }
    }
}
