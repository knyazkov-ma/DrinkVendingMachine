using DrinkVendingMachine.Migration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.ServiceLocation;

namespace DrinkVendingMachine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityServiceLocator serviceLocator = new UnityServiceLocator(UnityConfig.Container);
            IMigrationRunner migrationRunner = serviceLocator.GetInstance<IMigrationRunner>();
            migrationRunner.Update();
        }
    }
}
