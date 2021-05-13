using MediatR;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Collections.Generic;

namespace Basket.Core.IntegrationEventHandlers.PaymentCompleted
{
    public class PaymentCompletedIntegrationEvent : IntegrationEvent, INotification
    {
        public Guid PlayerId { get; set; }

        public List<Guid> Tournaments { get; set; }
    }
}
