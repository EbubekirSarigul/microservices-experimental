using MediatR;

namespace Tournament.Data.Events
{
    public class NewTournamentAddedDomainEvent : INotification
    {
        public string Name { get; }

        public string Date { get; }

        public int Price { get; }

        public string Address { get; }

        public NewTournamentAddedDomainEvent(string name, string date, int price, string address)
        {
            Name = name;
            Date = date;
            Price = price;
            Address = address;
        }
    }
}
