using Autofac;
using Autofac.Integration.Mvc;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Reflection;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // 🔹 Đăng ký MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // 🔹 Đăng ký DbContext
            builder.RegisterType<AppDbContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            // 🔹 Đăng ký Identity
            builder.RegisterType<UserStore<User>>()
                   .As<IUserStore<User>>()
                   .WithParameter("context", new AppDbContext())
                   .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            //builder.RegisterType<ApplicationSignInManager>()
            //       .AsSelf()
            //       .InstancePerLifetimeScope();

            // 🔹 Đăng ký MediatR
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AsImplementedInterfaces();

            // 🔹 Build container
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
