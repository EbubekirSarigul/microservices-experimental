using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.Abstractions
{
    public interface IEventBus
    {
        Task Publish(IntegrationEvent @event, string topic);

        void Subscribe<TEvent>(string topic) where TEvent : IntegrationEvent;

        Task StartConsuming(CancellationToken cancellationToken);
    }
}
