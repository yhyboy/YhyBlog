using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Model.Blog;

namespace Domain.Core.Interface
{
 
    public interface IBlogManager : IBaseManager<Blog, int>
    {


        /// <summary>
        /// 分页 管理
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<Blog> Getlist(Paging<Blog> paging, string where = "");

        /// <summary>
        /// Home展示
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IQueryable<Blog> Getlist(Paging<Blog> paging, int categoryId = -1);

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Response AddBlog(Blog blog);


    }
}
