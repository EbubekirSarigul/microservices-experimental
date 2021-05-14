using System.Collections.Generic;

namespace MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka
{
    public class KafkaConfiguration
    {
        public string Servers { get; set; }

        public string ConsumerGroupId { get; set; }
    }
}
