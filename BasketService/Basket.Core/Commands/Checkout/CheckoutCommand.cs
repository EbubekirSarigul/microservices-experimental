using MicroserviceTraining.Framework.Commands;

namespace Basket.Core.Commands.Checkout
{
    public class CheckoutCommand : BaseCommand<CheckoutResult>
    {
        public string PlayerId { get; set; }
    }
}
