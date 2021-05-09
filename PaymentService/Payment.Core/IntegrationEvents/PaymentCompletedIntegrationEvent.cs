using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Collections.Generic;

namespace Payment.Core.IntegrationEvents
{
    public class PaymentCompletedIntegrationEvent : IntegrationEvent
    {
        public Guid PlayerId { get; }

        public List<Guid> Tournaments { get; set; }

        public PaymentCompletedIntegrationEvent(Guid playerId, List<Guid> tournaments)
        {
            PlayerId = playerId;
            Tournaments = tournaments;
        }
    }
}
