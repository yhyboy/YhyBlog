using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 自定义配置
    /// </summary>
    public class CustomCon : ConfigurationSection
    {
        /// <summary>
        /// 博客所属
        /// </summary>
        [ConfigurationProperty("WebInfo", IsRequired = true)]
        public WebInfo WebInfo
        {
            get { return (WebInfo)this["WebInfo"]; }
        }
  
    }
}
