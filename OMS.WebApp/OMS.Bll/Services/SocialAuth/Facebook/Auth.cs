using OMS.Bll.Interfaces;
using System.Text.Json.Serialization;

namespace OMS.Bll.Services.SocialAuth.Facebook
{
    class Auth : BaseSocialAuthPlatform<Auth>, ISocialResponse
    {
        private const string EMAIL_FIELD = "email";
        private const string FirstName_FIELD = "first_name";
        private const string LastName_FIELD = "last_name";
        private const string Picture_FIELD = "picture";
        private static string[] FIELDS = { EMAIL_FIELD, FirstName_FIELD, LastName_FIELD, Picture_FIELD };

        [JsonPropertyName(EMAIL_FIELD)]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName(FirstName_FIELD)]
        public string FirstName { get; set; }
        [JsonPropertyName(LastName_FIELD)]
        public string LastName { get; set; }
        public string Picture => PictureData.Data.Url;
        [JsonPropertyName(Picture_FIELD)]
        public PictureData PictureData { get; set; }

        protected override string BaseUrl => "https://graph.facebook.com/v10.0/";

        protected override SocialPlatform Platform => SocialPlatform.Facebook;

        protected override string GetRequestArgs(string accessToken)
        {
            return $"me?fields={string.Join(",", FIELDS)}&access_token={accessToken}";
        }
    }

    class PictureData
    {
        [JsonPropertyName("data")]
        public Properties Data { get; set; }
        public class Properties
        {
            [JsonPropertyName("height")]
            public int Height { get; set; }
            [JsonPropertyName("width")]
            public int Width { get; set; }
            [JsonPropertyName("is_silhouette")]
            public bool IsSilhouette { get; set; }
            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
