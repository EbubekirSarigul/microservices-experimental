using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}
