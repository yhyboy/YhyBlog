using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Model.Category;

namespace Domain.Core.Interface.ICategory
{
    public interface ICategoryPageManager : IBaseManager<CategoryPage,int>
    {
        /// <summary>
        /// 删除单页栏目-根据栏目ID
        /// </summary>
        /// <param name="categoryID">栏目ID</param>
        /// <returns></returns>
        Response DeleteByCategoryID(int categoryID);
    }
}
