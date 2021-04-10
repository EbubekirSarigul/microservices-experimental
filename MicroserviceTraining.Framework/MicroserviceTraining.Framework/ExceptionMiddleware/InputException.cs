using System;
using System.Collections.Generic;

namespace MicroserviceTraining.Framework.ExceptionMiddleware
{
    public class InputException : Exception
    {
        public readonly ICollection<ValidationError> _validationErrors;

        public InputException(ICollection<ValidationError> validationErrors)
        {
            _validationErrors = validationErrors;
        }
    }
}
