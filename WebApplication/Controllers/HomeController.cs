using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TestConnection()
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    return Content("✅ Connected to SQL Server successfully!");
                }
            }
            catch (Exception ex)
            {
                return Content("❌ Connection failed: " + ex.Message);
            }
        }
    }
}