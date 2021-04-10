using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;

namespace Tournament.Core.IntegrationEvents
{
    public class TournamentAddedIntegrationEvent : IntegrationEvent
    {
        public string Name { get; }

        public string Date { get; }

        public int Price { get; }

        public string Address { get; }

        public TournamentAddedIntegrationEvent(string name, string date, int price, string address)
        {
            Name = name;
            Date = date;
            Price = price;
            Address = address;
        }
    }
}
