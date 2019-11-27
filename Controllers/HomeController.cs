using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workshop_5_Group_3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Created by:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please contact one of our agents for all of your travel needs.";

            return View();
        }
    }
}