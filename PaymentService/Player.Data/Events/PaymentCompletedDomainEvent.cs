using MediatR;
using System;
using System.Collections.Generic;

namespace Player.Data.Events
{
    public class PaymentCompletedDomainEvent : INotification
    {
        public Guid PlayerId { get; }

        public List<Guid> Tournaments { get; }

        public PaymentCompletedDomainEvent(Guid playerId, List<Guid> tournaments)
        {
            PlayerId = playerId;
            Tournaments = tournaments;
        }
    }
}
