using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Others;
using Domain.Core.Interface.IUser;
using Domain.Core.User;
using Domain.Model.User;
using Microsoft.Owin.Security;
using WebPage.Areas.Member.Models;

namespace WebPage.Areas.Member.Controllers
{
    [Export]
    public class UserController : Controller
    {
        [Import]
        private IUserManager userManager;
        // [Import]
        //private IRoleManager roleManager ;
        //

        #region 登录
        public ActionResult Login()
        {

            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginModel_Me loginViewModel)
        {
       
            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion
            if (ModelState.IsValid)
            {
                string _password = Security.SHA256(loginViewModel.Password);
                var _response = userManager.Login(loginViewModel.Username, _password);

                if (_response.Code == 1)
                {
                    User user = (User)_response.Data;
                    CurrUser.Serialize(user.ID, user.Username);

                    return RedirectToAction("Index", "Home", new { Areas = "Member" });
                }
                else if (_response.Code == 2) ModelState.AddModelError("Accounts", _response.Message);
                else if (_response.Code == 3) ModelState.AddModelError("Password", _response.Message);
                else ModelState.AddModelError("", _response.Message);
            }
            return View(loginViewModel);
        }
        #endregion

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            #region 网站设置
            CustomCon custom = (CustomCon)ConfigurationManager.GetSection("customCon");
            WebInfo webInfo = custom.WebInfo;
            ViewBag.WebInfo = webInfo;
            #endregion

            if (userManager.HasUserName(register.UserName)) ModelState.AddModelError("UserName", "用户名已存在");
            if (userManager.HasEmail(register.Email)) ModelState.AddModelError("Email", "Email已存在");
            if (ModelState.IsValid)
            {
                User user = new User();
                if (TryUpdateModel(user))
                {
                    user.Password = Security.SHA256(register.Password);
                    user.RegTime = DateTime.Now;
                    user.RoleID = 1;
                    Response response = userManager.Add(user);
                    if (response.Code == 1)
                    {
                        user = response.Data;
                        CurrUser.Serialize(user.ID, user.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.er = "注册失败:" + response.Message;
                    }
                }
            }
            ViewBag.er = "注册失败";
            return View(register);
        }

        #endregion

        #region 校验

        [HttpPost]
        public ActionResult CheckUserNameR(string UserName)
        {
            return Json(!userManager.HasUserName(UserName));
        }

        [HttpPost]
        public ActionResult CheckEmailR(string Email)
        {
            return Json(!userManager.HasEmail(Email));
        }
        #endregion


        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
    }
}