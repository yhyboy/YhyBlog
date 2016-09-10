using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Others;
using Domain.Core;
using Domain.Core.Interface;
using Domain.Model;
using WebPage.Filter;

namespace WebPage.Areas.Control.Controllers
{
    [Export]
    [AdminAuthor]
    public class WebConfigController : Controller
    {
        private WebConfigManager webConfigManager = new WebConfigManager();
        [Import]
        private IBlogManager blogManager;

        //
        // GET: /Control/WebConfig/
        public ActionResult Index()
        {
            BlogConfig blogConfig = webConfigManager.GetConfig();
            return View(blogConfig);
        }
        [HttpPost]
        public ActionResult TopBlog(int id)
        {
            Response response = new Response() { Code = 0 };
            if (blogManager.Find(id) == null)
            {
                response.Message = "该博客不存在！";
                return Json(response);
            }
            webConfigManager.SetConfig("TopBlog", id.ToString());
            response.Code = 1;
            return Json(response);
        }

        [HttpPost]
        public ActionResult MyInfo(string MyInfo)
        {
            Response response = new Response() { Code = 1 };
            webConfigManager.SetConfig("MyInfo", MyInfo);
            return Json(response);
        }
    }
}