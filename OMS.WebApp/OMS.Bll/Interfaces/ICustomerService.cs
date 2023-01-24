using OMS.Common.Dtos.Customers;
using OMS.Common.Models.PagedRequest;

namespace OMS.Bll.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<IEnumerable<CustomerDto>> GetNewCustomersAsync();
        Task<PaginatedResult<CustomerDto>> GetPagedCustomersAsync(PagedRequest pagedRequest);
        Task<UpdateCustomerDto> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CreateCustomerDto dto);
        Task UpdateCustomerAsync(int id, UpdateCustomerDto dto);
        Task DeleteCustomerAsync(int id);
    }
}
