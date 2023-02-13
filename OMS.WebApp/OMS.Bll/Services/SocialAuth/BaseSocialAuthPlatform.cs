using OMS.Bll.Interfaces;
using OMS.Domain;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OMS.Bll.Services.SocialAuth
{
    abstract class BaseSocialAuthPlatform<TResponse> : ISocialAuthPlatform where TResponse : ISocialResponse
    {
        protected readonly HttpClient _client;
        protected BaseSocialAuthPlatform()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        protected abstract string BaseUrl { get; }
        protected abstract SocialPlatform Platform { get; }
        protected abstract string GetRequestArgs(string accessToken);
        public async Task<User> LoginAsync(string accessToken)
        {
            var result = await GetAsync(accessToken);
            if (result == null)
            {
                return null;
            }
            var email = !string.IsNullOrEmpty(result.Email) ? result.Email : result.FirstName + result.LastName + $"@{Platform}.com";
            return new User
            {
                Email = email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Picture = result.Picture,
                ContactInfo = !string.IsNullOrEmpty(result.Email) ? result.Email : Platform.ToString(),
                UserName = email,
                IsSocialAccount = true,
                EmailConfirmed = true
            };
        }

        protected async Task<TResponse> GetAsync(string accessToken)
        {
            var response = await _client.GetAsync(GetRequestArgs(accessToken));
            if (!response.IsSuccessStatusCode)
                return default;

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(result);
        }
    }

    interface ISocialResponse
    {
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string Picture { get; }
    }
}
