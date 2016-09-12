using System.Web;
using System.Web.Mvc;
using WebPage.Filter;

namespace WebPage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionAttribute());
        }
    }
}
