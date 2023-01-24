using AutoMapper;
using OMS.Common.Dtos.Invoices;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(x => x.PaymentTerm, y => y.MapFrom(z => z.PaymentTerm.PaymentTermsType))
                .ForMember(x => x.Discount, y => y.MapFrom(z => z.Discount.DiscountType))
                .ForMember(x => x.PaymentStatus, y => y.MapFrom(z => z.PaymentStatus.Status))
                .ForMember(x => x.PurchaseOrderNumber, y => y.MapFrom(z => z.Order.PurchaseOrderNumber));
            CreateMap<Invoice, OpenInvoiceDto>()
                .ForMember(x => x.Customer, y => y.MapFrom(z => z.Order.Customer.CompanyName))
                .ForMember(x => x.Balance, y => y.MapFrom(z => z.InvoicedAmount));
            CreateMap<CreateInvoiceDto, Invoice>();
        }
    }
}
