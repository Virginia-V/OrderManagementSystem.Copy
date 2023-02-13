using OMS.Bll.Interfaces;
using System.Text.Json.Serialization;

namespace OMS.Bll.Services.SocialAuth.Google
{
    class Auth : BaseSocialAuthPlatform<Auth>, ISocialResponse
    {
        protected override string BaseUrl => "https://oauth2.googleapis.com/";

        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("family_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("given_name")]
        public string LastName { get; set; }
        [JsonPropertyName("picture")]
        public string Picture { get; set; }

        protected override SocialPlatform Platform => SocialPlatform.Google;

        protected override string GetRequestArgs(string accessToken)
        {
            return $"tokeninfo?id_token={accessToken}";
        }
    }
}
