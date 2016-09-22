using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Core;
using Domain.Core.Category;
using Domain.Model;

namespace WebPage.Controllers
{
    public class LinqController : Controller
    {
        //
        // GET: /Linq/
        public ActionResult Index()
        {
            YhContext db = new YhContext();

            #region 基本Linq



            #endregion
            //几个点
            //JOIN
           

            var result1 = from b in db.Blogs
                        join c in db.Categories on  b.Category.Name equals c.Name     
                         select new { b .ID,b.Publish,栏目ID=c.ID};

            var result16 = db.Blogs.Join(db.Categories, b => b.Category.ID, c => c.ID, (b, c) => new{类型=c.Name,博客名称=b.Title}).ToList();
            //分组  注意泛型
            var result12 = db.Blogs.GroupBy(b => b.Category.Name).Select(a => new {aa=a.Select(bs=>bs.Category.Name),count=a.Count()}).ToList();
            var result13 = db.Blogs.GroupBy(b => b.Publish).Select(c => new {aa=c.Select(cs => cs.Publish)}).ToList();
            var result15 = db.Blogs.GroupBy(b => b.Volume).Select(group => new { Content = group.Select(bs => bs) }).ToList();
            var result14 = db.Blogs.GroupBy(b => b.Volume).Select(group => new { Title = group .Select(bs=>bs.Title)}).ToList();
           
            
            //聚合
            var result123 = (from b in db.Blogs
                select new {b.Category.Name}).Distinct();//.SUM.MAX...




            return Json(result16, JsonRequestBehavior.AllowGet);
        }
    }
}