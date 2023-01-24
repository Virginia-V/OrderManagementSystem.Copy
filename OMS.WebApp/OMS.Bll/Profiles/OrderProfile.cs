using AutoMapper;
using OMS.Common.Dtos.Orders;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Customer, y => y.MapFrom(z => z.Customer.CompanyName))
                .ForMember(x => x.OrderType, y => y.MapFrom(z => z.OrderType.Type));
            CreateMap<CreateOrderDto, Order>();
            CreateMap<OrderedProductDto, OrderedProduct>();
        }
    }
}
