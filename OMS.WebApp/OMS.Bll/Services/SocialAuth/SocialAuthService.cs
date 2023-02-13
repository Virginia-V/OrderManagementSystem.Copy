using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.Auth;
using OMS.Common.Exceptions;
using OMS.Domain;

namespace OMS.Bll.Services.SocialAuth
{
    public class SocialAuthService : ISocialAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public SocialAuthService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Task<AuthResultDto> LoginAsync(string accessToken, SocialPlatform platform)
        {
            return Execute(new ExecuteProps
            {
                AccessToken = accessToken,
                SocialAuthPlatform = GetPlatform(platform),
                Result = async (user) =>
                {
                    var domainUser = await _userManager.FindByEmailAsync(user.Email);
                    if (domainUser == null)
                        throw new ValidationException(ErrorMessages.UserDoesNotExists);

                    domainUser.Picture = user.Picture;
                    domainUser.FirstName = user.FirstName;
                    domainUser.LastName = user.LastName;
                    await _userManager.UpdateAsync(domainUser);
                    return AuthResultDto.Succeded(domainUser.Email, _mapper.Map<UserInfoDto>(domainUser));
                }
            });
        }

        public Task<AuthResultDto> RegisterAsync(string accessToken, SocialPlatform platform)
        {
            return Execute(new ExecuteProps
            {
                AccessToken = accessToken,
                SocialAuthPlatform = GetPlatform(platform),
                Result = async (user) =>
                {
                    var domainUser = await _userManager.FindByEmailAsync(user.Email);
                    if (domainUser != null)
                        throw new ValidationException(ErrorMessages.EmailExists);
                    var result = await _userManager.CreateAsync(user);
                    domainUser = user;
                    if (!result.Succeeded)
                    {
                        return AuthResultDto.Failed(result.Errors.First().Description);
                    }
                    return AuthResultDto.Succeded(domainUser.Email, _mapper.Map<UserInfoDto>(domainUser));
                }
            });
        }

        private static async Task<AuthResultDto> Execute(ExecuteProps executeProps)
        {
            var user = await executeProps.SocialAuthPlatform.LoginAsync(executeProps.AccessToken);
            if (user == null)
                return AuthResultDto.Failed(ErrorMessages.SocialLoginError);
            var result = await executeProps.Result(user);
            return result;
        }

        private static ISocialAuthPlatform GetPlatform(SocialPlatform socialPlatform)
        {
            return socialPlatform switch
            {
                SocialPlatform.Google => new Google.Auth(),
                SocialPlatform.Facebook => new Facebook.Auth(),
                _ => throw new ArgumentException($"There is no provider for {socialPlatform}"),
            };
        }

        private class ExecuteProps
        {
            public string AccessToken { get; set; }
            public ISocialAuthPlatform SocialAuthPlatform { get; set; }
            public Func<User, Task<AuthResultDto>> Result { get; set; }
        }
    }
}
