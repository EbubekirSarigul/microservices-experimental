using MediatR;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using Payment.Core.Models;
using System;
using System.Collections.Generic;

namespace Payment.Core.IntegrationEventHandlers
{
    public class CheckoutIntegrationEvent : IntegrationEvent, INotification
    {
        public int TotalPrice { get; set; }

        public Guid PaymentId { get; set; }

        public Guid PlayerId { get; set; }

        public List<Tournament> Tournaments { get; set; }

    }
}
