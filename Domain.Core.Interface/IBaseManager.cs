using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Infrastructure.Core.Base;

namespace Domain.Core.Interface
{
    /// <summary>
    /// 单模型管理类接口  多模型可自己扩充方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    public interface IBaseManager<T, Tkey> where T : BaseEntity<Tkey>
    {
        T Find(Tkey ID);
        T Find(Expression<Func<T, bool>> where);
        IQueryable<T> FindList();
        Response Add(T entity);
        Response Update(T entity);
        Response Delete(Tkey id);
        Response Delete(T entity);
        Response Delete(Expression<Func<T,bool>> where);
        Paging<T> FindPageList(Paging<T> paging);
        Paging<T> FindPageList(Paging<T> paging, bool order);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);

    }
}
