using MicroserviceTraining.Framework.Extensions;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.Services
{
    public class IntegrationEventService
    {
        private readonly ILogger<IntegrationEventService> _logger;
        private readonly IEventBus _eventBus;

        public IntegrationEventService(ILogger<IntegrationEventService> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task PublishEventAsync(IntegrationEvent @event, string topic)
        {
            try
            {
                _logger.LogInformation($"Integration event is beeing published. Topic:{topic}");
                await _eventBus.Publish(@event, topic);
                _logger.LogInformation($"Integration event is published. Topic:{topic}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Unhandled exception while publishing integration event. Topic:{topic}, Exception: {ex.ToString()}");
            }
        }
    }
}
