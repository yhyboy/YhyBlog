using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Data
{
    public interface IUnitOfWork
    {

        #region 数据对象操作
        /// <summary>
        /// 提交事务
        /// </summary>
        int Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
        #endregion

        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        Task<int> CommitAsync();
    }
}
