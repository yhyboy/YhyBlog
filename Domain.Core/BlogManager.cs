using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Core.Category;
using Domain.Core.Interface;
using Domain.Model.Blog;

namespace Domain.Core
{
  [Export(typeof(IBlogManager))]
   public class BlogManager:BaseManager<Blog,int>,IBlogManager
    {

       /// <summary>
       /// 分页 管理
       /// </summary>
       /// <param name="paging"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public IQueryable<Blog> Getlist(Paging<Blog> paging,string where="")
       {
           IQueryable<Blog> _list = Repository.FindList();
           if (!string.IsNullOrEmpty(where))
           {
               _list = _list.Where(b => b.Title.Contains(where));
           }
           _list = _list.OrderByDescending(b => b.CreateTime);                         
           paging.TotalNumber  = _list.Count();
           return _list.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize);
       }
       /// <summary>
       /// Home展示
       /// </summary>
       /// <param name="paging"></param>
       /// <param name="categoryId"></param>
       /// <returns></returns>
       public IQueryable<Blog> Getlist(Paging<Blog> paging,int categoryId=-1)
       {
           CategoryManager categoryManager = new CategoryManager();
           Model.Category.Category category= categoryManager.Find(categoryId);         
           IQueryable<Blog> _list = Repository.FindList().Where(b=>b.Publish);
           if (category!=null)
           {
               _list = _list.Where(b => b.Category.ID == category.ID);
           }
           _list = _list.OrderByDescending(b => b.CreateTime);
           paging.TotalNumber = _list.Count();
           return _list.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize);
       }

       /// <summary>
       /// 添加文章
       /// </summary>
       /// <param name="blog"></param>
       /// <returns></returns>
       public Response AddBlog(Blog blog)
       {
           blog.CreateTime=DateTime.Now;
           blog.Volume = 0;
           return base.Add(blog);
       }
    }
}
