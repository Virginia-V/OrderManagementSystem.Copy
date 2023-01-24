using AutoMapper;
using OMS.Common.Dtos.CustomerTypes;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class CustomerTypeProfile : Profile
    {
        public CustomerTypeProfile()
        {
            CreateMap<CustomerType, CustomerTypeDto>()
                .ForMember(x => x.CustomerType, y => y.MapFrom(z => z.Type));
        }
    }
}
