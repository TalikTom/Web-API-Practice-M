using AutoMapper;
using Practice.Model;
using Practice.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice.MVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChefModelDTO, ChefView>();
            CreateMap<ChefModelDTO, ChefDetailsView>();
            CreateMap<ChefDetailsView, ChefModelDTO>();
        }
    }
}