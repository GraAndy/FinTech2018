using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinTech.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Users = "info@fintech.ru")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}