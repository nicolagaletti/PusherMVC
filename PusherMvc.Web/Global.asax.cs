using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Integration.Mvc;
using PusherMvc.Web.Repositories;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Embedded;

namespace PusherMvc.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region Autofac

            var builder = new ContainerBuilder();
            
            #region RavenDB
            
            //register document store as single instance
            builder.Register(ds =>
            {
                var documentStore = new EmbeddableDocumentStore
                {
                    ConnectionStringName = "RavenDB",
                    //by default ravendb separate parts with / but this could cause a problem with the routing engine
                    Conventions = {IdentityPartsSeparator = "-"}
                };

                documentStore.Initialize();
                return documentStore;
            }).As<IDocumentStore>().SingleInstance();

            //register session and open it
            builder.Register(s =>
                s.Resolve<IDocumentStore>().OpenSession()
                ).As<IDocumentSession>().InstancePerLifetimeScope().OnRelease(sr =>
                {
                    sr.SaveChanges();
                    sr.Dispose();
                });

            #endregion

            //TODO: register controllers to leverage autofac MVC

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}