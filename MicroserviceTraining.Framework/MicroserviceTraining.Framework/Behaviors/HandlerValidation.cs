using FluentValidation;
using MediatR;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Behaviors
{
    public class HandlerValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public HandlerValidation(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
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

            return await next();
        }
    }
}
