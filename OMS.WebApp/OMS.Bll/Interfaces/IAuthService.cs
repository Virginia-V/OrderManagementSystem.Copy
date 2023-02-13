using OMS.Common.Dtos.Auth;

namespace OMS.Bll.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterByEmailAsync(RegisterDto model);
        Task<AuthResultDto> LoginByEmailAsync(EmailDto model);
        Task<string> GetEmailConfirmationTokenAsync(string email);
        Task<bool> ConfirmEmailAsync(string email, string token);
        Task<string> GetResetPasswordTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string passwordToken, string newPassword);
    }
}
