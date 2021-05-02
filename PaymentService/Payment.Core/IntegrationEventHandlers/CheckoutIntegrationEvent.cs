using MediatR;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;

namespace Payment.Core.IntegrationEventHandlers
{
    public class CheckoutIntegrationEvent : IntegrationEvent, INotification
    {
        public int TotalPrice { get; set; }

        public Guid PaymentId { get; set; }
    }
}
