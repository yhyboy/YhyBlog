using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Model.User;

namespace Domain.Core.Interface.IUser
{
    public interface IUserManager : IBaseManager<User, int>
    {
        /// <summary>
        /// 检测用户名是否重复
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        bool HasUserName(string UserName);
        /// <summary>
        /// 检测邮箱是否重复
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        bool HasEmail(string Email);

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Response Login(string accounts, string password);
    }
}
