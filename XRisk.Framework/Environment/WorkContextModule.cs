using System;

namespace XRisk.Environment
{
    //public class WorkContextModule : NinjectModule
    //{
    //    public override void Load()
    //    {
    //        Bind<IWorkContextAccessor>().To<WorkContextAccessor>().InRequestScope();
    //        Bind<WorkContext>().To<WorkContextImplementation>().InRequestScope();
    //        Bind<WorkContextProperty<HttpContextBase>>().ToSelf().InRequestScope();
    //    }
    //}

    public class Work<T> where T : class
    {
        private readonly Func<Work<T>, T> _resolve;

        public Work(Func<Work<T>, T> resolve)
        {
            _resolve = resolve;
        }

        public T Value
        {
            get { return _resolve(this); }
        }
    }
}
