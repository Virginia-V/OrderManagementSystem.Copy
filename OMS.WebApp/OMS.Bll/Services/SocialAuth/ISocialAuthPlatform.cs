using OMS.Domain;

namespace OMS.Bll.Services.SocialAuth
{
    interface ISocialAuthPlatform
    {
        Task<User> LoginAsync(string accessToken);
    }
}
