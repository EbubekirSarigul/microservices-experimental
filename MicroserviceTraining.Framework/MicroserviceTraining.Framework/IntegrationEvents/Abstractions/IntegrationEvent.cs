using System;

namespace MicroserviceTraining.Framework.IntegrationEvents.Abstractions
{
    public abstract class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
