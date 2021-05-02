using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Services;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Core.IntegrationEvents;
using Tournament.Data.Events;

namespace Tournament.Core.DomainEventHandlers
{
    public class TournamentEntryPriceChangedDomainEventHandler : INotificationHandler<TournamentEntryPriceChangedDomainEvent>
    {
        private readonly IntegrationEventService _integrationEventService;

        public TournamentEntryPriceChangedDomainEventHandler(IntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle(TournamentEntryPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            TournamentPriceChangedIntegrationEvent integrationEvent = new TournamentPriceChangedIntegrationEvent(notification.Id, notification.Name, notification.Price);

            await _integrationEventService.PublishEventAsync(integrationEvent, Constant.EventTopic_TournamentPriceChanged);
        }
    }
}
