using OMS.Common.Dtos.PaymentStatuses;

namespace OMS.Bll.Interfaces
{
    public interface IPaymentStatusService
    {
        Task<IEnumerable<PaymentStatusDto>> GetPaymentStatusesAsync();
    }
}
