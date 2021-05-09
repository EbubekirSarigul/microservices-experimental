using MicroserviceTraining.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Tournament.Data.Events;

namespace Tournament.Data.Entities
{
    public class Tournament : Entity
    {
        private Tournament()
        {
        }

        public Tournament(string name, string date, int entryPrice, string address)
        {
            Name = name;
            Date = date;
            EntryPrice = entryPrice;
            Address = address;
            _participants = new List<Participant>();
            AddDomainEvent(new NewTournamentAddedDomainEvent(Name, Date, entryPrice, address));
        }

        public string Name { get; private set; }

        public string Date { get; private set; }

        public int EntryPrice { get; private set; }

        public string Address { get; private set; }

        // DDD: Participant cannot be added "outside of the aggregate root which is tournament"
        private readonly List<Participant> _participants;

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void SetPrice(int entryPrice)
        {
            if (entryPrice != EntryPrice)
            {
                AddDomainEvent(new TournamentEntryPriceChangedDomainEvent(Id, Name, entryPrice));
            }

            EntryPrice = entryPrice;
        }

        public void SetDate(string date)
        {
            if (date != Date)
            {
                var playerList = Participants.Select(x => x.Id);
                AddDomainEvent(new TournamentDateChangedDomainEvent(Name, date, playerList));
            }

            Date = date;
        }
        
        public void AddParticipant(Guid playerId)
        {
            var participant = new Participant(playerId, Id);
            _participants.Add(participant);
        }
    }
}
