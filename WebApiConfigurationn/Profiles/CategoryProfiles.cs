using AutoMapper;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Categories;

namespace WebApiConfigurationn.Profiles
{
    public class CategoryProfiles:Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
           
            
        }
    }
}
