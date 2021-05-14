using Castle.MicroKernel.Lifestyle;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Middleware
{
    public class ApiRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (IOC.IocFacility.Container.BeginScope())
            {
                await _next(httpContext);
            }
        }
    }
}
