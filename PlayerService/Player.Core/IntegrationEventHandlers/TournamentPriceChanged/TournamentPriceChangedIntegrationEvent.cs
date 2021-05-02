using MediatR;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;

namespace Player.Core.IntegrationEventHandlers.TournamentPriceChanged
{
    public class TournamentPriceChangedIntegrationEvent : IntegrationEvent, INotification
    {
        public Guid TournamentId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return String.Format("Tournament price changed!!\n Tournament name: {0}, New Price: {1}",
                Name, Price);
        }
    }
}
