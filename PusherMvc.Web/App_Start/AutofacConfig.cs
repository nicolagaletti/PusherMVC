using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using PusherMvc.Data.Contracts;
using PusherMvc.Data.Repositories;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;
using PusherMvc.Web.Services;
using PusherMvc.Web.Contracts;
using System.Configuration;
using PusherServer;

namespace PusherMvc.Web.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterBuilder()
        {
            #region Autofac

            var builder = new ContainerBuilder();

            #region RavenDB

            //register document store as single instance
            builder.Register(ds =>
            {
                var documentStore = new EmbeddableDocumentStore
                {
                    //If you don't specify the line below the document store will not be visible if the app is running
                    //if you get a permission denied run cmd and execute the following:
                    //netsh http add urlacl url=http://+:8080/ user=Everyone
                    //UseEmbeddedHttpServer = true,
                    ConnectionStringName = "RavenDB",
                    //by default ravendb separate parts with / but this could cause a problem with the routing engine
                    Conventions = { IdentityPartsSeparator = "-" }
                };

                //NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                //documentStore.Configuration.Port = 8080;

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

            builder.RegisterType<Pusher>()
                .As<IPusher>()
                .WithParameters(new List<NamedParameter>() {
                new NamedParameter("appId", ConfigurationManager.AppSettings["application_id"]),
                new NamedParameter("appKey", ConfigurationManager.AppSettings["application_key"]),
                new NamedParameter("appSecret", ConfigurationManager.AppSettings["application_secret"])
                })
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerHttpRequest();
            
            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerHttpRequest();

            builder.RegisterType<PusherService>()
                .As<IPusherService>()
                .InstancePerLifetimeScope();



            //register controllers to leverage autofac MVC
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion
        }
    }
}