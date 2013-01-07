using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Routing;
using Funq;
using ServiceStack.Mvc;
using XRisk;
using XRisk.Data;
using XRisk.Settings;
using XRisk.Mvc;
using XRisk.Environment;
using XRisk.Events;
using XRisk.Exceptions;
using XRisk.Services;
using XRisk.Security;
using XRisk.UI.Notify;

namespace KendoUIMVC
{
    public class Global : System.Web.HttpApplication
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("servicestack.ashx/{*pathInfo}");

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var container = new Funq.Container();

            container.RegisterAs<WorkContextAccessor, IWorkContextAccessor>().ReusedWithin(ReuseScope.Container);
            container.RegisterAs<XRiskServices, IXRiskServices>().ReusedWithin(ReuseScope.Container);
            container.RegisterAs<DefaultEventBus, IEventBus>().ReusedWithin(ReuseScope.Container);
            container.RegisterAs<HttpContextAccessor, IHttpContextAccessor>().ReusedWithin(ReuseScope.Container);
            container.RegisterAs<DefaultExceptionPolicy, IExceptionPolicy>().ReusedWithin(ReuseScope.Container);
            container.RegisterAs<Authorizer, IAuthorizer>().ReusedWithin(ReuseScope.Container);
            container.RegisterAs<RolesBasedAuthorizationService, IAuthorizationService>()
                     .ReusedWithin(ReuseScope.Container);
            container.RegisterAs<Notifier, INotifier>().ReusedWithin(ReuseScope.Container);

            container.RegisterAs<Clock, IClock>().ReusedWithin(ReuseScope.Container);

            container.RegisterAs<OracleConnectionLocator, IConnectionLocator>().ReusedWithin(ReuseScope.None);
            container.Register<IWorkContextStateProviderComposite>(c => new WorkContextStateProviderComposite(
                                                                            new CurrentSiteWorkContext(
                                                                                c.TryResolve<ISiteService>()),
                                                                            new HttpContextWorkContext(
                                                                                c.TryResolve<IHttpContextAccessor>())))
                                                                                .ReusedWithin(ReuseScope.Request);

            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
        }

        protected void Application_BeginRequest(object src, EventArgs e)
        {
            //if (Request.IsLocal)
            //    ServiceStack.MiniProfiler.Profiler.Start();
        }

        protected void Application_EndRequest(object src, EventArgs e)
        {
            //ServiceStack.MiniProfiler.Profiler.Stop();
        }

    }
}
