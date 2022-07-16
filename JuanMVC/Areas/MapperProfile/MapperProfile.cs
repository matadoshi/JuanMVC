using AutoMapper;
using DomainModels.Models;
using JuanMVC.Areas.Admin.ViewModels;

namespace JuanMVC.Areas.ProfileMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductPostViewModel>().ReverseMap();
        }
    }
}
