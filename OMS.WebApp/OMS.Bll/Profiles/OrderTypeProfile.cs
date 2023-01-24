using AutoMapper;
using OMS.Common.Dtos.OrderTypes;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class OrderTypeProfile : Profile
    {
        public OrderTypeProfile()
        {
            CreateMap<OrderType, OrderTypeDto>();
        }
    }
}
