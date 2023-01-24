using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Customers;
using OMS.Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : AppBaseController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService, 
            ILogger<CustomersController> logger, IStringLocalizer<Resource> stringLocalizer) 
            : base(logger, stringLocalizer)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public Task<IActionResult> GetCustomersAsync()
        {
            return HandleAsync(() => _customerService.GetCustomersAsync());
        }

        [HttpGet("newCustomers")]
        public Task<IActionResult> GetNewCustomersAsync()
        {
            return HandleAsync(() => _customerService.GetNewCustomersAsync());
        }

        [HttpPost("paginated-search")]
        public Task<IActionResult> GetPagedCustomersAsync(PagedRequest pagedRequest)
        {
            return HandleAsync(() => _customerService.GetPagedCustomersAsync(pagedRequest));
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetCustomerAsync(int id)
        {
            return HandleAsync(() => _customerService.GetCustomerByIdAsync(id));
        }

        [HttpPost]
        public Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerDto dto)
        {
            return HandleAsync(() => _customerService.AddCustomerAsync(dto));
        }

        [HttpPatch("{id}")]
        public Task<IActionResult> UpdateCustomerAsync(int id, [FromBody] UpdateCustomerDto dto)
        {
            return HandleAsync(() => _customerService.UpdateCustomerAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteCustomerAsync(int id)
        {
            return HandleAsync(() => _customerService.DeleteCustomerAsync(id));
        }
    }
}
