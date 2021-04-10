using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.Enum;
using MicroserviceTraining.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.ExceptionMiddleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
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
                var exception = CreateExceptionModel(businessException._errorCode, businessException._errorMessage);
                await WriteException(httpContext, exception);
            }
            catch (InputException validationException)
            {
                var exception = CreateExceptionModel(validationException);
                await WriteException(httpContext, exception);
            }
            catch (Exception)
            {
                var exception = CreateExceptionModel(ErrorCodes.SERVER_ERROR.Value, "Unknown error");
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
                ErrorCode = ErrorCodes.VALIDATION_ERROR.Value,
                ErrorMessage = "Details in validation errors",
                ValidationErrors = validationException._validationErrors
            };
            return exception.ToJson();
        }

        private async Task WriteException(HttpContext httpContext, string exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json"; // todo: accept
            await httpContext.Response.WriteAsync(exception);
        }
    }
}
