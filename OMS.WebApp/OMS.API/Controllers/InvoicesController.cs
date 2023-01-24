using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Invoices;
using OMS.Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/invoices")]
    public class InvoicesController : AppBaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService,
            ILogger<InvoicesController> logger,
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public Task<IActionResult> GetInvoicesAsync()
        {
            return HandleAsync(() => _invoiceService.GetInvoicesAsync());
        }

        [HttpPost("paginated-search")]
        public Task<IActionResult> GetPagedInvoicesAsync(PagedRequest pagedRequest)
        {
            return HandleAsync(() => _invoiceService.GetPagedInvoicesAsync(pagedRequest));
        }

        [HttpGet("openInvoices")]
        public Task<IActionResult> GetOpenInvoicesAsync()
        {
            return HandleAsync(() => _invoiceService.GetOpenInvoicesAsync());
        }
        [HttpGet("overdueInvoices")]
        public Task<IActionResult> GetOverdueInvoicesAsync()
        {
            return HandleAsync(() => _invoiceService.GetOverdueInvoicesAsync());
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetInvoiceAsync(int id)
        {
            return HandleAsync(() => _invoiceService.GetInvoiceByIdAsync(id));
        }

        [HttpPost]
        public Task<IActionResult> CreateInvoiceAsync([FromBody] CreateInvoiceDto invoiceDto)
        {
            return HandleAsync(() => _invoiceService.AddInvoiceAsync(invoiceDto));
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteInvoiceAsync(int id)
        {
            return HandleAsync(() => _invoiceService.DeleteInvoiceAsync(id));
        }
    }
}
