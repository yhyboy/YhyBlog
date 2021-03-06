﻿using System;
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
    [Export(typeof(ICategoryPageManager))]
    public class CategoryPageManager : BaseManager<CategoryPage, int>, ICategoryPageManager
    {
        /// <summary>
        /// 删除单页栏目-根据栏目ID
        /// </summary>
        /// <param name="categoryID">栏目ID</param>
        /// <returns></returns>
        public Response DeleteByCategoryID(int categoryID)
        {
            return base.Delete(cp => cp.CategoryID == categoryID);
        }
    }
}
