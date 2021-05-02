using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;

namespace Tournament.Core.IntegrationEvents
{
    public class TournamentPriceChangedIntegrationEvent : IntegrationEvent
    {
        public Guid TournamentId { get; }
        public string Name { get; }
        public int Price { get; }

        public TournamentPriceChangedIntegrationEvent(Guid tournamentId, string name, int price)
        {
            TournamentId = tournamentId;
            Name = name;
            Price = price;
        }
    }
}
