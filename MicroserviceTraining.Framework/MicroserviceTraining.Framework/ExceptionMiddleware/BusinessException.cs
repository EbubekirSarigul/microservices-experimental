using System;
using System.Net;

namespace MicroserviceTraining.Framework.ExceptionMiddleware
{
    public class BusinessException : Exception
    {
        public HttpStatusCode _statusCode { get; set; }
        public string _errorCode { get; set; }
        public string _errorMessage { get; set; }

        public BusinessException(string errorCode, string errorMessage, HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
            _errorCode = errorCode;
            _errorMessage = errorMessage;
        }
    }
}
