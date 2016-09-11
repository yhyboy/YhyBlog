using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Base;

namespace Infrastructure.Core.Data
{
    public interface IExtensionRepository<T, Tkey> : IRepository<T,Tkey> where T:BaseEntity<Tkey>
    {
    }
}
