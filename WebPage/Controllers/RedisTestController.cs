using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Common.Cache;
using Domain.Model.User;
using ServiceStack.Redis;

namespace WebPage.Controllers
{
    public class RedisTestController : Controller
    {
        //
        // GET: /RedisTest/
        public ActionResult Index()
        {
            RedisClient Client = new RedisClient("127.0.0.1", 6379);
            Dictionary<string, string> redisDictionary = new Dictionary<string, string>() { };
            //1..String
            Client.Set("2015", "2015");
 
            redisDictionary.Add("MyKey1", Client.Get<string>("MyKey1"));
            redisDictionary.Add("6", Client.Get<string>("6"));
            //封装测试
            Domain.Model.User.User user = new Domain.Model.User.User() { Name = "张三", Password = "6666" };
            //插入类
            RedisCache redisCache = new RedisCache();
            redisCache.SetCache<User>("user", user, 2);
            redisDictionary.Add("user", ((User)redisCache.GetCache<User>("user")).Name);
            //去除
            redisCache.Remove<string>("MyKey1");
            ViewBag.value = redisDictionary;
            //2..HASH
            Client.SetEntryInHash("user1", "name", "LiSi");
            Client.SetEntryInHash("user1", "age", "18");
            Client.SetEntryInHash("user2", "age", "18");
            List<string> listh1 = Client.GetHashKeys("user1");
            List<string> listh2 = Client.GetHashValues("user1");//获取值  
            List<string> listh3 = Client.GetAllKeys();//获取所有的key。  

            List<List<string>> sList = new List<List<string>>();
            sList.Add(listh1);
            sList.Add(listh2);
            sList.Add(listh3);
            ViewBag.Hash = sList;
            //3..List

            //队列使用  
            Client.EnqueueItemOnList("push", "队列1");
            Client.EnqueueItemOnList("push", "队列2");
            Client.EnqueueItemOnList("push", "队列3");
            Client.EnqueueItemOnList("push", "队列4");
            Client.EnqueueItemOnList("push", "队列1");
            Client.EnqueueItemOnList("push", "队列2");
            //Client.EnqueueItemOnList("push2", "队列5");

            long listcount = Client.GetListCount("push");

            List<string> List_ = new List<string>();
            for (int i = 0; i < listcount; i++)
            {
                List_.Add(Client.DequeueItemFromList("push"));//出队 真的出队了
            }
            ViewBag.List = List_;

            List<string> List__ = new List<string>();
            Client.PushItemToList("栈1", "栈1");
            Client.PushItemToList("栈1", "栈2");
            Client.PushItemToList("栈1", "栈3");
            Client.PushItemToList("栈1", "栈4");
            Client.PushItemToList("栈1", "栈1");
            long count = Client.GetListCount("栈1");
            for (int i = 0; i < count; i++)
            {
                List__.Add(Client.PopItemFromList("栈1"));
            }
            ViewBag.List2 = List__;


            var user2 = Client.As<User>();
    
            user2.SetValue("user", new User() { Name = "张三2", Password = "66662" });
            return View();
        }

        public ActionResult Time1()
        {
            RedisClient Client = new RedisClient("127.0.0.1", 6379);
           // Client.Remove("时间测试");
            Client.Set<string>("时间测试", DateTime.Now.ToString(), TimeSpan.FromMinutes(1));
            return Content("创建时间:"+ Client.Get<string>("时间测试"));
        }

        public ActionResult Time2()
        {
            RedisClient Client = new RedisClient("127.0.0.1", 6379);
            return Content("获取元素：" + Client.Get<string>("时间测试"));
        }

    }
}