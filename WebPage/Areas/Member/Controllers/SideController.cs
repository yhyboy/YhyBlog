using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Core;
using Domain.Core.Category;
using Domain.Core.Interface;
using Domain.Core.Interface.ICategory;
using Domain.Model.Blog;
using Domain.Model.Category;

namespace WebPage.Areas.Member.Controllers
{
    [Export]
    public class SideController : Controller
    {
        [Import] 
        private IBlogManager blogManager;

        [Import] 
        private ICategoryManager categoryManager;
       // [Import]
        //private ILeaveMsgManager leaveMsgManager;
        

        /// <summary>
        /// 侧栏内容
        /// </summary>
        /// <returns></returns>
        public ActionResult _HomeSideNavPartial()
        {
            //获取一些侧栏信息
            List<Category> categories = categoryManager.FindList().OrderBy(c => c.Depth).ToList();
            List<Blog> blogs = blogManager.FindList().OrderByDescending(b => b.CreateTime).Take(8).ToList();
            List<Blog> blogs2 = blogManager.FindList().OrderByDescending(b => b.Volume).Take(8).ToList();
            ViewBag.NewBlog = blogs;
            ViewBag.ReadBlog = blogs2;
            return PartialView(categories);
        }
    }
}