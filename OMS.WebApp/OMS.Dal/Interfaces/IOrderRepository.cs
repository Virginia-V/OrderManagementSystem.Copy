using OMS.Common.Dtos.Orders;
using OMS.Domain;

namespace OMS.Dal.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<SalesByCategoryDto>> GetSalesByProductCategoryAsync();

    }
}
