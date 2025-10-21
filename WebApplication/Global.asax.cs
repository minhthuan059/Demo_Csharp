using Autofac;
using Autofac.Integration.Mvc;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.Interfaces.Repositories.Models;
using WebApplication.Models;
using WebApplication.Repositories;
using static System.Net.Mime.MediaTypeNames;
using MediatR;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();

            // 🔹 1. Register all controllers in this assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // 🔹 2. Register MediatR
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            // 🔹 3. Register ServiceFactory
            builder.RegisterType<NotificationRepository>()
                   .As<INotificationRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                   .As<IUserRepository>()
                   .InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // 🔹 4. Register all handlers from Application assembly
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly, Assembly.GetExecutingAssembly())
                   .AsImplementedInterfaces();


            // 🔹 5. Build container
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // 🔹 6. Register MVC defaults
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
