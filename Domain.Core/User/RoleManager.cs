using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interface.IUser;
using Domain.Model.User;

namespace Domain.Core.User
{
        [Export(typeof(IRoleManager))]
  public  class RoleManager:BaseManager<Role,int> ,IRoleManager
    {

    }
}
