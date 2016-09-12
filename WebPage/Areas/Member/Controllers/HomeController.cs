using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Others;
using Domain.Core;
using Domain.Core.Category;
using Domain.Core.Interface;
using Domain.Core.Interface.ICategory;
using Domain.Core.User;
using Domain.Model;
using Domain.Model.Blog;
using Domain.Model.Category;

namespace WebPage.Areas.Member.Controllers
{
    [Export]
    public class HomeController : Controller
    {
        [Import]
        private IBlogManager blogManager;
        [Import]
        private ILeaveMsgManager leaveMsgManager;

        private WebConfigManager webConfigManager=new WebConfigManager();
        //
        // GET: /Member/Home/
        public ActionResult Index(int? pageindex, int id = -1)
        {
            Paging<Blog> page = new Paging<Blog>();
            page.PageIndex = pageindex == null ? 1 : pageindex.Value;
            page.PageSize = 5;
            page.Items = blogManager.Getlist(page, id).ToList();

            #region 构造分页
            //构造标签
            int PageCount = page.TotalNumber;
            int PageSize = page.PageSize;
            int totalPage = (PageCount + PageSize - 1) / PageSize;
            string urlhead = "/Member/Home/Index/" + id + "?pageindex=";
            string pages = "<li><a href='" + urlhead + "1" + "'>首页</a></li>"; ;
            if (page.PageIndex == 1)
            {
                pages += "<li class='disabled'><a href='#'>上一页</a></li>";
            }
            else
            {
                pages += "<li ><a href='" + urlhead + (page.PageIndex - 1) + "'>上一页</a></li>";
            }
            if (page.PageIndex == totalPage)
            {
                pages += "<li class='disabled'><a href='#'>下一页</a></li>";
            }
            else
            {
                pages += "<li ><a href='" + urlhead + (page.PageIndex + 1) + "'>下一页</a></li>";
            }
            pages += "<li ><a href='" + urlhead + totalPage + "'>最后一页</a></li>";
            ViewBag.Page = pages;
            ViewBag.PageIndex = page.PageIndex;
            ViewBag.PageCount = totalPage;
            #endregion

            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion

            #region 置顶博客
         
            var TopBlog = blogManager.Find(webConfigManager.GetConfig().TopBlog);
            ViewBag.TopBlog = TopBlog;

            #endregion

            return View(page.Items);

        }



        public ActionResult About()
        {
            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion

           BlogConfig blogConfig= webConfigManager.GetConfig();
           return View(blogConfig);
        }
        public ActionResult LeaveMsg()
        {
            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion

            List<LeaveMsg> leaveMsgs = leaveMsgManager.FindList().AsEnumerable().OrderBy(x => Guid.NewGuid()).Take(20).ToList();
            return View(leaveMsgs);
        }

        public ActionResult DoLeaveMsg()
        {
    
            if (!CurrUser.IsLogin)
            {
                return Json("必须是登录用户才能留言", JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoLeaveMsg(LeaveMsg leaveMsg_me)
        {
            Response response = new Response() { Code = 0 };
            if (!CurrUser.IsLogin)
            {
                response.Message = "必须是登录用户才能留言";
                return Json(response);
            }
            if (ModelState.IsValid)
            {
                Domain.Model.User.User user = new UserManager().Find(Convert.ToInt32(CurrUser.UserID));
                if (user == null)
                {
                    response.Message = "必须是注册前台用户才能留言";
                    return Json(response);
                }
                Domain.Model.Blog.LeaveMsg leaveMsg = new LeaveMsg();
                leaveMsg.Msg = leaveMsg_me.Msg;
                leaveMsg.CreateTime = DateTime.Now;
                leaveMsg.User = user;
                response = leaveMsgManager.Add(leaveMsg);
            }
            return Json(response);
        }
        //登出
        public ActionResult Out()
        {
            CurrUser.Exit();
            return RedirectToAction("Index", "Home", new { Areas = "Member" });
        }
    }
}