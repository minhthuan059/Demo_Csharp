using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebApplication.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello

        [HttpGet]
        [Route("hello")]
        public ActionResult Index(string name, string email)
        {
            var user = new Models.User
            {
                Name = name ?? "Guest",
                Email = email
            };
            return View(user);
        }
    }
}