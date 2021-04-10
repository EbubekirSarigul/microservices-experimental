using MediatR;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using System;
using System.Globalization;

namespace Player.Core.IntegrationEventHandlers.NewTournamentAdded
{
    public class NewTournamentAddedIntegrationEvent : IntegrationEvent, INotification
    {
        public string Name { get; set; }

        public string Date { get; set; }

        public int Price { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return String.Format("New Tournament!! Name: {0}, Date{1}, Price: {2}, Address: {3}", Name,
                                                                                      DateTime.ParseExact(Date, "yyyyMMdd", CultureInfo.InvariantCulture).Date,
                                                                                      Price / 100, Address);
        }
    }
}
