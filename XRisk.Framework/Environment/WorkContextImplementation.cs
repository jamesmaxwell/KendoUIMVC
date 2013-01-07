using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace XRisk.Environment
{
    class WorkContextImplementation : WorkContext
    {
        private readonly Funq.Container _container;
        readonly ConcurrentDictionary<string, Func<object>> _stateResolvers = new ConcurrentDictionary<string, Func<object>>();
        readonly IEnumerable<IWorkContextStateProvider> _workContextStateProviders;

        public WorkContextImplementation(Funq.Container container)
        {
            _container = container;
            _workContextStateProviders = _container.TryResolve<IWorkContextStateProviderComposite>().StateProviders();
        }

        public override T Resolve<T>()
        {
            return _container.TryResolve<T>();
        }

        public override T GetState<T>(string name)
        {
            var resolver = _stateResolvers.GetOrAdd(name, FindResolverForState<T>);
            return (T)resolver();
        }

        Func<object> FindResolverForState<T>(string name)
        {
            var resolver = _workContextStateProviders.Select(wcsp => wcsp.Get<T>(name)).FirstOrDefault(value => !Equals(value, default(T)));

            if (resolver == null)
            {
                return () => default(T);
            }
            return () => resolver(this);
        }


        public override void SetState<T>(string name, T value)
        {
            _stateResolvers[name] = () => value;
        }
    }
}