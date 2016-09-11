using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Domain.Core;
using Domain.Core.Interface;
using Domain.Model.Blog;
using WebPage.Models;

namespace WebPage.Areas.Member.Controllers
{
     [Export]
    public class ContentController : Controller
    {
       [Import]
        private IBlogManager blogManager ;
        //
        // GET: /Member/Content/
        public ActionResult Index(int id)
        {
            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion
          Blog blog=  blogManager.Find(id);
          if (blog==null)
          {
              return View("Prompt" ,new Prompt() { Title = "错误", Message = "该文章不存在！" });
          }
          if (!blog.Publish)
          {
              return View("Prompt", new Prompt() { Title = "错误", Message = "该文章未发布！" });
          }
          blog.Volume = blog.Volume+1;
          blogManager.Update(blog);

      

          return View(blog);
        }
	}
}