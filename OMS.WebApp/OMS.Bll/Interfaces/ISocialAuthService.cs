using OMS.Common.Dtos.Auth;

namespace OMS.Bll.Interfaces
{
    public interface ISocialAuthService
    {
        Task<AuthResultDto> LoginAsync(string accessToken, SocialPlatform platform);
        Task<AuthResultDto> RegisterAsync(string accessToken, SocialPlatform platform);
    }
    public enum SocialPlatform
    {
        Facebook, Google
    }
}
