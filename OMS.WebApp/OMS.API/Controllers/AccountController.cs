using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OMS.API.Infrastructure.Extensions;
using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Auth;
using OMS.Common.Exceptions;
using System.Security.Claims;

namespace OMS.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : AppBaseController
    {
        private readonly IAuthService _authService;
        private readonly ISocialAuthService _socialAuthService;
        private const string UserInfoCookie = "UserInfo";
        //private const string EmailConfirmedCookie = "EmailConfirmed";
        private readonly string _clientUrl;
        public AccountController(IAuthService authService,
                                 ISocialAuthService socialAuthService,
                                 IStringLocalizer<Resource> stringLocalizer,
                                 ILogger<AccountController> logger,
                                 //IEmailSender emailSender, 
                                 IConfiguration configuration)
            : base(logger, stringLocalizer)
        {
            _authService = authService;
            _socialAuthService = socialAuthService;
            //_emailSender = emailSender;
            _clientUrl = configuration["client"];
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login/external")]
        public Task<IActionResult> ExternalLogin([FromQuery] string accessToken, [FromQuery] SocialPlatform platform)
        {
            return AuthenticateAsync(() => _socialAuthService.LoginAsync(accessToken, platform));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("register/external")]
        public Task<IActionResult> ExternalRegistration([FromQuery] string accessToken, [FromQuery] SocialPlatform platform)
        {
            return AuthenticateAsync(() => _socialAuthService.RegisterAsync(accessToken, platform));
        }

        //[HttpPost]
        //[Route("requestreset/{email}")]
        //public Task<IActionResult> ResetPassword(string email)
        //{
        //    return HandleAsync(() =>
        //    {
        //        return SendAuthorizationEmail(
        //            () => _authService.GetResetPasswordTokenAsync(email),
        //            (token) => new ResetPasswordTemplate(email, new EmailWithLinkProps
        //            {
        //                ButtonTitle = _stringLocalizer["Reset Password"],
        //                Message = _stringLocalizer["To Reset Password, please click"],
        //                Link = _clientUrl + $"/reset?email={Base64UrlEncoder.Encode(email)}&passwordToken={Base64UrlEncoder.Encode(token)}"
        //            }));
        //    });
        //}

        //[HttpGet]
        //[Route("confirm/{email}")]
        //public async Task<IActionResult> Confirm(string email, string token)
        //{
        //    var confirmed = await _authService.ConfirmEmail(email, token);
        //    if (confirmed)
        //        Response.SetNotHttpOnlyCookie(EmailConfirmedCookie, confirmed.ToString(), DateTimeOffset.UtcNow.AddMinutes(5));
        //    return Redirect(_clientUrl);
        //}

        //[HttpGet]
        //[Route("resendConfirmation/{email}")]
        //public Task<IActionResult> ResendConfirmation(string email)
        //{
        //    var path = $"resendConfirmation/{email}";
        //    return HandleAsync(() => SendConfirmationEmail(email, path));
        //}

        //[HttpPost]
        //[Route("reset/{email}")]
        //public Task<IActionResult> ResetPassword(string email, string passwordToken, string newPassword)
        //{
        //    return HandleAsync(async () => {
        //        var reseted = await _authService.ResetPassword(email, passwordToken, newPassword);
        //        if (!reseted) throw new Exception("Couldn't Reset");
        //        var model = new EmailDto { Email = Base64UrlEncoder.Decode(email), Password = newPassword };
        //        var result = await AuthotificateTask(() => _authService.LoginByEmail(model));
        //        return result;
        //    });
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        public Task<IActionResult> EmailRegister([FromBody] RegisterDto model)
        {
            return HandleAsync(async () => {
                var result = await _authService.RegisterByEmailAsync(model);
                //await SendConfirmationEmail(model.Email, "register");
                return result;
            });
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public Task<IActionResult> EmailLogin([FromBody] EmailDto model)
        {
            return AuthenticateAsync(() => _authService.LoginByEmailAsync(model));
        }

        [HttpGet]
        [Route("logout")]
        public Task<IActionResult> Logout()
        {
            return HandleAsync(async () =>
            {
                Response.DeleteNotHttpOnlyCookie(UserInfoCookie, DateTimeOffset.UtcNow.AddDays(1));
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            });
        }

        //private Task SendConfirmationEmail(string email, string action)
        //{
        //    var redirectLink = string.Concat(new[] { Request.Scheme, "://", Request.Host.Value, Request.Path.Value.Replace(action, "confirm") });
        //    return SendAuthorizationEmail(
        //            () => _authService.GetEmailConfirmationTokenAsync(email),
        //            (token) => new ConfirmEmailTemplate(email, new EmailWithLinkProps
        //            {
        //                ButtonTitle = _stringLocalizer["Confirm"],
        //                Message = _stringLocalizer["To complete registration, please click"],
        //                Link = redirectLink + $"/{Base64UrlEncoder.Encode(email)}?token={Base64UrlEncoder.Encode(token)}"
        //            }));
        //}

        //private async Task SendAuthorizationEmail(Func<Task<string>> request, Func<string, IEmailDynamicTemplate> template)
        //{
        //    var token = await request();
        //    if (string.IsNullOrEmpty(token))
        //        return;
        //    await _emailSender.SendEmailFromDynamicTemplate(template(token));
        //}

        private Task<IActionResult> AuthenticateAsync(Func<Task<AuthResultDto>> resultFunc)
        {
            return HandleAsync(() => AuthenticateTask(resultFunc));
        }

        private async Task<AuthResultDto> AuthenticateTask(Func<Task<AuthResultDto>> resultFunc)
        {
            var result = await resultFunc();
            if (!result.Succeeded) throw new ValidationException(_stringLocalizer[result.Error].Value);
            //if (!result.EmailConfirmed) return result;
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, result.Email),
                    new Claim(ClaimTypes.Name, result.Name)
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            var ser = System.Text.Json.JsonSerializer.Serialize(result);
            Response.SetNotHttpOnlyCookie(UserInfoCookie, ser, DateTimeOffset.UtcNow.AddDays(1));
            return result;
        }
    }
}
