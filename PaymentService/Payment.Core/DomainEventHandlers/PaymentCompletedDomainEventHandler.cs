using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Services;
using Payment.Core.IntegrationEvents;
using Player.Data.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Core.DomainEventHandlers
{
    public class PaymentCompletedDomainEventHandler : INotificationHandler<PaymentCompletedDomainEvent>
    {
        private readonly IntegrationEventService _integrationEventService;

        public PaymentCompletedDomainEventHandler(IntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle(PaymentCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            PaymentCompletedIntegrationEvent integrationEvent = new PaymentCompletedIntegrationEvent(notification.PlayerId, notification.Tournaments);

            await _integrationEventService.PublishEventAsync(integrationEvent, Constant.EventTopic_PaymentCompleted);
        }
    }
}
