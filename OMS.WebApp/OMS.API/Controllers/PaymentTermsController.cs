using OMS.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/paymentTerms")]
    public class PaymentTermsController : AppBaseController
    {
        private readonly IPaymentTermService _paymentTermService;

        public PaymentTermsController(IPaymentTermService paymentTermService, 
            ILogger<PaymentTermsController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _paymentTermService = paymentTermService;
        }

        [HttpGet]
        public Task<IActionResult> GetPaymentTermsAsync()
        {
            return HandleAsync(() => _paymentTermService.GetPaymentTermsAsync());
        }
    }
}
