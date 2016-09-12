using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Others;
using Domain.Core.Interface.IUser;
using Domain.Core.User;
using Domain.Model.User;
using WebPage.Filter;

namespace WebPage.Areas.Control.Controllers
{
    [Export]
    [AdminAuthor]
    public class RoleController : Controller
    {
        [Import]
        private IRoleManager roleManager;
        //
        // GET: /Control/Role/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetList(string order, int offset, int limit)
        {
            Paging<Role> paging = new Paging<Role>();
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
            roleManager.FindPageList(paging, _order);
            var roles = paging.Items.Select(r => { return new { r.ID, r.Name, r.Description }; });
            return Json(new { total = paging.TotalNumber, rows = roles });

        }

        #region 添加
        [HttpGet]
        public ActionResult AddRole()
        {
            return PartialView("AddRolePartialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(Role role)
        {
        
            Response _res = new Response();
            if (ModelState.IsValid) _res = roleManager.Add(role);
            else
            {
                _res.Code = 0;
                _res.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_res);
        }
        #endregion

        #region 删除
        [HttpPost]
        public ActionResult DeleteRole(int id)
        {
            Response _res = roleManager.Delete(id);
            return Json(_res);
        }
        #endregion

        #region 修改

        [HttpGet]
        public ActionResult Modify(int id)
        {
           Role role= roleManager.Find(id);
           if (role==null)
           {
               return Json("该角色已经不存在",JsonRequestBehavior.AllowGet);
           }
           return PartialView(role);
        }
        [HttpPost]
        public ActionResult Modify(Role role)
        {
            Response _resp = new Response();
            if (ModelState.IsValid)
            {
                _resp = roleManager.Update(role);
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