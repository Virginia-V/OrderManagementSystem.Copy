using OMS.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/orderTypes")]
    public class OrderTypesController : AppBaseController
    {
        private readonly IOrderTypeService _orderTypeService;

        public OrderTypesController(IOrderTypeService orderTypeService, 
            ILogger<OrderTypesController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _orderTypeService = orderTypeService;
        }

        [HttpGet]
        public Task<IActionResult> GetOrderTypesAsync()
        {
            return HandleAsync(() => _orderTypeService.GetOrderTypesAsync());
        }
    }
}
