using AutoMapper;
using OMS.Common.Dtos.PaymentTerms;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class PaymentTermProfile : Profile
    {
        public PaymentTermProfile()
        {
            CreateMap<PaymentTerm, PaymentTermDto>();
        }
    }
}
