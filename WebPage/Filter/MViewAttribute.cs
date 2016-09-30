using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Log;

namespace WebPage.Filter
{
    public class MViewAttribute : ActionFilterAttribute
    {
        IExtLog log = ExtLogManager.GetLogger("dblog");
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var Request = filterContext.HttpContext.Request;
            string ViewTime = DateTime.Now.ToString("yyyy-MM-dd");
            string ViewIp = Request.UserHostAddress;      
            //记录日志      
            log.Info(Utils.GetIP(), "一般访问者", "Home", "Index", "访问网站! 时间:" + ViewTime + "IP:" + ViewIp);


        }
    }
}