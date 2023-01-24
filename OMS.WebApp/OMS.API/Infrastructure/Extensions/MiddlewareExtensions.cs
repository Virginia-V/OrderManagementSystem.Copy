using OMS.API.Infrastructure.Middlewares;

namespace OMS.API.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbTransactionMiddleware>();
        }
    }
}
