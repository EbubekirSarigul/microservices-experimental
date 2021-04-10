using MicroserviceTraining.Framework.Extensions;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka
{
    public class EventBusKafka : IEventBus
    {
        private readonly KafkaProducer _kafkaProducer;
        private readonly KafkaConsumer _kafkaConsumer;
        private readonly ISubscriptionManager _handlerManager;

        public EventBusKafka(KafkaProducer kafkaProducer, KafkaConsumer kafkaConsumer, ISubscriptionManager handlerManager)
        {
            _kafkaProducer = kafkaProducer;
            _kafkaConsumer = kafkaConsumer;
            _handlerManager = handlerManager;
        }

        public async Task Publish(IntegrationEvent @event, string topic)
        {
            var message = @event.ToJson();
            await _kafkaProducer.SendMessage(message, topic);
        }

        public async Task StartConsuming(CancellationToken cancellationToken)
        {
            await _kafkaConsumer.StartAsync(cancellationToken);
        }

        public void Subscribe<TEvent>(string topic) where TEvent : IntegrationEvent
        {
            _handlerManager.Subscribe<TEvent>(topic);
        }
    }
}
