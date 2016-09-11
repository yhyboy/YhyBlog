using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Configuration;

namespace WebPage.Areas.Member.Models
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Remote("CheckUserNameR", "User",  HttpMethod = "Post", ErrorMessage = "用户名已存在")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "必须选择{0}")]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "必须选择{0}")]
        public string Name { get; set; }

        /// <summary>
        /// 性别【0-女，1-男，2-保密】
        /// </summary>
        [Required(ErrorMessage = "必须选择{0}")]
        [Range(0, 2, ErrorMessage = "{0}范围{1}-{2}")]
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 密码
        /// </summary>     
        [Required(ErrorMessage = "必须选择{0}")]
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "必须选择{0}")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "密码不一致")]
        [Display(Name = "确认密码")]
        public string PasswordConfirm { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Remote("CheckEmailR", "User",  HttpMethod = "Post", ErrorMessage = "邮箱已存在")]
        [Required(ErrorMessage = "必须选择{0}")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}