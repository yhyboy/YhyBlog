using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Base;

namespace Domain.Model.Category
{
    /// <summary>
    /// 单页栏目模型
    /// </summary>
    public class CategoryPage : BaseEntity<int>
    {


        /// <summary>
        /// 栏目ID
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "栏目ID")]
        public int CategoryID { get; set; }

        /// <summary>
        /// 栏目视图
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(255, ErrorMessage = "必须少于{1}个字")]
        [Display(Name = "栏目视图")]
        public string View { get; set; }

    }
}
