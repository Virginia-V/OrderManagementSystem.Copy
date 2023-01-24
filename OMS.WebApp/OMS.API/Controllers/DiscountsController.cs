using OMS.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/discounts")]
    public class DiscountsController : AppBaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService, ILogger<DiscountsController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public Task<IActionResult> GetDiscountsAsync()
        {
            return HandleAsync(() => _discountService.GetDiscountsAsync());
        }
    }
}
