using Microsoft.AspNetCore.Builder;

namespace GeoGuardian.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionMiddleware>();
    }
}