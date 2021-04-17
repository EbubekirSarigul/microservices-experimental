using MediatR;
using System;

namespace Tournament.Data.Events
{
    public class TournamentEntryPriceChangedDomainEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Price { get; }

        public TournamentEntryPriceChangedDomainEvent(Guid id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
