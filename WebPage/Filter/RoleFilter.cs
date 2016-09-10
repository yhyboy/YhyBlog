using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Others;

namespace WebPage.Filter
{
    public class RoleFilter : ActionFilterAttribute
    {
        private readonly string[] roles;
        private readonly string action;

        public RoleFilter(string action = "", params string[] roles)
        {
            this.roles = roles;
            this.action = action;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            bool IsPass = true;
            //限制角色
            if (roles.Length != 0)
            {
                IsPass = !string.IsNullOrEmpty(CurrUser.Role)
                    && (roles.Where(r => r.Contains(CurrUser.Role)).Count() > 0);
            }
            //限制权限
            if (!string.IsNullOrEmpty(action))
            {
                IsPass = action.Equals(CurrUser.Authority);
            }

            if (!IsPass)
            {
                filterContext.HttpContext.Response.Write("<Script lanuage='javascript'>alert('您没有此操作权限');</Script>");
                filterContext.HttpContext.Response.Write("<a href='/Home/Index'>返回主页</a>");
                filterContext.HttpContext.Response.End();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}