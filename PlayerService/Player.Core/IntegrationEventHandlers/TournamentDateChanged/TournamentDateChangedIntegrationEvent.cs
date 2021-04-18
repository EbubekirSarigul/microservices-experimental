using MediatR;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Player.Core.IntegrationEventHandlers.TournamentDateChanged
{
    public class TournamentDateChangedIntegrationEvent : IntegrationEvent, INotification
    {
        public string Name { get; set; }
        public string Date { get; set; }

        public IEnumerable<Guid> PlayerList { get; set; }

        public override string ToString()
        {
            return String.Format("Tournament data changed information!!\n Tournament name: {0}, New Date: {1}",
                Name, DateTime.ParseExact(Date, "yyyyMMdd", CultureInfo.InvariantCulture).Date);
        }
    }
}
