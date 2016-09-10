using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using AutoMapper;
using Domain.Model.Blog;
using WebPage.Areas.Control.Models;

namespace WebPage.Odt.Profiles
{
    public class ModifyBlog_Blog : Profile
    {
        protected override void Configure()
        {

          //var map=  Mapper.CreateMap<ModifyBlogViewModel, Blog>().ForMember(blog=>blog.ID,o=>o.Ignore()).ForMember(blog => blog.Content, o => o.MapFrom(_o=>_o.editorValue));
            var map = Mapper.CreateMap<ModifyBlogViewModel, Blog>().ForMember(blog => blog.ID, o => o.Ignore()).ForMember(blog=>blog.Content,o=>o.MapFrom(_o=>_o.editorValue));
            //会覆盖愿对象
            //map.ConstructUsing(s => new Blog
            //{
            //    Content = s.editorValue
            //});

            var map2 = Mapper.CreateMap<Blog, ModifyBlogViewModel>();
            map2.ConstructUsing(s => new ModifyBlogViewModel
            {
                editorValue = s.Content
            });
        }
    }
}