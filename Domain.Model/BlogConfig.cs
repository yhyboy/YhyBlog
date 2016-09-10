using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class BlogConfig
    {
        /// <summary>
        /// 置顶博客ID
        /// </summary>
        [Display(Name = "置顶博客ID")]
        public int TopBlog { get; set; }

        /// <summary>
        /// 友情链接
        /// </summary>
        [Display(Name = "参考链接")]
        public List<Link> Links { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        [Display(Name = "个人简介")]
        public string MyInfo { get; set; }

    }

    public class Link
    {
        public string LinkName { get; set; }
        public string Href { get; set; }

    }
}
