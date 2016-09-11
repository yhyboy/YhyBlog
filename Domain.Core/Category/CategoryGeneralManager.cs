using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Core.Interface;
using Domain.Core.Interface.ICategory;
using Domain.Model.Category;

namespace Domain.Core.Category
{
 
    /// <summary>
    /// 常规栏目管理
    /// </summary>
   [Export(typeof(ICategoryGeneralManager))]
    public class CategoryGeneralManager : BaseManager<CategoryGeneral,int>,ICategoryGeneralManager
    {
        /// <summary>
        /// 删除常规栏目-根据栏目ID
        /// </summary>
        /// <param name="categoryID">栏目ID</param>
        /// <returns></returns>
        public Response DeleteByCategoryID(int categoryID)
        {
            return base.Delete(cg => cg.CategoryID == categoryID);
        }
    }
}
