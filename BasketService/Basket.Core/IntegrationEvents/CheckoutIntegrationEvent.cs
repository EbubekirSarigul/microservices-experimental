using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;

namespace Basket.Core.IntegrationEvents
{
    public class CheckoutIntegrationEvent : IntegrationEvent
    {
        public int TotalPrice { get; }

        public Guid PaymentId { get; }

        public CheckoutIntegrationEvent(int totalPrice)
        {
            TotalPrice = totalPrice;
            PaymentId = Guid.NewGuid();
        }
    }
}
