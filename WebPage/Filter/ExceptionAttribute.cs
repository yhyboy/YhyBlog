using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Log;
using Common.Others;
using log4net;

namespace WebPage.Filter
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ExceptionAttribute : HandleErrorAttribute
    {
        public static IExtLog log = ExtLogManager.GetLogger("dblog");
        public override void OnException(ExceptionContext filterContext)
        {

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (!filterContext.ExceptionHandled)
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                string msg = filterContext.Exception.Message +
                (string.IsNullOrEmpty(filterContext.Exception.InnerException.Message) ?
                             "" : filterContext.Exception.InnerException.Message);
                log.Error(Utils.GetIP(), CurrUser.UserName, "", controllerName + "/" + actionName, msg);
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                Response response = new Response();
                response.Code = 0;
                response.Message = "服务器出现异常,请联系管理员查看日志！";
                //filterContext.Result = new JsonResult() { Data = response };
                string re = "{Code:0,Message:'服务器出现异常,请联系管理员查看日志！'}";
                filterContext.HttpContext.Response.ContentType = "application/json";
                filterContext.HttpContext.Response.Write(re);
                filterContext.HttpContext.Response.Flush();
                filterContext.HttpContext.Response.End();
                //filterContext.HttpContext.Response.
            }
            else
            {
                base.OnException(filterContext);
                filterContext.HttpContext.Response.Redirect("/Error.html");
            }
        }
    }
}