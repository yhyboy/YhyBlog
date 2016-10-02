using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Common.Others;
using Domain.Core;
using Domain.Core.Interface;
using Domain.Model;
using Domain.Model.Blog;
using Domain.Model.User;
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
        [Import]
        private ITimnAxisManager ItimnAxisManager;
        //
        // GET: /Control/WebConfig/
        public ActionResult Index()
        {
            BlogConfig blogConfig = webConfigManager.GetConfig();
            return View(blogConfig);
        }
        [System.Web.Mvc.HttpPost]
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

        [System.Web.Mvc.HttpPost]
        public ActionResult MyInfo(string MyInfo)
        {
            Response response = new Response() { Code = 1 };
            webConfigManager.SetConfig("MyInfo", MyInfo);
            return Json(response);
        }

        public ActionResult Advance()
        {
       
            return View();
        }
        #region 获取列表
        #endregion

        #region 获取列表
        [System.Web.Mvc.HttpPost]
        public ActionResult TimnAxis(string order, int offset, int limit, string search)
        {
            Paging<TimnAxis> paging = new Paging<TimnAxis>();
            bool _order = true;
            if (!string.IsNullOrEmpty(order))
            {
                if (order != "asc")
                {
                    _order = false;
                }
            }
            if (offset > 0)
            {
                paging.PageIndex = offset / limit + 1;
            }
            if (limit > 0)
            {
                paging.PageSize = limit;
            }
            paging = ItimnAxisManager.FindPageList(paging, _order);
            return Json(new { total = paging.TotalNumber, rows = paging.Items });
        }
        #endregion

        #region 删除
        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteTimnAxis(int id)
        {                
            Response _res = ItimnAxisManager.Delete(id);
            return Json(_res);
        }
        #endregion

        #region 添加
        [System.Web.Mvc.HttpGet]
        public ActionResult AddTimnAxis()
        {
            return PartialView();
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTimnAxis([FromBody]TimnAxis timnAxis)
        {
      
            Response _res = new Response();
            if (ModelState.IsValid) _res = ItimnAxisManager.Add(timnAxis);
            else
            {
                _res.Code = 0;
                _res.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_res);
        }
        #endregion
    
        #region 修改
        [System.Web.Mvc.HttpGet]
        public ActionResult EditTimnAxis(int id)
        {
           
           var timnAxis= ItimnAxisManager.Find(id);
           if (timnAxis==null)
           {
               return Json("已经不存在",JsonRequestBehavior.AllowGet);
           }
           return PartialView(timnAxis);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult EditTimnAxis(TimnAxis timnAxis)
        {
            Response _resp = new Response();
            if (ModelState.IsValid)
            {
                _resp = ItimnAxisManager.Update(timnAxis);
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_resp);
        }

        #endregion

    }
}