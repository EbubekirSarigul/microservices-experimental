using MicroserviceTraining.Framework.Models;

namespace Payment.Core.Commands.Payment
{
    public class PaymentResult : BaseResult
    {
        public string OrderId { get; set; }
    }
}
