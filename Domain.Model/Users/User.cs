using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Blog;
using Infrastructure.Core.Base;

namespace Domain.Model.User
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class User : BaseEntity<int>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        //[Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "角色")]
        public int? RoleID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        /// <summary>
        /// 名称【可做昵称、真实姓名等】
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 性别【0-女，1-男，2-保密】
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [Range(0, 2, ErrorMessage = "{0}范围{1}-{2}")]
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "最后登录时间")]
        public Nullable<DateTime> LastLoginTime { get; set; }


        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Display(Name = "最后登录IP")]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "注册时间")]
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }
        /// <summary>
        /// 留言
        /// </summary>
       // public virtual ICollection<LeaveMsg> LeaveMsg { get; set; }

    }
}
