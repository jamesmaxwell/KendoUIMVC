[assembly: WebActivator.PreApplicationStartMethod(typeof(KendoUIMVC.App_Start.Combres), "PreStart")]
namespace KendoUIMVC.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}