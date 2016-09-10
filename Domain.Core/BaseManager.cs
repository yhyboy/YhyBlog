using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Core.Interface;
using Domain.Model;
using Infrastructure.Core.Base;
using Infrastructure.Core.Data;
using Infrastructure.Core.Data.Entity;
using Infrastructure.Factory;

namespace Domain.Core
{
   
  public abstract class BaseManager<T,Tkey>:IBaseManager<T,Tkey> where T:BaseEntity<Tkey>
    {

      protected IRepository<T,Tkey> Repository { get; set; }
      protected IUnitOfWork IUnitOfWork =ContextFactory.CurrentContext();

      protected BaseManager()
      {
          Repository = new BaseRepository<T, Tkey>(IUnitOfWork);
      }


      public virtual T Find(Tkey ID)
      {
          return Repository.Find(ID);
      }

      public virtual T Find(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return Repository.Find(where);
        }

      public virtual IQueryable<T> FindList()
        {
            return Repository.FindList();
        }

      public virtual Response Add(T entity)
        {
            Response _response = new Response();
            Repository.Add(entity);
            if (IUnitOfWork .Commit()> 0)
            {
                _response.Code = 1;
                _response.Message = "添加数据成功！";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "添加数据失败！";
            }

            return _response;
        }

      public virtual Response Update(T entity)
        {
            Response _response = new Response();
            Repository.Update(entity);
            if (IUnitOfWork.Commit() > 0)
            {
                _response.Code = 1;
                _response.Message = "更新数据成功！";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "更新数据失败！";
            }

            return _response;
        }

      public virtual Response Delete(Tkey id)
        {
            Response _response = new Response();
            var _entity = Find(id);
            if (_entity == null)
            {
                _response.Code = 10;
                _response.Message = "ID为【" + id + "】的记录不存在！";
            }
            else
            {
                Repository.Delete(_entity);
                if (IUnitOfWork.Commit() > 0)
                {
                    _response.Code = 1;
                    _response.Message = "删除数据成功！";
                }
                else
                {
                    _response.Code = 0;
                    _response.Message = "删除数据失败！";
                }
            }
            return _response;
        }

      public virtual Response Delete(T entity)
        {
            Response _response = new Response();
            Repository.Delete(entity);
            if (IUnitOfWork.Commit() > 0)
            {
                _response.Code = 1;
                _response.Message = "删除成功";
            }
            else
            {
                _response.Code = 0;
                _response.Message = "删除失败";
            }
            return _response;
        }

      public virtual Response Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            Response _response = new Response();
            Repository.Delete(where);
            if (IUnitOfWork.Commit() > 0)
            {
                _response.Code = 1;
                _response.Message = "删除成功";
            }
            else
            {
                _response.Code = 0;
                _response.Message = "删除失败";
            }
            return _response;
        }

      public virtual Paging<T> FindPageList(Paging<T> paging)
        {
            paging.Items = Repository.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber).ToList();
            return paging;
        }


      public virtual Paging<T> FindPageList(Paging<T> paging,bool order)
      {
          paging.Items = Repository.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber,order).ToList();
          return paging;
      }


      public virtual int Count()
        {
            return Repository.Count();
        }

      public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Repository.Count(predicate);
        }
    }
}
