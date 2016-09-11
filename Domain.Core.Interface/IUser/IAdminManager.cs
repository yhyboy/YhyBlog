using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Model;

namespace Domain.Core.Interface.IUser
{
    public interface IAdminManager:IBaseManager<Administrator,int>
    {
        Response Login(string accounts, string password);

        Administrator Find(string Account);

        bool HasAccounts(string accounts);

        Response UpadateLoginInfo(int administratorID, string ip, DateTime time);

        Response Delete(List<int> administratorIDList);
    }

}
