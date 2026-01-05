using AutoMapper;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Orders;

namespace WebApiConfigurationn.Profiles
{
    public class OrderProfiles:Profile
    {
        public OrderProfiles()
        {
            CreateMap<CreateOrderDTO ,Order>().ReverseMap();
            CreateMap<UpdateOrderDTO, Order>().ReverseMap();
            CreateMap<GetOrderDTO, Order>().ReverseMap();
        }
    }
}
