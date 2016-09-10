using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPage.Areas.Member.Models
{
    public class LoginModel_Me
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "用户名")]
        public string Username { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

    }
}