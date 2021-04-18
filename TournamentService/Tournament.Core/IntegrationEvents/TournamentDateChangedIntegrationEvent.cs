using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Collections.Generic;

namespace Tournament.Core.IntegrationEvents
{
    public class TournamentDateChangedIntegrationEvent : IntegrationEvent
    {
        public string Name { get; }
        public string Date { get; }

        public IEnumerable<Guid> PlayerList { get; }

        public TournamentDateChangedIntegrationEvent(string name, string date, IEnumerable<Guid> playerList)
        {
            Name = name;
            Date = date;
            PlayerList = playerList;
        }
    }
}
