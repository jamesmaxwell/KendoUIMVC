using System;
using System.Collections.Concurrent;
using System.Web;
using XRisk.Mvc;
using ServiceStack;

namespace XRisk.Environment
{
    public class WorkContextAccessor : IWorkContextAccessor
    {
        readonly Funq.Container _container;

        readonly IHttpContextAccessor _httpContextAccessor;
        // a different symbolic key is used for each tenant.
        // this guarantees the correct accessor is being resolved.
        readonly object _workContextKey = new object();

        [ThreadStatic]
        static ConcurrentDictionary<object, WorkContext> _threadStaticContexts;

        public WorkContextAccessor(
            IHttpContextAccessor httpContextAccessor,
             Funq.Container container)
        {
            _httpContextAccessor = httpContextAccessor;
            _container = container;
        }

        public WorkContext GetContext(HttpContextBase httpContext)
        {
            return httpContext.Items[_workContextKey] as WorkContext;
        }

        public WorkContext GetContext()
        {
            var httpContext = _httpContextAccessor.Current();
            if (httpContext != null)
                return GetContext(httpContext);

            WorkContext workContext;
            return EnsureThreadStaticContexts().TryGetValue(_workContextKey, out workContext) ? workContext : null;
        }

        public IWorkContextScope CreateWorkContextScope(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
            //var activationBlock = _kernel.BeginBlock();
            //activationBlock.Get<WorkContextProperty<HttpContextBase>>().Value = httpContext;

            //return new HttpContextScopeImplementation(
            //    activationBlock,
            //    httpContext,
            //    _workContextKey);
        }


        public IWorkContextScope CreateWorkContextScope()
        {
            throw new NotImplementedException();
            //var httpContext = _httpContextAccessor.Current();
            //if (httpContext != null)
            //    return CreateWorkContextScope(httpContext);

            //return new ThreadStaticScopeImplementation(
            //    _kernel.BeginBlock(),
            //    EnsureThreadStaticContexts(),
                //_workContextKey);
        }

        static ConcurrentDictionary<object, WorkContext> EnsureThreadStaticContexts()
        {
            return _threadStaticContexts ?? (_threadStaticContexts = new ConcurrentDictionary<object, WorkContext>());
        }


        //class HttpContextScopeImplementation : IWorkContextScope
        //{
        //    readonly WorkContext _workContext;
        //    readonly Action _disposer;

        //    public HttpContextScopeImplementation(IActivationBlock activationBlock, HttpContextBase httpContext, object workContextKey)
        //    {
        //        _workContext = activationBlock.Get<WorkContext>();
        //        httpContext.Items[workContextKey] = _workContext;
        //        _disposer = () =>
        //        {
        //            httpContext.Items.Remove(workContextKey);
        //            activationBlock.Dispose();
        //        };
        //    }

        //    void IDisposable.Dispose()
        //    {
        //        _disposer();
        //    }

        //    public WorkContext WorkContext
        //    {
        //        get { return _workContext; }
        //    }

        //    public TService Resolve<TService>()
        //    {
        //        return WorkContext.Resolve<TService>();
        //    }
        //}

        //class ThreadStaticScopeImplementation : IWorkContextScope
        //{
        //    readonly WorkContext _workContext;
        //    readonly Action _disposer;

        //    public ThreadStaticScopeImplementation(IActivationBlock activationBlock, ConcurrentDictionary<object, WorkContext> contexts, object workContextKey)
        //    {
        //        _workContext = activationBlock.Get<WorkContext>();
        //        contexts.AddOrUpdate(workContextKey, _workContext, (a, b) => _workContext);
        //        _disposer = () =>
        //        {
        //            WorkContext removedContext;
        //            contexts.TryRemove(workContextKey, out removedContext);
        //            activationBlock.Dispose();
        //        };
        //    }

        //    void IDisposable.Dispose()
        //    {
        //        _disposer();
        //    }

        //    public WorkContext WorkContext
        //    {
        //        get { return _workContext; }
        //    }

        //    public TService Resolve<TService>()
        //    {
        //        return WorkContext.Resolve<TService>();
        //    }
        //}
    }
}
