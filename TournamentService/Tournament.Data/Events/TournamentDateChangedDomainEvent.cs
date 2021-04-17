using MediatR;

namespace Tournament.Data.Events
{
    public class TournamentDateChangedDomainEvent : INotification
    {
        public string Name { get; }
        public string Date { get; }

        public TournamentDateChangedDomainEvent(string name, string date)
        {
            Name = name;
            Date = date;
        }
    }
}
