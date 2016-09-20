using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
   public class RedisConfig
    {
       /// <summary>
       /// IP地址
       /// </summary>
       public string Ip { get; set; }
       
       /// <summary>
       /// 端口号
       /// </summary>
       public int Port { get; set; }
    }
}
