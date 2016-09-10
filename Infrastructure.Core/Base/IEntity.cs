using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Base
{
    public interface IEntity<TKey>
    {
       /// <summary>
       /// 主键
       /// </summary>
         TKey ID { get; set; }
    }
}
