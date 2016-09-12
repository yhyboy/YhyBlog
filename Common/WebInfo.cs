using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WebInfo : ConfigurationElement
    {


       [ConfigurationProperty("BlogName", IsRequired = true)]
       public string BlogName
       {
           get { return this["BlogName"].ToString(); }
           set { this["BlogName"] = value; }
       }
       //[ConfigurationProperty("TopBlog", IsRequired = true)]
       //public int TopBlog
       //{
       //    get { return Convert.ToInt32(this["TopBlog"]); }
       //    set { this["TopBlog"] = value; }
       //}
       [ConfigurationProperty("CallMe", IsRequired = true)]
       public string CallMe
       {
           get { return this["CallMe"].ToString(); }
           set { this["CallMe"] = value; }
       }
    }
}
