using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Orders;
using OMS.Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/orders")]
    public class OrdersController : AppBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public Task<IActionResult> GetOrdersAsync()
        {
            return HandleAsync(() => _orderService.GetOrdersAsync());
        }

        [HttpPost("paginated-search")]
        public Task<IActionResult> GetPagedOrdersAsync(PagedRequest pagedRequest)
        {
            return HandleAsync(() => _orderService.GetPagedOrdersAsync(pagedRequest));
        }

        [HttpGet("todayOrders")]
        public Task<IActionResult> GetTodayOrdersAsync()
        {
            return HandleAsync(() => _orderService.GetTodayOrdersAsync());
        }

        [HttpGet("salesByCategory")]
        public Task<IActionResult> GetSalesByProductCategoryAsync()
        {
            return HandleAsync(async () =>
            {
                var result = (await _orderService.GetSalesByProductCategoryAsync()).ToList();

                foreach (var item in result)
                    item.Category = _stringLocalizer[item.Category];

                return result;
            });
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetOrderAsync(int id)
        {
            return HandleAsync(() => _orderService.GetOrderByIdAsync(id));
        }

        [HttpPost]
        public Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto orderDto)
        {
            return HandleAsync(() => _orderService.AddOrderAsync(orderDto));
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteOrderAsync(int id)
        {
            return HandleAsync(() => _orderService.DeleteOrderAsync(id));
        }
    }
}
