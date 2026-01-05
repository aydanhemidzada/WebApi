using AutoMapper;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.OrderItems;

namespace WebApiConfigurationn.Profiles
{
    public class OrderItemProfiles:Profile
    {
        public OrderItemProfiles()
        {
            CreateMap<CreateOrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<GetOrderItemDTO, OrderItem>().ReverseMap();
        }
    }
}
