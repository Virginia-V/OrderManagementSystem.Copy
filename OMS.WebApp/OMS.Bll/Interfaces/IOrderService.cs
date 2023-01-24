using OMS.Common.Dtos.Orders;
using OMS.Common.Models.PagedRequest;

namespace OMS.Bll.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task<PaginatedResult<OrderDto>> GetPagedOrdersAsync(PagedRequest pagedRequest);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task AddOrderAsync(CreateOrderDto dto);
        Task DeleteOrderAsync(int id);
        Task<IEnumerable<OrderDto>> GetTodayOrdersAsync();
        Task<IEnumerable<SalesByCategoryDto>> GetSalesByProductCategoryAsync();

    }
}
