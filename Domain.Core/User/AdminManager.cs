using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Core.Interface.IUser;
using Domain.Model;

namespace Domain.Core.User
{
    [Export(typeof(IAdminManager))]
    public class AdminManager : BaseManager<Administrator, int>, IAdminManager
    {


        public Response Login(string accounts, string password)
        {
            Response _resp = new Response();
            var _admin = base.Repository.Find(a => a.Accounts == accounts);
            if (_admin == null)
            {
                _resp.Code = 2;
                _resp.Message = "帐号为:【" + accounts + "】的管理员不存在";
            }
            else if (_admin.Password == password)
            {
                _resp.Code = 1;
                _resp.Message = "验证通过";
            }
            else
            {
                _resp.Code = 3;
                _resp.Message = "密码错误";
            }
            return _resp;
        }

        #region 查找账号

        public Administrator Find(string Account)
        {
            return Repository.Find(a => a.Accounts == Account);
        }


        #endregion


        /// <summary>
        /// 帐号是否存在
        /// </summary>
        /// <param name="accounts">帐号[不区分大小写]</param>
        /// <returns></returns>
        public bool HasAccounts(string accounts)
        {
            return base.Repository.Count(a => a.Accounts.ToUpper() == accounts.ToUpper()) > 0;
        }

        #region 更新登录信息
        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="administratorID">主键</param>
        /// <param name="ip">IP地址</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Response UpadateLoginInfo(int administratorID, string ip, DateTime time)
        {
            Response _resp = new Response();
            var _admin = Find(administratorID);
            if (_admin == null)
            {
                _resp.Code = 0;
                _resp.Message = "该主键的管理员不存在";
            }
            else
            {
                _admin.LoginIP = ip;
                _admin.LoginTime = time;
                _resp = Update(_admin);
            }
            return _resp;
        }
        #endregion

        #region 添加admin
        /// <summary>
        /// admini  ADD
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns></returns>
        public override Response Add(Administrator administrator)
        {
            Response response = new Response();
            if (HasAccounts(administrator.Accounts))
            {
                response.Code = 0;
                response.Message = "帐号已存在";
            }
            else response = base.Add(administrator);

            return response;
        }
        #endregion

        #region 删除管理员
        /// <summary>
        /// 删除【批量】返回值Code：1-成功，2-部分删除，0-失败
        /// </summary>
        /// <param name="administratorIDList">管理员ID列表</param>
        /// <returns></returns>
        public Response Delete(List<int> administratorIDList)
        {
            Response _resp = new Response();
            int _totalDel = administratorIDList.Count;
            int _totalAdmin = Count();
            foreach (int i in administratorIDList)
            {
                if (_totalAdmin > 1)
                {
                    base.Repository.Delete(i);
                    _totalAdmin--;
                }
                else _resp.Message = "最少需保留1名管理员";
            }
            _resp.Data = IUnitOfWork.Commit();
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "名管理员";
            }
            else if (_resp.Data > 0)
            {
                _resp.Code = 2;
                _resp.Message = "成功删除" + _resp.Data + "名管理员";
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }
        #endregion


    }
}
