using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Model.Category;

namespace Domain.Core.Interface.ICategory
{
    public interface ICategoryGeneralManager : IBaseManager<CategoryGeneral,int>
    {
        /// <summary>
        /// 删除常规栏目-根据栏目ID
        /// </summary>
        /// <param name="categoryID">栏目ID</param>
        /// <returns></returns>
        Response DeleteByCategoryID(int categoryID);
    }
}
