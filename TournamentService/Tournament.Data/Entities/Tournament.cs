using MicroserviceTraining.Framework.Data;
using System.Collections.Generic;
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
            AddDomainEvent(new NewTournamentAddedDomainEvent(Name, Date, entryPrice, address));
        }

        public string Name { get; private set; }

        public string Date { get; private set; }

        public int EntryPrice { get; private set; }

        public string Address { get; private set; }

        // DDD: Participant cannot be added "outside of the aggregate root which is tournament"
        private List<Participant> _participants { get; set; } = new List<Participant>();

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
                AddDomainEvent(new TournamentDateChangedDomainEvent(Name, date));
            }

            Date = date;
        }
    }
}
