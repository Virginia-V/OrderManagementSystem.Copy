using OMS.Common.Dtos.Discounts;

namespace OMS.Bll.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountDto>> GetDiscountsAsync();
    }
}
