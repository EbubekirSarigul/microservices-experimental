using Basket.Core.Models;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Collections.Generic;

namespace Basket.Core.IntegrationEvents
{
    public class CheckoutIntegrationEvent : IntegrationEvent
    {
        public int TotalPrice { get; }

        public Guid PaymentId { get; }

        public Guid PlayerId { get; }

        public List<Tournament> Tournaments { get; }

        public CheckoutIntegrationEvent(int totalPrice, Guid playerId, List<Tournament> tournaments)
        {
            TotalPrice = totalPrice;
            PlayerId = playerId;
            PaymentId = Guid.NewGuid();
            Tournaments = tournaments;
        }
    }
}
