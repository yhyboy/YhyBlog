using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    public class BaseCache : IDisposable
    {
        protected string def_ip = string.Empty;
        protected int def_port = 0;
        protected string def_password = string.Empty;

        public BaseCache()
        {

        }

        public virtual void InitCache(string ip = "", int port = 0, string password = "")
        {

        }

        public virtual bool SetCache<T>(string key, T t, int timeOutMinute = 10) where T : class,new()
        {

            return false;
        }

        public virtual T GetCache<T>(string key) where T : class,new()
        {

            return default(T);
        }

        public virtual bool Remove<T>(string key)
        {

            return false;
        }

        public virtual bool FlushAll()
        {

            return false;
        }

        public virtual bool Any(string key)
        {

            return false;
        }

        public virtual void Dispose(bool isfalse)
        {

            if (isfalse)
            {


            }
        }

        //手动释放
        public void Dispose()
        {

            this.Dispose(true);
            //不自动释放
            GC.SuppressFinalize(this);
        }
    }
}
