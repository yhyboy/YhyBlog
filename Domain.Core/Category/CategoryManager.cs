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
    [Export(typeof(ICategoryManager))]
    public class CategoryManager : BaseManager<Model.Category.Category, int>, ICategoryManager
    {


        #region 添加栏目
        /// <summary>
        /// 添加栏目【Code：2-常规栏目信息不完整，3-单页栏目信息不完整，4-链接栏目信息不完整，5-栏目类型不存在】
        /// </summary>
        /// <param name="category">栏目数据【包含栏目类型对应数据】</param>
        /// <returns></returns>
        public override Response Add(Model.Category.Category category)
        {
            return Add(category, new CategoryGeneral() { CategoryID = category.ID, ContentView = "Index", View = "Index" });
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="general">常规栏目信息</param>
        /// <returns></returns>
        public Response Add(Model.Category.Category category, CategoryGeneral general)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Add(category);
            general.CategoryID = category.ID;
            var _generalManager = new CategoryGeneralManager();
            _generalManager.Add(general);
            return _response;
        }
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="page">单页栏目信息</param>
        /// <returns></returns>
        public Response Add(Model.Category.Category category, CategoryPage page)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Add(category);
            page.CategoryID = category.ID;
            var _pageManager = new CategoryPageManager();
            _pageManager.Add(page);
            return _response;
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">基本信息</param>
        /// <param name="link">链接栏目信息</param>
        /// <returns></returns>
        public Response Add(Model.Category.Category category, CategoryLink link)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Add(category);
            link.CategoryID = category.ID;
            var _linkManager = new CategoryLinkManager();
            _linkManager.Add(link);
            return _response;
        }
        #endregion

        #region 更新栏目

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <param name="general">常规信息</param>
        /// <returns></returns>
        public Response Update(Model.Category.Category category, CategoryGeneral general)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Update(category);
            if (_response.Code == 1)
            {
                general.CategoryID = category.ID;
                var _generalManager = new CategoryGeneralManager();
                if (general.ID == 0) _response = _generalManager.Add(general);
                else _response = _generalManager.Update(general);
            }
            if (_response.Code == 1) _response.Message = "更新栏目成功！";
            return _response;
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <param name="page">单页信息</param>
        /// <returns></returns>
        public Response Update(Model.Category.Category category, CategoryPage page)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Update(category);
            if (_response.Code == 1)
            {
                page.CategoryID = category.ID;
                var _pageManager = new CategoryPageManager();
                if (page.ID == 0) _response = _pageManager.Add(page);
                else _response = _pageManager.Update(page);
            }
            if (_response.Code == 1) _response.Message = "更新栏目成功！";
            return _response;
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <param name="link">链接信息</param>
        /// <returns></returns>
        public Response Update(Model.Category.Category category, CategoryLink link)
        {
            Response _response = new Response() { Code = 1 };
            _response = base.Update(category);
            if (_response.Code == 1)
            {
                link.CategoryID = category.ID;
                var _linkManager = new CategoryLinkManager();
                if (link.ID == 0) _response = _linkManager.Add(link);
                else _response = _linkManager.Update(link);
            }
            if (_response.Code == 1) _response.Message = "更新栏目成功！";
            return _response;
        }

        #endregion


        /// <summary>
        /// 查找子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public List<Model.Category.Category> FindChildren(int id)
        {
            return Repository.FindList(c => c.ParentID == id, new OrderParam() { Method = OrderMethod.ASC, PropertyName = "Order" }).ToList();
        }

        /// <summary>
        /// 查找根栏目
        /// </summary>
        /// <returns></returns>
        public List<Model.Category.Category> FindRoot()
        {
            return FindChildren(0);
        }

        /// <summary>
        /// 查找栏目路径
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public List<Model.Category.Category> FindPath(int id)
        {
            List<Model.Category.Category> _categories = new List<Model.Category.Category>();
            var _category = Find(id);
            while (_category != null)
            {
                _categories.Insert(0, _category);
                _category = Find(_category.ParentID);
            }
            return _categories;
        }

        /// <summary>
        /// 查找列表
        /// </summary>
        /// <param name="number">返回数量【0-全部】</param>
        /// <param name="type">栏目类型【null 全部】</param>
        /// <param name="orderParams">【排序参数】</param>
        /// <returns></returns>
        public List<Model.Category.Category> FindList(int number, CategoryType? type, OrderParam[] orderParams)
        {
            List<Model.Category.Category> _categories;
            if (orderParams == null) orderParams = new OrderParam[] { new OrderParam() { Method = OrderMethod.ASC, PropertyName = "ParentPath" }, new OrderParam() { Method = OrderMethod.ASC, PropertyName = "Order" } };
            if (type == null) _categories = base.Repository.FindList(c => true, orderParams, number).ToList();
            else _categories = base.Repository.FindList(c => c.Type == type, orderParams, number).ToList();
            return _categories;

        }
    }
}
