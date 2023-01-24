using OMS.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/paymentStatuses")]
    public class PaymentStatusesController : AppBaseController
    {
        private readonly IPaymentStatusService _paymentStatusService;

        public PaymentStatusesController(IPaymentStatusService paymentStatusService, 
            ILogger<PaymentStatusesController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _paymentStatusService = paymentStatusService;
        }
        [HttpGet]
        public Task<IActionResult> GetPaymentStatusesAsync()
        {
            return HandleAsync(() => _paymentStatusService.GetPaymentStatusesAsync());
        }
    }
}
