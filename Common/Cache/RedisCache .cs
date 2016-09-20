using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace Common.Cache
{
 /// <summary>
     /// Redis缓存
     /// </summary>
     public class RedisCache : BaseCache
     {
         public RedisClient redis = null;
 
         public RedisCache()
         {
 
             //这里去读取默认配置文件数据
             def_ip = "127.0.0.1";
             def_port = 6379;
             def_password = "";
         }
 
         #region Redis缓存
 
         public override void InitCache(string ip = "", int port = 0, string password = "")
         {
 
             if (redis == null)
             {
                 ip = string.IsNullOrEmpty(ip) ? def_ip : ip;
                 port = port == 0 ? def_port : port;
                 password = string.IsNullOrEmpty(password) ? def_password : password;
                 redis = new RedisClient(ip, port, password);
             }
         }
 
         public override bool SetCache<T>(string key, T t, int timeOutMinute = 10)
         {
             var isfalse = false;
             try
             {
                 if (string.IsNullOrEmpty(key)) { return isfalse; }
 
                 InitCache();
                 isfalse = redis.Set<T>(key, t, TimeSpan.FromMinutes(timeOutMinute));
             }
             catch (Exception ex)
             {
             }
             finally { this.Dispose(); }
             return isfalse;
         }
 
         public override T GetCache<T>(string key)
         {
             var t = default(T);
             try
             {
                 if (string.IsNullOrEmpty(key)) { return t; }
 
                 InitCache();
                 t = redis.Get<T>(key);
             }
             catch (Exception ex)
             {
             }
             finally { this.Dispose(); }
             return t;
         }
 
         public override bool Remove<T>(string key)
         {
             var isfalse = false;
             try
             {
                 if (string.IsNullOrEmpty(key)) { return isfalse; }
                 InitCache();
                 isfalse = redis.Remove(key);
             }
             catch (Exception ex)
             {
             }
             finally { this.Dispose(); }
             return isfalse;
         }
 
         public override void Dispose(bool isfalse)
         {
 
             if (isfalse && redis != null)
             {
 
                 redis.Dispose();
                 redis = null;
             }
         }
 
         #endregion
     }
}
