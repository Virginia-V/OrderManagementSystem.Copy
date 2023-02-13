using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.Auth;
using OMS.Common.Exceptions;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AuthResultDto> LoginByEmailAsync(EmailDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return AuthResultDto.Failed(ErrorMessages.EmailInvalid);
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
                return AuthResultDto.Failed(ErrorMessages.EmailInvalid);
            //if (!user.EmailConfirmed)
            //    return AuthResultDto.Failed(ErrorMessages.EmailNotConfirmed);
            return AuthResultDto.Succeded(model.Email, _mapper.Map<UserInfoDto>(user));
        }

        public async Task<AuthResultDto> RegisterByEmailAsync(RegisterDto model)
        {
            var userExists = (await _userManager.FindByEmailAsync(model.Email)) != null;
            if (userExists)
                throw new ValidationException(ErrorMessages.EmailExists);
            var domainUser = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.Name.Split(' ').FirstOrDefault(),
                LastName = model.Name.Split(' ').LastOrDefault(),
                ContactInfo = model.ContactInfo
            };
            var result = await _userManager.CreateAsync(domainUser);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.First().Description);
            }

            await _userManager.AddPasswordAsync(domainUser, model.Password);
            return AuthResultDto.Succeded(domainUser.Email, _mapper.Map<UserInfoDto>(domainUser));
        }

        public Task<string> GetResetPasswordTokenAsync(string email)
        {
            return GetTokenFor(email, (user) => _userManager.GeneratePasswordResetTokenAsync(user));
        }

        public Task<string> GetEmailConfirmationTokenAsync(string email)
        {
            return GetTokenFor(email, (user) => _userManager.GenerateEmailConfirmationTokenAsync(user));
        }
        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await ValidateEmail(email);
            if (user == null)
                return false;
            var result = await _userManager.ConfirmEmailAsync(user, Base64UrlEncoder.Decode(token));
            return result.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(string email, string passwordToken, string newPassword)
        {
            var user = await ValidateEmail(email);
            if (user == null)
                return false;
            var result = await _userManager.ResetPasswordAsync(user, Base64UrlEncoder.Decode(passwordToken), newPassword);
            return result.Succeeded;
        }

        private async Task<string> GetTokenFor(string email, Func<User, Task<string>> tokenRequest)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userExists = user != null;
            if (!userExists)
                return string.Empty;
            if (user.IsSocialAccount)
                throw new ValidationException(ErrorMessages.SocialAccount);

            var token = await tokenRequest(user);
            return token;
        }

        private async Task<User> ValidateEmail(string email)
        {
            email = Base64UrlEncoder.Decode(email);
            var user = await _userManager.FindByEmailAsync(email);
            var userExists = user != null;
            if (!userExists)
                return null;
            if (user.IsSocialAccount)
                throw new ValidationException(ErrorMessages.SocialAccount);
            return user;
        }
    }
}
