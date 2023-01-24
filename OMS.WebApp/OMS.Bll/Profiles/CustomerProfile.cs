using AutoMapper;
using OMS.Common.Dtos.Customers;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(x => x.CustomerType, y => y.MapFrom(z => z.CustomerType.Type));

            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<Customer, UpdateCustomerDto>();
        }
    }
}
