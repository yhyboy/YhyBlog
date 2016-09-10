using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPage.Areas.Control.Models
{
    public class AddBlogViewModel
    {
        [Required(ErrorMessage = "{0}是必须的")]
        [Display(Name = "博客标题")]
        [StringLength(50, ErrorMessage = "长度必须少于{1}个字")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0}是必须的")]
        [Display(Name = "文章内容")]
        public string editorValue { get; set; }
        [Display(Name = "是否发布")]
        public bool Publish { get; set; }
        [Required(ErrorMessage = "{0}是必须的")]
        [Display(Name = "栏目ID")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "{0}是必须的")]
        [Display(Name = "描述")]
        [StringLength(1000, ErrorMessage = "长度必须少于{1}个字")]
        public string Summary { get; set; }
    }
}