using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Website.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View();
        }
    }
}