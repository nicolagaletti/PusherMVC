using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Repositories;
using Raven.Client;
using Raven.Client.Embedded;
using PusherMvc.Web.App_Start;
using System.Web.Optimization;
using System.Reflection;
using Raven.Database.Server;

namespace PusherMvc.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AutofacConfig.RegisterBuilder();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutomapperConfig.RegisterMappings();
        }
    }
}