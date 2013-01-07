using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace XRisk
{
    public interface IWorkContextAccessor
    {
        WorkContext GetContext(HttpContextBase httpContext);
        IWorkContextScope CreateWorkContextScope(HttpContextBase httpContext);

        WorkContext GetContext();
        IWorkContextScope CreateWorkContextScope();
    }

    public interface IWorkContextStateProvider : IDependency
    {
        Func<WorkContext, T> Get<T>(string name);
    }

    public interface IWorkContextStateProviderComposite
    {
        IEnumerable<IWorkContextStateProvider> StateProviders();
    }

    public class WorkContextStateProviderComposite : IWorkContextStateProviderComposite
    {
        private readonly IEnumerable<IWorkContextStateProvider> _stateProviders;

        public WorkContextStateProviderComposite(params IWorkContextStateProvider[] stateProviders)
        {
            _stateProviders = stateProviders.ToArray();
        }

        public IEnumerable<IWorkContextStateProvider> StateProviders() { return _stateProviders; }
    }

    public interface IWorkContextScope : IDisposable
    {
        WorkContext WorkContext { get; }
        TService Resolve<TService>();
    }
}
