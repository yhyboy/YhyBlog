using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPage.Filter;

namespace WebPage.Areas.Control.Controllers
{
   [AdminAuthor]
    public class WebConfigController : Controller
    {
        //
        // GET: /Control/WebConfig/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }
	}
}