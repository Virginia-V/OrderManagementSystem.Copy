using OMS.Common.Dtos.Invoices;
using OMS.Common.Models.PagedRequest;

namespace OMS.Bll.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetInvoicesAsync();
        Task<IEnumerable<OpenInvoiceDto>> GetOpenInvoicesAsync();
        Task<IEnumerable<OpenInvoiceDto>> GetOverdueInvoicesAsync();
        Task<PaginatedResult<InvoiceDto>> GetPagedInvoicesAsync(PagedRequest pagedRequest);
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task AddInvoiceAsync(CreateInvoiceDto dto);
        Task DeleteInvoiceAsync(int id);
    }
}
