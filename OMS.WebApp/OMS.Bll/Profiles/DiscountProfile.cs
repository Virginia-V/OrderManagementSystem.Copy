using AutoMapper;
using OMS.Common.Dtos.Discounts;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountDto>();

        }
    }
}
