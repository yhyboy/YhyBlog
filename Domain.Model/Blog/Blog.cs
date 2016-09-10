using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Base;

namespace Domain.Model.Blog
{
   public class Blog:BaseEntity<int>
    {
       [Required(ErrorMessage = "{0}是必须的")]
       [Display(Name = "博客标题")]
       [StringLength(50, ErrorMessage = "长度必须少于{1}个字")]
       public string Title { get; set; }

       [Required(ErrorMessage = "{0}是必须的")]
       [Display(Name = "描述")]
       [StringLength(1000, ErrorMessage = "长度必须少于{1}个字")]
       public string Summary { get; set; }
 
       [Required(ErrorMessage = "{0}是必须的")]
       public string Content  { get; set; }

       [Required(ErrorMessage = "{0}是必须的")]
       [Display(Name = "创建时间")]
       public DateTime CreateTime { get; set; }

       [Required(ErrorMessage = "{0}是必须的")]
       [Display(Name = "阅读量")]
       public int Volume { get; set; }

        [Display(Name = "是否发布")]
       public bool Publish { get; set; }

       /// <summary>
       /// 栏目
       /// </summary>
       public virtual Category.Category Category { get; set; }
  
    
       //public int CategoryId { get; set; }  //如果显示添加改建 便支持级联 此处不希望级联
    }
}
