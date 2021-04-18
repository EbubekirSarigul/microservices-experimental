using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Services;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Core.IntegrationEvents;
using Tournament.Data.Events;

namespace Tournament.Core.DomainEventHandlers
{
    public class TournamentDateChangedDomainEventHandler : INotificationHandler<TournamentDateChangedDomainEvent>
    {
        private readonly IntegrationEventService _integrationEventService;

        public TournamentDateChangedDomainEventHandler(IntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle(TournamentDateChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            TournamentDateChangedIntegrationEvent integrationEvent = new TournamentDateChangedIntegrationEvent(notification.Name, notification.Date, notification.PlayerList);

            await _integrationEventService.PublishEventAsync(integrationEvent, Constant.EventTopic_TournamentDateChanged);
        }
    }
}
