using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Core.Interface.ICategory;
using Domain.Core.Interface.IUser;

namespace Domain.Core.User
{
     [Export(typeof(IUserManager))]
  public  class UserManager:BaseManager<Model.User.User,int>,IUserManager
  {

      #region 用户名重复

      public bool HasUserName(string UserName)
      {
          if (!string.IsNullOrEmpty(UserName))
          {
          return Repository.Find(u => u.Username.Equals(UserName))!=null;
          }
          return false;
      }

      #endregion


      #region Email重复

      public bool HasEmail(string Email)
      {
          if (!string.IsNullOrEmpty(Email))
          {
              return Repository.Count(u => u.Email.Equals(Email))>0;
          }
          return false;
      }

      #endregion


#region 登录 
      public Response Login(string accounts, string password)
      {
          Response _resp = new Response();
          var user = base.Repository.Find(a => a.Username == accounts);
          if (user == null)
          {
              _resp.Code = 2;
              _resp.Message = "帐号为:【" + accounts + "】的用户不存在";
          }
          else if (user.Password == password)
          {
              _resp.Code = 1;
              _resp.Data = user;
              _resp.Message = "验证通过";

          }
          else
          {
              _resp.Code = 3;
              _resp.Message = "密码错误";
          }
          return _resp;
      }
#endregion
  }
}
