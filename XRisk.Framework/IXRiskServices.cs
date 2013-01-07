using XRisk.Data;
//using XRisk.ContentManagement;
using XRisk.Security;
using XRisk.UI.Notify;

namespace XRisk
{
    /// <summary>
    /// Most important parts of the XRisk API
    /// </summary>
    public interface IXRiskServices : IDependency
    {
        //IContentManager ContentManager { get; }
        //ITransactionManager TransactionManager { get; }
        IAuthorizer Authorizer { get; }
        INotifier Notifier { get; }

        WorkContext WorkContext { get; }
    }
}
