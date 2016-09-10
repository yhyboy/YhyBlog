using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Infrastructure.Core.Data;
using Infrastructure.Core.Data.Entity;
using WebPage.Test;
using System.ComponentModel.Composition.AttributedModel;
using System.Xml.Linq;
using AutoMapper;
using WebPage.Odt;

namespace WebPage.Controllers
{
    [Export]
    public class HomeController : Controller
    {
        [Import]
        IMefTest1 mefTest;
        public ActionResult Index()
        {
            //    IUnitOfWork yhContext = new YhContext();
            //    IRepository<Administrator, int> Repository = new BaseRepository<Administrator, int>(yhContext);

            //    Administrator administrator = new Administrator() {Accounts="yhyboy",CreateTime = DateTime.Now,LoginIP = "192.168.0.0.1",LoginTime = DateTime.Now,Password = "123456"};
            //    Repository.Add(administrator);
            //    yhContext.Commit();
            //测试
            //ViewBag.Msg = "Null";
            //ViewBag.Msg = mefTest.ShowTest();
            //测试 ODT

            //Map1 map1=new Map1(){S1="MAP1"};
            //Map2 map2 = new Map2(){S6="自定义"};
            //map2 = Mapper.Map<Map1,Map2>(map1, map2);
            //string s6 = map2.S6;
            //string s2 = map2.S2;
            // return Json("map2.s6=" + s6 + "    map2.s2=" + s2, JsonRequestBehavior.AllowGet);
            /// <summary>
            /// 当前程序根目录
            /// </summary>
            string RootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = RootPath + @"/Config/BlogConfig.xml";
            //加载
            XElement root = XElement.Load(path);
            string roots = "加载:" + root.ToString();
        
            var ele = root.Element("TopBlog");
            ele.SetValue("20");
            root.Save(path);  
            return Json(ele.Value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Ueditor()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpUeditor(string content)
        {
            return Json(content);
        }
    }
}