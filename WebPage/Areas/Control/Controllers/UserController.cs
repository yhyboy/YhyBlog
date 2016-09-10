using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Common;
using Common.Others;
using Domain.Core.Interface.IUser;
using Domain.Core.User;
using Domain.Model;
using Domain.Model.User;
using WebPage.Areas.Control.Models;
using WebPage.Filter;

namespace WebPage.Areas.Control.Controllers
{


    [Export]
    [AdminAuthor]
    public class UserController : Controller
    {
        [Import]
        private IUserManager userManager;
        [Import]
        private IRoleManager roleManager;

        //
        // GET: /Control/User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        #region list
        public ActionResult GetList(string order, int offset, int limit, string search)
        {
            Paging<User> paging = new Paging<User>();
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
            userManager.FindPageList(paging, _order);
            var users = paging.Items.Select(u => { return new { u.ID, u.Sex, u.Username, u.Email, u.LastLoginIP, u.LastLoginTime, u.RegTime, Rolename = u.Role.Name }; });
            return Json(new { total = paging.TotalNumber, rows = users });
        }
        #endregion

        #region 删除
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            Response response = new Response();
            User user = userManager.Find(id);
            if (user == null)
            {
                response.Code = 0;
                response.Message = "该用户已经不存在！";
                return Json(response);
            }
            response = userManager.Delete(user);
            return Json(response);
        }

        #endregion


        #region 添加

        public async Task<ActionResult> AddUser()
        {
            List<Role> roles = await roleManager.FindList().ToListAsync();
            List<SelectListItem> select_roles = new List<SelectListItem>();
            foreach (var role in roles)
            {
                select_roles.Add(new SelectListItem() { Text = role.Name, Value = role.ID.ToString() });
            }
            ViewBag.select_roles = select_roles;
            return PartialView("AddUserPartialView");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUserViewModel addUserViewModel)
        {
            Response _resp = new Response() { Code = -1 };
            if (ModelState.IsValid)
            {

                //检查用户名
                if (userManager.HasUserName(addUserViewModel.Username))
                {
                    _resp.Code = 0;
                    _resp.Message = "用户名已存在";
                }
                //检查Email
                if (userManager.HasEmail(addUserViewModel.Email))
                {
                    _resp.Code = 0;
                    _resp.Message = string.IsNullOrEmpty(_resp.Message) ? "Email已存在" : _resp.Message + "\n Email已存在";
                }
                if (_resp.Code == 0) return Json(_resp);
                //  User _user = Mapper.Map<User>(addUserViewModel);

                User _user = new User();
                _user.RoleID = addUserViewModel.RoleID;
                _user.Username = addUserViewModel.Username;
                _user.Name = addUserViewModel.Name;
                _user.Sex = addUserViewModel.Sex;
                _user.Password = Security.SHA256(addUserViewModel.Password);
                _user.Email = addUserViewModel.Email;
                _user.RegTime = System.DateTime.Now;
                _resp = userManager.Add(_user);

            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);
            }

            return Json(_resp);
        }
        #endregion


        #region 修改

        public ActionResult Modify(int id)
        {
            //角色列表
            var _roles = new RoleManager().FindList();
            List<SelectListItem> _listItems = new List<SelectListItem>(_roles.Count());
            foreach (var _role in _roles)
            {
                _listItems.Add(new SelectListItem() { Text = _role.Name, Value = _role.ID.ToString() });
            }
            ViewBag.Roles = _listItems;
            //角色列表结束
            return PartialView(userManager.Find(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(int id, User _user_e)
        {
            Response _resp = new Response();
            var _user = userManager.Find(id);
            if (ModelState.IsValid)
            {
                if (_user == null)
                {
                    _resp.Code = 0;
                    _resp.Message = "用户不存在，可能已被删除，请刷新后重试";
                }
                else
                {
                    _user.Username = _user_e.Username;
                    _user.Email = _user_e.Email;
                    // _user.LastLoginIP = _user_e.LastLoginIP;
                    _user.Name = _user_e.Name;
                    // _user.LastLoginTime = _user_e.LastLoginTime;
                    _user.RoleID = _user_e.RoleID;
                    // _user.RegTime = _user_e.RegTime;
                    _user.Sex = _user_e.Sex;
                    if (_user.Password != Security.SHA256(_user_e.Password)) _user.Password = Security.SHA256(_user_e.Password);
                    _resp = userManager.Update(_user);
                }
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_resp);
        }
        #endregion

        #region 校验

        [HttpPost]
        public ActionResult CheckUserName(string UserName)
        {
            return Json(!userManager.HasUserName(UserName));
        }

        [HttpPost]
        public ActionResult CheckEmail(string Email)
        {
            return Json(!userManager.HasEmail(Email));
        }
        #endregion
    }
}