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
    /// 栏目模型类
    /// </summary>
    public class Category:BaseEntity<int>
    {
 
        /// <summary>
        /// 栏目类型
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "栏目类型")]
        public CategoryType Type { get; set; }

        /// <summary>
        /// 父栏目ID
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "父栏目")]
        public int ParentID { get; set; }

        /// <summary>
        /// 父栏目路径格式【0,……,父栏目ID】
        /// </summary>
        [Display(Name = "父栏目路径")]
        public string ParentPath { get; set; }

        /// <summary>
        /// 深度【表示栏目所处层次，根栏目为0，依次类推】
        /// </summary>
        [Required()]
        [Display(Name = "深度")]
        public int Depth { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "必须少于{1}个字")]
        [Display(Name = "栏目名称")]
        public string Name { get; set; }

        /// <summary>
        /// 栏目说明
        /// </summary>
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "必须少于{1}个字")]
        [Display(Name = "栏目说明")]
        public string Description { get; set; }

        /// <summary>
        /// 栏目排序【同级栏目数字越小越靠前】
        /// </summary>
        [Display(Name = "栏目排序")]
        public int Order { get; set; }

        /// <summary>
        /// 打开方式
        /// </summary>
        [Display(Name = "打开方式")]
        public string Target { get; set; }
    }
}
