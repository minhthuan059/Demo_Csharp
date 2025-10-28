using Autofac;
using Autofac.Integration.Mvc;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
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
using WebApplication.App_Start;
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

            AutofacConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            using (var context = new AppDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                    roleManager.Create(new IdentityRole("Admin"));

                if (!roleManager.RoleExists("User"))
                    roleManager.Create(new IdentityRole("User"));
            }
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
