using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Others;
using Domain.Core.Category;
using Domain.Core.Content;
using Domain.Core.Interface.ICategory;
using Domain.Model.Category;
using WebPage.Filter;
using WebPage.Models;

namespace WebPage.Areas.Control.Controllers
{
   [Export]
   [AdminAuthor]
    public class CategoryController : Controller
    {
       [Import] 
        private ICategoryManager categoryManager;
        [Import] 
       private ICategoryGeneralManager _generalManager;
        public CategoryController()
        {
            //categoryManager = new CategoryManager();
        }
        #region 添加
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="id">父栏目ID</param>
        /// <returns></returns>
        public ActionResult Add(int? id)
        {
            Category _category = new Category() { ParentID = 0 };
            if (id != null && id > 0)
            {
                var _parent = categoryManager.Find((int)id);
                if (_parent != null && _parent.Type == CategoryType.General) _category.ParentID = (int)id;
            }
            return View(_category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Add(Category _category)
        {
           
            if (ModelState.IsValid)
            {
                //检查父栏目
                if (_category.ParentID > 0)
                {
                    var _parentCategory = categoryManager.Find(_category.ParentID);
                    if (_parentCategory == null) ModelState.AddModelError("ParentID", "父栏目不存在，请刷新后重新添加");
                    else if (_parentCategory.Type != CategoryType.General) ModelState.AddModelError("ParentID", "父栏目不允许添加子栏目");
                    else
                    {
                        _category.ParentPath = _parentCategory.ParentPath + "," + _parentCategory.ID;
                        _category.Depth = _parentCategory.Depth + 1;
                    }
                }
                else
                {
                    _category.ParentPath = "0";
                    _category.Depth = 0;
                }
                //栏目基本信息保存
                Response _response = new Response() { Code = 0, Message = "初始失败信息" };
                //根据栏目类型进行处理
                switch (_category.Type)
                {
                    case CategoryType.General:
                        var _general = new CategoryGeneral();
                        TryUpdateModel(_general);
                        _response = categoryManager.Add(_category, _general);
                        break;
                    case CategoryType.Page:
                        return View("Prompt", new Prompt() { Title = "暂未实现", Message = "暂未实现单页栏目添加" });
                        var _page = new CategoryPage();
                        TryUpdateModel(_page);
                        _response = categoryManager.Add(_category, _page);
                        break;
                    case CategoryType.Link:
                        return View("Prompt", new Prompt() { Title = "暂未实现", Message = "暂未实现外部链接添加" });
                        var _link = new CategoryLink();
                        TryUpdateModel(_link);
                        _response = categoryManager.Add(_category, _link);
                        break;
                }
                if (_response.Code == 1) return View("Prompt", new WebPage.Models.Prompt() { Title = "添加栏目成功", Message = "添加栏目【" + _category.Name + "】成功" });
                else return View("Prompt", new Prompt() { Title = "添加失败", Message = "添加栏目【" + _category.Name + "】时发生系统错误，未能保存到数据库，请重试" });
            }    
            return View(_category);
        }

        /// <summary>
        /// 常规栏目信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGeneral()
        {
            var _general = new CategoryGeneral() { ContentView = "Index", View = "Index" };
            List<SelectListItem> _contentTypeItems = new List<SelectListItem>() { new SelectListItem() { Value = "0", Text = "无", Selected = true } };
            ContentTypeManager _contentTypeManager = new ContentTypeManager();
            var _contentTypes = _contentTypeManager.FindList();
            foreach (var contentType in _contentTypes)
            {
                _contentTypeItems.Add(new SelectListItem() { Value = contentType.ID.ToString(), Text = contentType.Name });
            }
            ViewBag.ContentTypeItems = _contentTypeItems;
            return PartialView(_general);
        }

        /// <summary>
        /// 添加单页栏目 暂不实现
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPage()
        {
            var _page = new CategoryPage() { View = "Index" };
            return PartialView(_page);

        }
        /// <summary>
        /// 添加外部链接 暂不实现
        /// </summary>
        /// <returns></returns>
        public ActionResult AddLink()
        {
            var _link = new CategoryLink() { Url = "http://" };
            return PartialView(_link);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public ActionResult Modify(int id)
        {
            var _category = categoryManager.Find(id);
            if (_category == null) return View("Prompt", new Prompt() { Title = "错误", Message = "栏目不存在！" });
            return View(_category);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(int id, FormCollection form)
        {
            Category _category = categoryManager.Find(id);
            if (_category == null) return View("Prompt", new Prompt() { Title = "错误", Message = "栏目不存在！" });
            if (TryUpdateModel(_category, new string[] { "Type", "ParentID", "Name", "Description", "Order", "Target" }))
            {            

                //检查父栏目
                if (_category.ParentID > 0)
                {
                    var _parentCategory = categoryManager.Find(_category.ParentID);
                    if (_parentCategory == null) ModelState.AddModelError("ParentID", "父栏目不存在，请刷新后重新添加");
                    else if (_parentCategory.Type != CategoryType.General) ModelState.AddModelError("ParentID", "父栏目不允许添加子栏目");
                    else if (_parentCategory.ParentPath.IndexOf(_category.ParentPath) >= 0) ModelState.AddModelError("ParentID", "父栏目不能是其本身或其子栏目");
                    else
                    {
                        _category.ParentPath = _parentCategory.ParentPath + "," + _parentCategory.ID;
                        _category.Depth = _parentCategory.Depth + 1;
                    }
                }
                else
                {
                    _category.ParentPath = "0";
                    _category.Depth = 0;
                }
                //栏目基本信息保存
                Response _response = new Response() { Code = 0, Message = "初始失败信息" };
                //根据栏目类型进行处理
                switch (_category.Type)
                {
                    case CategoryType.General:                  
                        var _general = _generalManager.Find(g => g.CategoryID == id);
                        if (_general == null) _general = new CategoryGeneral() { CategoryID = id, View = "Index", ContentView = "Index" };
                        if (TryUpdateModel(_general, new string[] { "View", "ContentTypeID", "ContentView" })) _response = categoryManager.Update(_category, _general);
                        break;

                        //暂不实现
                    case CategoryType.Page:
                        return View("Prompt", new Prompt() { Title = "暂未实现", Message = "暂未实现单页栏目修改" });
                        var _pageManager = new CategoryPageManager();
                        var _page = _pageManager.Find(p => p.CategoryID == id);
                        if (_page == null) _page = new CategoryPage() { CategoryID = id, View = "index" };
                        if (TryUpdateModel(_page)) _response = categoryManager.Update(_category, _page);
                        break;
                    //暂不实现
                    case CategoryType.Link:
                        return View("Prompt", new Prompt() { Title = "暂未实现", Message = "暂未实现外部链接修改" });
                        var _linkManager = new CategoryLinkManager();
                        var _link = _linkManager.Find(l => l.CategoryID == id);
                        if (_link == null) _link = new CategoryLink() { CategoryID = id, Url = "http://" };
                        if (TryUpdateModel(_link)) _response = categoryManager.Update(_category, _link);
                        break;
                }
                if (ModelState.IsValid)
                {
                    if (_response.Code == 1) return View("Prompt", new Prompt() { Title = "修改栏目成功", Message = "修改栏目【" + _category.Name + "】成功" });
                    else return View("Prompt", new Prompt() { Title = "修改栏目失败", Message = "修改栏目【" + _category.Name + "】时发生系统错误，未能保存到数据库，请重试" });
                }
            }
            return View(_category);
        }

        /// <summary>
        /// 常规栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public ActionResult ModifyGeneral(int id)
        {
            List<SelectListItem> _contentTypeItems = new List<SelectListItem>() { new SelectListItem { Text = "无", Value = "0" } };
            ContentTypeManager _contentTypeManager = new ContentTypeManager();
            var _contentTypes = _contentTypeManager.FindList();
            foreach (var contentType in _contentTypes)
            {
                _contentTypeItems.Add(new SelectListItem() { Value = contentType.ID.ToString(), Text = contentType.Name });
            }
            ViewBag.ContentTypeItems = _contentTypeItems;
            var _generalManager = new CategoryGeneralManager();
            var _general = _generalManager.Find(g => g.CategoryID == id);
            if (_general == null) _general = new CategoryGeneral() { ContentView = "index", View = "index" };
            return PartialView(_general);
        }

        /// <summary>
        /// 单页栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public ActionResult ModifyPage(int id)
        {
            var _pageManager = new CategoryPageManager();
            var _page = _pageManager.Find(g => g.CategoryID == id);
            if (_page == null) _page = new CategoryPage() { View = "index" };
            return PartialView(_page);
        }

        /// <summary>
        /// 链接栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public ActionResult ModifyLink(int id)
        {
            var _linkManager = new CategoryLinkManager();
            var _link = _linkManager.Find(g => g.CategoryID == id);
            if (_link == null) _link = new CategoryLink() { Url = "http://" };
            return PartialView(_link);
        }


        #endregion

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Response _resp = new Response();
            var _category = categoryManager.Find(id);
            if (_category == null)
            {
                _resp.Code = 0;
                _resp.Message = "栏目不存在";
            }
            else
            {
                if (categoryManager.Count(c => c.ParentID == _category.ID) > 0)
                {
                    _resp.Code = 0;
                    _resp.Message = "该栏目栏目有子栏目，请先删除子栏目";
                }
                else
                {
                    switch (_category.Type)
                    {
                        case CategoryType.General:
                            new CategoryGeneralManager().DeleteByCategoryID(_category.ID);
                            break;
                        case CategoryType.Page:
                            new CategoryPageManager().DeleteByCategoryID(_category.ID);
                            break;
                        case CategoryType.Link:
                            new CategoryLinkManager().DeleteByCategoryID(_category.ID);
                            break;
                    }
                    _resp = categoryManager.Delete(_category);
                }
            }
            return Json(_resp);

        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 子栏目列表【暂时只返回Json格式】
        /// </summary>
        /// <param name="id">栏目ID.0-表示根栏目</param>
        /// <returns></returns>
        public ActionResult Children(int id)
        {
            return Json(categoryManager.FindChildren(id));

        }


        /// <summary>
        /// 树形节点数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Tree(bool showIcon = false)
        {
            List<TreeNode> _nodes = new List<TreeNode>();
            //栏目并进行排序使最深的节点排在最前
            var _categories = categoryManager.FindList(0, null, new OrderParam[] { new OrderParam() { Method = OrderMethod.ASC, PropertyName = "ParentPath" }, new OrderParam() { Method = OrderMethod.ASC, PropertyName = "Order" } });
            TreeNode _node;
            //遍历常规栏目
            foreach (var _category in _categories)
            {
                _node = new TreeNode() { pId = _category.ParentID, id = _category.ID, name = _category.Name, url = Url.Action("Modify", "Category", new { id = _category.ID }) };
                if (showIcon)
                {
                    switch (_category.Type)
                    {
                        case CategoryType.General:
                            _node.icon = Url.Content("~/Content/Images/folder.png");
                            break;
                        case CategoryType.Page:
                            _node.icon = Url.Content("~/Content/Images/page.png");
                            break;
                        case CategoryType.Link:
                            _node.icon = Url.Content("~/Content/Images/link.png");
                            break;
                    }
                }
                _nodes.Add(_node);
            }

            return Json(_nodes);
        }

        /// <summary>
        /// 下拉树【常规栏目】
        /// </summary>
        /// <returns></returns>
        public ActionResult DropdownTree()
        {
            //栏目并进行排序使最深的节点排在最前
            var _categories = categoryManager.FindList(0, CategoryType.General, new OrderParam[] { new OrderParam() { Method = OrderMethod.ASC, PropertyName = "ParentPath" }, new OrderParam() { Method = OrderMethod.ASC, PropertyName = "Order" } });
            List<TreeNode> _nodes = new List<TreeNode>();
            //遍历常规栏目
            foreach (var _category in _categories)
            {
                _nodes.Add(new TreeNode() { pId = _category.ParentID, id = _category.ID, name = _category.Name });
            }

            return Json(_nodes);
        }
	}
}