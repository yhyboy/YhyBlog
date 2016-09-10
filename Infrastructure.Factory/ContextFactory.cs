using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Infrastructure.Factory
{
  public  class ContextFactory
    {
        /// <summary>
        /// 获取当前线程的数据上下文
        /// </summary>
        /// <returns>数据上下文</returns>
        public static YhContext CurrentContext()
        {
            YhContext _nContext = CallContext.GetData("yhcontext") as YhContext;
            if (_nContext == null)
            {
                _nContext = new YhContext();
                CallContext.SetData("yhcontext", _nContext);
            }
            return _nContext;
        }
    }
}
