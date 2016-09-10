using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using AutoMapper;
using Common.Others;
using Domain.Core;
using Domain.Core.Category;
using Domain.Core.Interface;
using Domain.Core.Interface.ICategory;
using Domain.Model.Blog;
using Domain.Model.Category;
using Domain.Model.User;
using WebPage.Areas.Control.Models;
using WebPage.Filter;
using WebPage.Models;

namespace WebPage.Areas.Control.Controllers
{

    [Export]
    [AdminAuthor]
    public class BlogController : Controller
    {
        [Import]
        private IBlogManager blogManager;
        [Import]
        private ICategoryManager categoryManager;
        //
        // GET: /Control/Blog/
        public ActionResult Index()
        {
            return View();
        }
        #region 获取列表
        [HttpPost]
        public ActionResult GetList(string order, int offset, int limit, string search)
        {
            Paging<Blog> paging = new Paging<Blog>();
            bool _order = true;
            if (!string.IsNullOrEmpty(order))
            {
                if (order != "asc")
                {
                    _order = false;
                }
            }
            if (offset > 0)
            {
                paging.PageIndex = offset / limit + 1;
            }
            if (limit > 0)
            {
                paging.PageSize = limit;
            }
            if (string.IsNullOrEmpty(search))
            {
                paging.Items = blogManager.Getlist(paging, "").ToList();
            }
            else
            {
                paging.Items = blogManager.Getlist(paging, search).ToList();
            }
            var blogs = paging.Items.Select(b => { return new { b.ID, b.Title, Type = b.Category.Name, b.Volume, b.CreateTime, b.Publish }; });
            return Json(new { total = paging.TotalNumber, rows = blogs });

        }
        #endregion

        #region 添加文章
        public ActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBlog(AddBlogViewModel addBlogViewModel)
        {
            Response response = new Response() { Code = 0 };
            Blog blog;
            if (ModelState.IsValid)
            {
                var category = categoryManager.Find(addBlogViewModel.CategoryId);
                if (category == null || category.Type != CategoryType.General)
                {
                    response.Message = "栏目不匹配或者未找到！";
                    return Json(response);
                }
                if (blogManager.Find(b => b.Title == addBlogViewModel.Title) != null)
                {
                    response.Message = "标题重复了！";
                    return Json(response);
                }
                blog = Mapper.Map<Blog>(addBlogViewModel);
                blog.Category = category;
                response = blogManager.AddBlog(blog);
            }
            else
            {
                response.Message = "输入不合法！";
            }
            return Json(response);
        }
        #endregion

        #region 删除文章
        [HttpPost]
        public ActionResult DeleteBlog(int id)
        {
            Response response = blogManager.Delete(id);
            return Json(response);
        }
        #endregion

        #region 修改文章

        public ActionResult ModifyBlog(int id)
        {
            Blog blog = blogManager.Find(id);
            if (blog == null) return View("Prompt", new Prompt() { Title = "错误", Message = "文章不存在！" });
            //modifyBlogViewModel.ID = blog.ID;
            //modifyBlogViewModel.CategoryId = blog.Category.ID;
            //modifyBlogViewModel.editorValue = blog.Content;
            //modifyBlogViewModel.Title = blog.Title;
            //modifyBlogViewModel.Publish = blog.Publish;
            //modifyBlogViewModel.Summary = blog.Summary;
            ModifyBlogViewModel modifyBlogViewModel = Mapper.Map<ModifyBlogViewModel>(blog);
            return View(modifyBlogViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifyBlog(ModifyBlogViewModel modifyBlogViewModel)
        {
            Response response = new Response() { Code = 0 };
            Blog blog;
            if (ModelState.IsValid)
            {

                var category = categoryManager.Find(modifyBlogViewModel.CategoryId);
                if (category == null || category.Type != CategoryType.General)
                {
                    response.Message = "栏目不匹配或者未找到！";
                    return Json(response);
                }
                if (blogManager.Count(b => b.Title == modifyBlogViewModel.Title) > 1)
                {
                    response.Message = "标题重复了！";
                    return Json(response);
                }
                blog = blogManager.Find(modifyBlogViewModel.ID);
                if (blog == null)
                {
                    response.Message = "该文章不存在！";
                    return Json(response);
                }
                //在已有对象上赋值
                blog = Mapper.Map<ModifyBlogViewModel, Blog>(modifyBlogViewModel, blog);
                ////blog.Title = modifyBlogViewModel.Title;           
                ////blog.Publish = modifyBlogViewModel.Publish;
                ////blog.Content = modifyBlogViewModel.editorValue;
                ////blog.Summary = modifyBlogViewModel.Summary;
                blog.Category = category;
                response = blogManager.Update(blog);
            }
            else
            {
                response.Message = "输入不合法！";
            }
            return Json(response);
        }
        #endregion

    }
}