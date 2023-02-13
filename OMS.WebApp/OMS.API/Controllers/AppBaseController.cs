using Microsoft.AspNetCore.Mvc;
using OMS.Common;
using Microsoft.Extensions.Localization;
using OMS.Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using OMS.Common.Exceptions;

namespace OMS.API.Controllers
{
    [ApiController]
    public abstract class AppBaseController : ControllerBase
    {
        private readonly ILogger _logger;
        protected readonly IStringLocalizer<Resource> _stringLocalizer;
        protected AppBaseController(ILogger logger, IStringLocalizer<Resource> stringLocalizer)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        protected async Task<IActionResult> HandleAsync<TResult>(Func<Task<TResult>> resultFunc)
        {
            try
            {
                var result = await resultFunc();
                return Ok(new ResponseDto<TResult> { Succeeded = true, Result = result });
            }
            catch (ValidationException ex)
            {
                return Ok(GetLocalizedError(ex.Message));
            }
            catch (Exception ex)
            {
                return GeneralExceptionResult(ex);
            }
        }

        protected async Task<IActionResult> HandleAsync(Func<Task> resultFunc)
        {
            try
            {
                await resultFunc();
                return Ok(new ResponseDto<object> { Succeeded = true });
            }
            catch (ValidationException ex)
            {
                return Ok(GetLocalizedError(ex.Message));
            }
            catch (Exception ex)
            {
                return GeneralExceptionResult(ex);
            }
        }

        private IActionResult GeneralExceptionResult(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, GetLocalizedError(ErrorMessages.GenericError));
        }

        private ResponseDto<object> GetLocalizedError(string message)
        {
            return new ResponseDto<object> { ErrorMessage = _stringLocalizer[message].Value, Succeeded = false };
        }
    }
}
