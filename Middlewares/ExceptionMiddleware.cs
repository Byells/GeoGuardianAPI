using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var pd = new ProblemDetails
                {
                    Title  = "Internal Server Error",
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                };
                pd.Extensions["traceId"] = ctx.TraceIdentifier;

                ctx.Response.ContentType = "application/problem+json";
                ctx.Response.StatusCode  = pd.Status.Value;
                var opts = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(pd, opts));
            }
        }
    }
}