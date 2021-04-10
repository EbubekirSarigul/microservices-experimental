namespace MicroserviceTraining.Framework.ExceptionMiddleware
{
    public class ValidationError
    {
        public ValidationError(string errorCode, string errorMessage, string fieldName)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            FieldName = fieldName;
        }
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string FieldName { get; set; }
    }
}
