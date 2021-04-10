using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.Services
{
    public class IntegrationEventService
    {
        private readonly IEventBus _eventBus;

        public IntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task PublishEventAsync(IntegrationEvent @event, string topic)
        {
            try
            {
                await _eventBus.Publish(@event, topic);
            }
            catch (Exception ex)
            {
                // todo : create alarm or log the failed event.
            }
        }
    }
}
