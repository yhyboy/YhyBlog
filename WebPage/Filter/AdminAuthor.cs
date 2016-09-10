using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Others;

namespace WebPage.Filter
{
    public class AdminAuthor : AuthorizeAttribute
    {
        //构造函数
        public AdminAuthor(string failControllerName = "Control/Admin", string failActionName = "Login")
        {
            _failControllerName = failControllerName;
            _failActionName = failActionName;
        }
        public string _failControllerName, _failActionName;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            base.AuthorizeCore(httpContext);
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //被添加AllowAnonymousAttribute特性的过滤器将不参加AuthorizationLoginFilter的验证
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            //为登陆页添加例外，其它页都自动在global.asax里添加到全局过滤器中，MVC3及以后版本支持它
            if (!skipAuthorization)
            {
                if (CurrUser.ExtInfo!="admin")
                {
                    filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { 
                     { "Action",_failActionName },
                     { "Controller", _failControllerName}, 
                     { "returnUrl", HttpContext.Current.Request.Url.ToString() } });

                    // filterContext.Result = new RedirectResult("~/Control/Admin/Login");
                }
            }
            base.OnAuthorization(filterContext);
        }
    }
}