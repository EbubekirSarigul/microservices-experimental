using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Services;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Core.IntegrationEvents;
using Tournament.Data.Events;

namespace Tournament.Core.DomainEventHandlers
{
    public class NewTournamentAddedDomainEventHandler : INotificationHandler<NewTournamentAddedDomainEvent>
    {
        private readonly IntegrationEventService _integrationEventService;

        public NewTournamentAddedDomainEventHandler(IntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle(NewTournamentAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            TournamentAddedIntegrationEvent integrationEvent = new TournamentAddedIntegrationEvent(notification.Name, notification.Date, notification.Price, notification.Address);

            await _integrationEventService.PublishEventAsync(integrationEvent, Constant.EventTopic_TournamentAdded);
        }
    }
}
