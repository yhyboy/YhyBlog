using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebPage.Test;

namespace WebPage.Odt
{
    public class Test : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<Map1, Map2>().ForMember(_map2 => _map2.S2, opt => opt.MapFrom(o => o.S1) );
          //Mapper.CreateMap<Map1, Map2>().ForMember(_map2 => _map2.S3, dto => dto.Ignore());
           // Mapper.CreateMap<Map1, Map2>().ForMember(_map2 => _map2.S6, dto => dto.Ignore());
      
        }
    }
}