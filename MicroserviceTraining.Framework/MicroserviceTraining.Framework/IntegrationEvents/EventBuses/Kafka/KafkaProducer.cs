using Confluent.Kafka;
using MicroserviceTraining.Framework.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(ILogger<KafkaProducer> logger, KafkaConfiguration configuration)
        {
            ProducerConfig producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.Servers,
                MessageTimeoutMs = 10000,
                Acks = Acks.Leader
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            _logger = logger;
        }

        public async Task SendMessage(string message, string topic)
        {
            _logger.LogDebug($"Integration event is beeing published. Topic:{topic}, Message:{message}");

            var kafkaMessage = new Message<Null, string>
            {
                Value = message
            };

            await _producer.ProduceAsync(topic, kafkaMessage);

            _logger.LogDebug($"Integration event is published. Topic:{topic}");
        }
    }
}
