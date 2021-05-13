using Basket.Core.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Core.IntegrationEventHandlers.PaymentCompleted
{
    public class PaymentCompletedIntegrationEventHandler : INotificationHandler<PaymentCompletedIntegrationEvent>
    {
        private readonly IBasketRepository _basketRepository;

        public PaymentCompletedIntegrationEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(PaymentCompletedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteBasket(notification.PlayerId.ToString());
        }
    }
}
