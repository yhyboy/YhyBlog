using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Domain.Model.Blog;
using WebPage.Areas.Control.Models;
using WebPage.Test;

namespace WebPage.Odt.Profiles
{
    public class AddBlog_Blog : Profile
    {
        protected override void Configure()
        {
            var map = Mapper.CreateMap<AddBlogViewModel, Blog>();
            map.ConstructUsing(s => new Blog
            {
              Content=s.editorValue
            }); 
        }
    }
}