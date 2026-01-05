using AutoMapper;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Products;

namespace WebApiConfigurationn.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles()
        {
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();


        }
    }
}
