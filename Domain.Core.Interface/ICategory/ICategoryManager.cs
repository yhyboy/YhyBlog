using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Others;
using Domain.Model.Category;

namespace Domain.Core.Interface.ICategory
{
    public interface ICategoryManager : IBaseManager<Category,int>
    {

        /// <summary>
        /// 添加栏目【Code：2-常规栏目信息不完整，3-单页栏目信息不完整，4-链接栏目信息不完整，5-栏目类型不存在】
        /// </summary>
        /// <param name="category">栏目数据【包含栏目类型对应数据】</param>
        /// <returns></returns>
        Response Add(Model.Category.Category category);


        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="general">常规栏目信息</param>
        /// <returns></returns>
        Response Add(Model.Category.Category category, CategoryGeneral general);

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="page">单页栏目信息</param>
        /// <returns></returns>
        Response Add(Model.Category.Category category, CategoryPage page);

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="link">链接栏目信息</param>
        /// <returns></returns>
        Response Add(Model.Category.Category category, CategoryLink link);

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <param name="general">常规信息</param>
        /// <returns></returns>
        Response Update(Model.Category.Category category, CategoryGeneral general);

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <param name="page">单页信息</param>
        /// <returns></returns>
        Response Update(Model.Category.Category category, CategoryPage page);

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <param name="link">链接信息</param>
        /// <returns></returns>
        Response Update(Model.Category.Category category, CategoryLink link);

        /// <summary>
        /// 查找子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        List<Model.Category.Category> FindChildren(int id);


        /// <summary>
        /// 查找根栏目
        /// </summary>
        /// <returns></returns>
        List<Model.Category.Category> FindRoot();

        /// <summary>
        /// 查找栏目路径
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        List<Model.Category.Category> FindPath(int id);

        /// <summary>
        /// 查找列表
        /// </summary>
        /// <param name="number">返回数量【0-全部】</param>
        /// <param name="type">栏目类型【null 全部】</param>
        /// <param name="orderParams">【排序参数】</param>
        /// <returns></returns>
        List<Model.Category.Category> FindList(int number, CategoryType? type, OrderParam[] orderParams);

    }
}
