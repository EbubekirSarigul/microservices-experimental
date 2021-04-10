namespace MicroserviceTraining.Framework.Models
{
    public abstract class BaseResult
    {
        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }
    }
}
