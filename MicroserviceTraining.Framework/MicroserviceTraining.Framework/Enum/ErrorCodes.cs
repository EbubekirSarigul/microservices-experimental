namespace MicroserviceTraining.Framework.Enum
{
    public class ErrorCodes
    {
        private ErrorCodes(string value)
        {
            Value = value;
        }

        public string Value;

        public static ErrorCodes SERVER_ERROR { get { return new ErrorCodes("SERVER_ERROR"); } }

        public static ErrorCodes VALIDATION_ERROR { get { return new ErrorCodes("VALIDATION_ERROR"); } }

        public static ErrorCodes VALIDATION_ERRORS { get { return new ErrorCodes("VALIDATION_ERRORS"); } }

        public static ErrorCodes TOURNAMENT_ALREADY_EXISTS { get { return new ErrorCodes("TOURNAMENT_ALREADY_EXISTS"); } }
    }
}
