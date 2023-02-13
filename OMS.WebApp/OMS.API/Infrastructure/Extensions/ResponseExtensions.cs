namespace OMS.API.Infrastructure.Extensions
{
    public static class ResponseExtensions
    {
        public static void SetHttpOnlyCookie(this HttpResponse response, string cookie, string value, DateTimeOffset? expiryDate = null)
        {
            response.Cookies.Append(cookie, value, HttpOnlyOptions(expiryDate));
        }

        public static void SetNotHttpOnlyCookie(this HttpResponse response, string cookie, string value, DateTimeOffset? expiryDate = null)
        {
            response.Cookies.Append(cookie, value, Options(expiryDate));
        }

        public static void DeleteNotHttpOnlyCookie(this HttpResponse response, string cookie, DateTimeOffset? expiryDate = null)
        {
            response.Cookies.Delete(cookie, Options(expiryDate));
        }
        public static void DeleteHttpOnlyCookie(this HttpResponse response, string cookie, DateTimeOffset? expiryDate = null)
        {
            response.Cookies.Delete(cookie, HttpOnlyOptions(expiryDate));
        }

        private static CookieOptions Options(DateTimeOffset? expiryDate = null) => new CookieOptions
        {
            HttpOnly = false,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = expiryDate,
        };
        private static CookieOptions HttpOnlyOptions(DateTimeOffset? expiryDate = null) => new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = expiryDate
        };
    }
}
