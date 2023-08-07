using AutoMapper;
using Order.Application.Commands.CreateOrder;
using Order.Application.Queries.GetOrders;
using Order.Domain.Entities;

namespace Order.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingOrder, GetOrdersOutput_Order>().ReverseMap();
        CreateMap<ShoppingOrder, CreateOrderCommand>().ReverseMap();
        CreateMap<ShoppingOrder, CreateOrderCommandOutput>().ReverseMap();
    }
}
