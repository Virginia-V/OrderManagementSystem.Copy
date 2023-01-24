using OMS.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/customerTypes")]
    public class CustomerTypesController : AppBaseController
    {
        private readonly ICustomerTypeService _customerTypeService;

        public CustomerTypesController(ICustomerTypeService customerTypeService, 
            ILogger<CustomerTypesController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _customerTypeService = customerTypeService;
        }

        [HttpGet]
        public Task<IActionResult> GetCustomerTypesAsync()
        {
            return HandleAsync(() => _customerTypeService.GetCustomerTypesAsync());
        }
    }
}
