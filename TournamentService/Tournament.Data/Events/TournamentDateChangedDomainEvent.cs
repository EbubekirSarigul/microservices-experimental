using MediatR;
using System;
using System.Collections.Generic;

namespace Tournament.Data.Events
{
    public class TournamentDateChangedDomainEvent : INotification
    {
        public string Name { get; }
        public string Date { get; }

        public IEnumerable<Guid> PlayerList { get; }

        public TournamentDateChangedDomainEvent(string name, string date, IEnumerable<Guid> playerList)
        {
            Name = name;
            Date = date;
            PlayerList = playerList;
        }
    }
}
