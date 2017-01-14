using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elmah.WebService.Client.Sample.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Fivehundred()
        {
            throw new NotImplementedException("Test exception from test/500 controller.");
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
    }
}