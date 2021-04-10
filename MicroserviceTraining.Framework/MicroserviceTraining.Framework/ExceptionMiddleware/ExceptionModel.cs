using System.Collections.Generic;

namespace MicroserviceTraining.Framework.ExceptionMiddleware
{
    public class ExceptionModel
    {
        public bool IncludeValidationErrors { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public ICollection<ValidationError> ValidationErrors { get; set; }
    }
}
