using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Common;
using Common.Others;
using Domain.Core.Interface.IUser;
using Domain.Core.User;
using Domain.Model;
using WebPage.Areas.Control.Models;
using WebPage.Filter;
using Common.Log;

namespace WebPage.Areas.Control.Controllers
{
    [Export]
    [AdminAuthor]
    public class AdminController : Controller
    {
        [Import] 
        private IAdminManager adminManager;
        IExtLog log = ExtLogManager.GetLogger("dblog");

        //管理主界面
        // GET: /Control/Admin/
        public ActionResult Index()
        {
            return View();
        }

        //登录

        #region 登录
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
              
                string _password = Security.SHA256(loginViewModel.Password);
                var _response = adminManager.Login(loginViewModel.Accounts, _password);
                //修改               
                if (_response.Code == 1)
                {
                    var _admin = adminManager.Find(loginViewModel.Accounts);
                    CurrUser.Serialize(_admin.ID, _admin.Accounts,"admin");
                    _admin.LoginTime = DateTime.Now;
                    _admin.LoginIP = Request.UserHostAddress;
                    adminManager.Update(_admin);
                    //记录日志      
                    log.Info(Utils.GetIP(), _admin.Accounts,Request.Url.ToString(),"Login","后台登录成功");
                    return RedirectToAction("Index", "Admin", new { Areas = "Control" });
                }
                else if (_response.Code == 2) ModelState.AddModelError("Accounts", _response.Message);
                else if (_response.Code == 3) ModelState.AddModelError("Password", _response.Message);
                else ModelState.AddModelError("", _response.Message);
                log.Info(Utils.GetIP(), loginViewModel.Accounts, Request.Url.ToString(), "Login", "后台登录失败");
            }
            return View(loginViewModel);
        }
        #endregion


        //登出
        public ActionResult OutLogin()
        {
            CurrUser.Exit();
            return RedirectToAction("Login");
        }


        //获取数据
        [HttpPost]
        public ActionResult GetList(string order, int offset, int limit)
        {
            Paging<Administrator> paging = new Paging<Administrator>();
            bool _order=true;
            if (!string.IsNullOrEmpty(order))
            {
                if (order!="asc")
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
            adminManager.FindPageList(paging, _order);

            return Json(new { total = paging.TotalNumber, rows = paging.Items });
        }
        #region 添加
        //
        public ActionResult AddPartialView()
        {
            return PartialView();
        }

        //添加admin
        [HttpPost]
        public ActionResult Add(AddAdminViewModel addAdminViewModel)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {

                if (adminManager.HasAccounts(addAdminViewModel.Accounts))
                {
                    response.Code = 0;
                    response.Message = "帐号已存在";
                }
                else
                {
                    Administrator administrator = new Administrator();
                    administrator.Accounts = addAdminViewModel.Accounts;
                    administrator.Password = Security.SHA256(addAdminViewModel.Password);
                    administrator.CreateTime = DateTime.Now;
                    response = adminManager.Add(administrator);
                }
            }
            else
            {
                response.Code = 0;
                response.Message = General.GetModelErrorString(ModelState);
            }
            return Json(response);
        }
        #endregion

        #region 删除
        [HttpPost]
        public ActionResult DeletAdmin(string ids)
        {
            List<int> idss=new List<int>();
            string[] idssp = ids.Split(',');
            foreach (var id in idssp)
            {
                if (id != "start")
                {
                    idss.Add(int.Parse(id));
                }
            }
            int _total = idss.Count();
            Response response = new Response();
            if (idss.Contains(int.Parse(CurrUser.UserID)))
            {
                return Json(new Response() {Code = 0, Message = "不能删除当前登录用户"});
            }
            response=  adminManager.Delete(idss);
            if (response.Code>0)
            {
                response.Message = "共提交删除" + _total + "名管理员,实际删除" + response.Data + "名管理员.";
            }
            if (response.Code==0)
            {
                response.Message = "共提交删除" + _total + "名管理员,实际删除" + response.Data + "名管理员. 删除失败！";
            }
            return Json(response);
        }


        /// <summary>
        /// 删除 
        /// Response.Code:1-成功，2-部分删除，0-失败
        /// Response.Data:删除的数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteJsonBatch(List<int> ids)
        {

            int _total = ids.Count();
            Response _res = new Response();
            int _currentAdminID = int.Parse(CurrUser.UserID);
            if (ids.Contains(_currentAdminID))
            {
                ids.Remove(_currentAdminID);
            }
            _res = adminManager.Delete(ids);
            if (_res.Code == 1 && _res.Data < _total)
            {
                _res.Code = 2;
                _res.Message = "共提交删除" + _total + "名管理员,实际删除" + _res.Data + "名管理员。\n原因：不能删除当前登录的账号";
            }
            else if (_res.Code == 2)
            {
                _res.Message = "共提交删除" + _total + "名管理员,实际删除" + _res.Data + "名管理员。";
            }
            return Json(_res);
        }

        #endregion

        #region 我的资料
        /// <summary>
        /// 我的资料
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            return View(adminManager.Find(int.Parse(CurrUser.UserID)));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MyInfo(FormCollection form)
        {
            var _admin = adminManager.Find(int.Parse(CurrUser.UserID));

            if (_admin.Password != Security.SHA256(form["Password"]))
            {
                _admin.Password = Security.SHA256(form["Password"]);
                var _resp = adminManager.Update(_admin);
                if (_resp.Code == 1) ViewBag.Message = "<div class=\"alert alert-success\" role=\"alert\"><span class=\"glyphicon glyphicon-ok\"></span>修改密码成功！</div>";
                else ViewBag.Message = "<div class=\"alert alert-danger\" role=\"alert\"><span class=\"glyphicon glyphicon-remove\"></span>修改密码失败！</div>";
            }
            return View(_admin);
        }
#endregion

    }
}