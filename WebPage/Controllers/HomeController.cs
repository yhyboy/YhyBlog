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
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using UnitTest;
using WebPage.Odt;

namespace WebPage.Controllers
{
    [Export]
    public class HomeController : Controller
    {
     public    static List<int> list = new List<int>() { 1, 2, 5, 9, 4, 7, 2, 6, 8, 2, 8, 7, 2 };   
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


            // throw  new Exception("666");


            // 当前程序根目录
          
            //string RootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //string path = RootPath + @"/Config/BlogConfig.xml";
            ////加载
            //XElement root = XElement.Load(path);
            //string roots = "加载:" + root.ToString();
        
            //var ele = root.Element("TopBlog");
            //ele.SetValue("20");
            //root.Save(path);  
           // return Json(ele.Value, JsonRequestBehavior.AllowGet);
   
            return RedirectToAction("Index","Home",new {Area="Member"});
        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";

//            return View();
//        }
//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }
//        public ActionResult Ueditor()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }
//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult UpUeditor(string content)
//        {
//            return Json(content);
//        }
//        [ActionName("action")]
//        public ActionResult dsadsa()
//        {
//            String sb="";
//            int temp = 0;
//            for (int i = list.Count; i >0; i--)
//            {
//                for (int j = 0; j < i-1; j++)
//                {
//                    if (list[j]>list[j+1])
//                    {
               
//                        temp = list[j];
//                        list[j] = list[j + 1];
//                        list[j + 1] = temp;
//                    }                  
//                }
//              sb+=  print()+@"/n";
//            }


//            return Content(print());

//        }
//        public string print()
//        {
//               StringBuilder sb = new StringBuilder();
//            list.ForEach(i => sb.Append("   "+i.ToString()));
//            return sb.ToString();
//        }
//        //发邮件测试
//        public ActionResult SendEmail()
//        {
//           //  //声明一个Mail对象
//           // MailMessage mymail = new MailMessage();
//           // //发件人地址
//           // //如是自己，在此输入自己的邮箱
//           // mymail.From = new MailAddress("18234139522@163.com");
//           // //收件人地址
//           // mymail.To.Add(new MailAddress("444967290@qq.com"));
//           // //邮件主题
//           // mymail.Subject = "测试（主题）";
//           // //邮件标题编码
//           // mymail.SubjectEncoding = System.Text.Encoding.UTF8;
//           // //发送邮件的内容
//           // mymail.Body ="<a href='#'>66<a/> <h2>H2<h2/>" ;
//           // //邮件内容编码
//           // mymail.BodyEncoding = System.Text.Encoding.UTF8;
//           // //添加附件
//           // //Attachment myfiles = new Attachment(tb_Attachment.PostedFile.FileName);
//           // //mymail.Attachments.Add(myfiles);
//           // //抄送到其他邮箱
//           //// mymail.CC.Add(new MailAddress(tb_cc.Text));
//           // //是否是HTML邮件
//           // mymail.IsBodyHtml = true;
//           // //邮件优先级
//           // mymail.Priority = MailPriority.High;
//           // //创建一个邮件服务器类
//           // SmtpClient myclient = new SmtpClient();
//           // myclient.Host = "SMTP.163.com"; 
//           // //SMTP服务端口
//           // myclient.Port = 25;
//           // //验证登录
//           // myclient.Credentials = new NetworkCredential("18234139522@163.com", "2047566yh");//"@"输入有效的邮件名, "*"输入有效的密码
//           // myclient.Send(mymail);



//            // 设置发送方的邮件信息,例如使用网易的smtp
//            string smtpServer = "smtp.163.com"; //SMTP服务器
//            string mailFrom = "18234139522@163.com"; //登陆用户名
//            string userPassword = "123456yh";//登陆密码

//            // 邮件服务设置
//            SmtpClient smtpClient = new SmtpClient();
//            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
//            smtpClient.Host = smtpServer; //指定SMTP服务器
//            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

////发送邮件设置        
//            MailMessage mailMessage = new MailMessage(mailFrom, "444967290@qq.com"); // 发送人和收件人
//            mailMessage.Subject = "zhuti";//主题
//            mailMessage.Body = "content";//内容
//            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
//            mailMessage.IsBodyHtml = true;//设置为HTML格式
//            mailMessage.Priority = MailPriority.Low;//优先级

//            try
//            {
//                smtpClient.Send(mailMessage); // 发送邮件

//            }
//            catch (SmtpException ex)
//            {

//            }
//           // int i = SendEmail("18234139522@163.com", "123456yh", new string[] { "444967290@qq.com" }, "标题", " mailMessage.BodyEncoding = Encoding.UTF8 这是测试中,zheshiceshi 689542dadasdasd邮件服务设置");


//            return Content("结果：");
//        }
//        ///<summary>
//        /// 发送邮件
//        ///</summary>
//        ///<param name="sendEmailAddress">发件人邮箱</param>
//        ///<param name="sendEmailPwd">发件人密码</param>
//        ///<param name="msgToEmail">收件人邮箱地址</param>
//        ///<param name="title">邮件标题</param>
//        ///<param name="content">邮件内容</param>
//        ///<returns>0：失败。1：成功！</returns>
//        public static int SendEmail(string sendEmailAddress, string sendEmailPwd, string[] msgToEmail, string title, string content)
//        {
//            //发件者邮箱地址
//            string fjrtxt = sendEmailAddress;
//            //发件者邮箱密码
//            string mmtxt = sendEmailPwd;
//            ////收件人收箱地址
//            //string sjrtxt = msgToEmail;
//            //主题
//            string zttxt = title;
//            //附件
//            //string fjtxt = fj.Text;
//            //内容
//            string nrtxt = content;
//            string[] fasong = fjrtxt.Split('@');
//            string[] fs = fasong[1].Split('.');
//            //发送
//            //设置邮件协议
//            SmtpClient client = new SmtpClient("smtp." + fs[0].ToString().Trim() + ".com");
//            client.UseDefaultCredentials = false;
//            //通过网络发送到Smtp服务器
//            client.DeliveryMethod = SmtpDeliveryMethod.Network;
//            //通过用户名和密码 认证
//            client.Credentials = new NetworkCredential(fasong[0].ToString(), mmtxt);
//            //发件人和收件人的邮箱地址
//            MailMessage mmsg = new MailMessage();
//            mmsg.From = new MailAddress(fjrtxt);
//            for (int i = 0; i < msgToEmail.Length; i++)
//            {
//                mmsg.To.Add(new MailAddress(msgToEmail[i]));
//            }
//            //邮件主题
//            mmsg.Subject = zttxt;
//            //主题编码
//            mmsg.SubjectEncoding = Encoding.UTF8;
//            //邮件正文
//            mmsg.Body = nrtxt;
//            //正文编码
//            mmsg.BodyEncoding = Encoding.UTF8;
//            //设置为HTML格式
//            mmsg.IsBodyHtml = true;
//            //优先级
//            mmsg.Priority = MailPriority.Low;
//            //if (fj.Text.Trim() != "")
//            //{
//            ////增加附件
//            //    mmsg.Attachments.Add(new Attachment(fj.Text));
//            //}
//            try
//            {
//                client.Send(mmsg);
//                return 1;
//            }
//            catch(Exception ex)
//            {
//                return 0;
//            }

//        }
//        delegate decimal pref(decimal amount);
//        public ActionResult Music()
//        {

           
         

//            return View();
//        }

        public ActionResult test()
        {
            UnitTest1 unitTest1 = new UnitTest1();

            unitTest1.TestMethod1();
            return Content("");
        }

    }
}