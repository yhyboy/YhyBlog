using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Infrastructure.Core.Base;

namespace Infrastructure.Core.Data
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        #region 属性
        #endregion

        #region 构造方法
        #endregion

        #region 查询

        #region 单个查询
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        T Find(TKey ID);
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> where);
        #endregion

        #region 多个查询

        IQueryable<T> FindList();
        /// <summary>
        /// 获得多个实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> where);

        IQueryable<T> FindList(Expression<Func<T, bool>> where, int number);

        IQueryable<T> FindList(Expression<Func<T, bool>> where, OrderParam orderParam);

        IQueryable<T> FindList(Expression<Func<T, bool>> where, OrderParam[] orderParams, int number);
        #endregion


        #endregion

        #region 单个实体 操作

        #region 添加
        /// <summary>
        /// 单个实体添加标记
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        #endregion

        #region 更新
        /// <summary>
        /// 单个实体更新标记
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        #endregion

        #region 删除
        /// <summary>
        /// 单个实体删除标记
        /// </summary>
        
        void Delete(T entity);

        void Delete(TKey id);


        void  Delete(Expression<Func<T, bool>> where);
        #endregion

        #endregion

        #region 多个实体 操作
        /// <summary>
        /// 批量添加标记
        /// </summary>
        /// <param name="entities">实体集</param>
        void Add(IEnumerable<T> entities);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Delete(IEnumerable<T> entities);
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Update(IEnumerable<T> entities);


        #endregion

        #region 其他
        /// <summary>
        /// 获得总数
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// 获得总数
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> where);

        #endregion

        #region page
        IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber);
        IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, OrderParam orderParam);
        IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, Expression<Func<T, bool>> where);
        IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, Expression<Func<T, bool>> where, OrderParam orderParam);
        IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, Expression<Func<T, bool>> where, OrderParam[] orderParams);
        IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, bool asc);
        #endregion

    }
}
