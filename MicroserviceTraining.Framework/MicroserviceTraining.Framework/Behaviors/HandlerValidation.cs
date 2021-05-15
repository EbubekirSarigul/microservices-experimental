using FluentValidation;
using MediatR;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Behaviors
{
    public class HandlerValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<HandlerValidation<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public HandlerValidation(ILogger<HandlerValidation<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug($"Validating {typeof(TRequest).Name}");

            var validationFailures = _validators
                                        .Select(validator => validator.Validate(request))
                                        .SelectMany(validationResult => validationResult.Errors)
                                        .Where(validationFailure => validationFailure != null)
                                        .ToList();

            if (validationFailures.Any())
            {
                var validationErrors = validationFailures.Select(x => new ValidationError(x.ErrorCode, x.ErrorMessage, x.PropertyName));
                throw new InputException(validationErrors.ToList());
            }

            _logger.LogDebug($"Validated {typeof(TRequest).Name}");

            return await next();
        }
    }
}
