using OMS.Common.Dtos.PaymentTerms;

namespace OMS.Bll.Interfaces
{
    public interface IPaymentTermService
    {
        Task<IEnumerable<PaymentTermDto>> GetPaymentTermsAsync();
    }
}
