using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common;
using Domain.Model;

namespace Domain.Core
{
    public class WebConfigManager
    {
        public static string RootPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string Path = RootPath + @"/Config/BlogConfig.xml";
        /// <summary>
        /// 获取配置类
        /// </summary>
        /// <returns></returns>
        public BlogConfig GetConfig()
        {
            BlogConfig blogConfig = new BlogConfig();
            //ID
            string TopBlog = ConfigHelper.GetXmlValueByName(Path, "TopBlog");
            int TopBlogId = -1;
            if (!string.IsNullOrEmpty(TopBlog))
            {
                TopBlogId = Convert.ToInt32(TopBlog);
            }
            blogConfig.TopBlog = TopBlogId;
            //MyInfo
            string MyInfo = ConfigHelper.GetXmlValueByName(Path, "MyInfo");
            blogConfig.MyInfo = MyInfo;
            //Link
            XElement xlink = ConfigHelper.GetXmlElement(Path, "Link");
            IEnumerable<XElement> xlinks = xlink.Elements("LinkItem");
            List<Link> links = new List<Link>();
            foreach (var item in xlinks)
            {
                links.Add(new Link() { Href = item.Element("Href").Value, LinkName = item.Element("LinkName").Value });
            }
            blogConfig.Links = links;
            return blogConfig;
        }


        public void SetConfig(string name,string value)
        {
            ConfigHelper.SetXmlValueByName(Path, name, value);

        }

    }
}
