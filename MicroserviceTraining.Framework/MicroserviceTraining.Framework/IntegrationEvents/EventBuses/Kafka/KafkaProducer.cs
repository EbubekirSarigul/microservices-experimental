using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(KafkaConfiguration configuration)
        {
            ProducerConfig producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.Servers,
                MessageTimeoutMs = 10000,
                Acks = Acks.Leader
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task SendMessage(string message, string topic)
        {
            var kafkaMessage = new Message<Null, string>
            {
                Value = message
            };

            await _producer.ProduceAsync(topic, kafkaMessage);
        }
    }
}
