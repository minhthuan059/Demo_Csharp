using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error(int? statusCode, string message, string stackTrace)
        {
            // Gửi thông tin lỗi ra ViewBag nếu muốn hiển thị cho dev
            ViewBag.Message = message;
            ViewBag.StackTrace = stackTrace;
            ViewBag.StatusCode = statusCode != null ? statusCode : 500;

            return View("Error");
        }

    }
}