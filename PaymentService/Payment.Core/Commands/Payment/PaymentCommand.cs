using MicroserviceTraining.Framework.Commands;

namespace Payment.Core.Commands.Payment
{
    public class PaymentCommand : BaseCommand<PaymentResult>
    {
        public string PaymentId { get; set; }
    }
}
