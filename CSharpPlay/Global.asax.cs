using CSharpPlay.Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.Mvc5;

namespace CSharpPlay
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new UnityContainer();

            var assemblies = AllClasses.FromAssemblies(BuildManager.GetReferencedAssemblies().Cast<Assembly>());
            //container.RegisterTypes
            //    (
            //        assemblies
            //            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))),
            //        WithMappings.FromAllInterfaces,
            //        WithName.TypeName,
            //        WithLifetime.PerThread,
            //        overwriteExistingMappings: true
            //    );
            container.RegisterType<IDomainEventHandler<ProductDoSomethingFinishedEvent>, ProductDoSomethingFinishedEventHandler>("a", new HierarchicalLifetimeManager());
            container.RegisterType<ProductService, ProductService>(new HierarchicalLifetimeManager());
            //container.RegisterType<DomainEvents, DomainEvents>(new HierarchicalLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
