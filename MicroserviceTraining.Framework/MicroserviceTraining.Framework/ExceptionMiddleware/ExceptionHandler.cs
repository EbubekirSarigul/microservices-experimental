using MicroserviceTraining.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.ExceptionMiddleware
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException businessException)
            {
                _logger.LogError($"Business exception: Code:{businessException._errorCode}, Message:{businessException._errorMessage}, Http status: {businessException._statusCode}");
                var exception = CreateExceptionModel(businessException._errorCode, businessException._errorMessage);
                await WriteException(httpContext, exception, httpStatusCode: businessException._statusCode);
            }
            catch (InputException validationException)
            {
                _logger.LogError($"Validation exception: {validationException._validationErrors.ToJson()}");
                var exception = CreateExceptionModel(validationException);
                await WriteException(httpContext, exception, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Unhandled exception: {ex.ToString()}");
                var exception = CreateExceptionModel("SERVER_ERROR", "Unknown error");
                await WriteException(httpContext, exception);
            }
        }

        private string CreateExceptionModel(string errorCode, string errorMessage)
        {
            var exception = new ExceptionModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
            return exception.ToJson();
        }

        private string CreateExceptionModel(InputException validationException)
        {
            var exception = new ExceptionModel
            {
                IncludeValidationErrors = true,
                ErrorCode = "VALIDATION_ERROR",
                ErrorMessage = "Details in validation errors",
                ValidationErrors = validationException._validationErrors
            };
            return exception.ToJson();
        }

        private async Task WriteException(HttpContext httpContext, string exception, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            httpContext.Response.StatusCode = (int)httpStatusCode;
            httpContext.Response.ContentType = "application/json"; // todo: accept
            await httpContext.Response.WriteAsync(exception);
        }
    }
}
