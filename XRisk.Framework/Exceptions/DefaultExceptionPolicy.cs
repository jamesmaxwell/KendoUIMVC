using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using XRisk.Environment;
using XRisk.Events;
using XRisk.Logging;
using XRisk.Security;
using XRisk.UI.Notify;

namespace XRisk.Exceptions
{
    public class DefaultExceptionPolicy : IExceptionPolicy
    {
        private readonly INotifier _notifier;
        private readonly Work<IAuthorizer> _authorizer;

        public DefaultExceptionPolicy()
        {
            Logger = NullLogger.Instance;
        }

        public DefaultExceptionPolicy(INotifier notifier, Work<IAuthorizer> authorizer)
        {
            _notifier = notifier;
            _authorizer = authorizer;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public bool HandleException(object sender, Exception exception)
        {
            if (IsFatal(exception))
            {
                return false;
            }

            if (sender is IEventBus && exception is XRiskFatalException)
            {
                return false;
            }

            Logger.Error(exception, "An unexpected exception was caught");

            do
            {
                RaiseNotification(exception);
                exception = exception.InnerException;
            } while (exception != null);

            return true;
        }

        private static bool IsFatal(Exception exception)
        {
            return
                exception is OrchardSecurityException ||
                exception is StackOverflowException ||
                exception is AccessViolationException ||
                exception is AppDomainUnloadedException ||
                exception is ThreadAbortException ||
                exception is SecurityException ||
                exception is SEHException;
        }

        private void RaiseNotification(Exception exception)
        {
            if (_notifier == null || _authorizer.Value == null)
            {
                return;
            }
            if (exception is XRiskException)
            {
                _notifier.Error((exception as XRiskException).Message);
            }
            else if (_authorizer.Value.Authorize(StandardPermissions.SiteOwner))
            {
                _notifier.Error(exception.Message);
            }
        }
    }
}
