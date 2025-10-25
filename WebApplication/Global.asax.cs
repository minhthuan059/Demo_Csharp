using Autofac;
using Autofac.Integration.Mvc;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

            //Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());

            //using (var context = new AppDbContext())
            //{
            //    context.Database.Initialize(force: true);
            //}

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();


            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly, Assembly.GetExecutingAssembly())
                   .AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            var ex = exception as HttpException ?? new HttpException(500, "Internal Server Error", exception);

            // 🧾 Ghi log lỗi ra file (App_Data/error_yyyy-MM-dd.txt)
            string logDir = Server.MapPath("~/App_Data");
            Directory.CreateDirectory(logDir);
            string logPath = Path.Combine(logDir, $"error_{DateTime.Now:yyyy-MM-dd}.txt");

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine("=== ERROR at {0} ===", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("Source: " + ex.Source);
                writer.WriteLine("TargetSite: " + ex.TargetSite);
                writer.WriteLine("StackTrace: " + ex.StackTrace);
                writer.WriteLine();
            }

            // 🧹 Xóa lỗi khỏi pipeline mặc định
            Server.ClearError();

            // 🚀 Chuyển hướng đến controller lỗi (dùng 1 view duy nhất)
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Error";
            routeData.Values["statusCode"] = ex.GetHttpCode();
            routeData.Values["message"] = ex.Message;
            routeData.Values["stackTrace"] = ex.StackTrace;

            IController errorController = new Controllers.ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
