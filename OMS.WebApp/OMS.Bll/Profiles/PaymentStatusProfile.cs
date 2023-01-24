using AutoMapper;
using OMS.Common.Dtos.PaymentStatuses;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class PaymentStatusProfile : Profile
    {
        public PaymentStatusProfile()
        {
            CreateMap<PaymentStatus, PaymentStatusDto>();
        }
    }
}
