using Castle.MicroKernel.Lifestyle;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Middleware
{
    public class ApiRequestMiddleware
    {
        private readonly ILogger<ApiRequestMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ApiRequestMiddleware(ILogger<ApiRequestMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogDebug($"Api request received. {httpContext.Request.Path} {httpContext.Request.Method} {httpContext.Request.QueryString.ToUriComponent()}");

            using (IOC.IocFacility.Container.BeginScope())
            {
                await _next(httpContext);
            }
        }
    }
}
