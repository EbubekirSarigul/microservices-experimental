using MicroserviceTraining.Framework.Models;

namespace Basket.Core.Commands.Checkout
{
    public class CheckoutResult : BaseResult
    {
        public string PaymentId { get; set; }
    }
}
