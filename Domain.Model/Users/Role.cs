using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Base;

namespace Domain.Model.User
{
    /// <summary>
    /// 角色模型
    /// </summary>
    public class Role : BaseEntity<int>
    {


        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(1000, ErrorMessage = "{0}必须少于{1}个字符")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "说明")]
        public string Description { get; set; }

    }
}
