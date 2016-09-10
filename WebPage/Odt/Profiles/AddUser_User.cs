using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Common;
using Domain.Model.Blog;
using Domain.Model.User;
using WebPage.Areas.Control.Models;

namespace WebPage.Odt.Profiles
{
    public class AddUser_User:Profile
    {

        protected override void Configure()
        {
            var map = Mapper.CreateMap<AddUserViewModel, User>();
            map.ConstructUsing(s => new User
            {
                Name = s.Name,
                Username = s.Username,
                Sex = s.Sex,
                Email = s.Email,
                //Password = Security.SHA256(s.Password),

              //  RegTime=DateTime.Now,
                RoleID=s.RoleID,
                

            });
        }
    }
}