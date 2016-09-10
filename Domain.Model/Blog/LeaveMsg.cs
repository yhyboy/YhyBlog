using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Base;

namespace Domain.Model.Blog
{
    public class LeaveMsg:BaseEntity<int>
    {

        [Required(ErrorMessage = "{0}是必须的")]
        [Display(Name = "内容")]
        [StringLength(500, ErrorMessage = "长度必须少于{1}个字")]
        public string Msg { get; set; }


        [Required(ErrorMessage = "{0}是必须的")]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

 
        [Display(Name = "用户")]
        public virtual User.User User { get; set; }


    }
}
