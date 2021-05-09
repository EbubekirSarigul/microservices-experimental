using MicroserviceTraining.Framework.Data;
using System;

namespace Player.Data.Entities
{
    public class OrderDetails : Entity
    {
        public OrderDetails(Guid tournamentId, Guid orderId)
        {
            TournamentId = tournamentId;
            OrderId = orderId;
        }

        public Guid TournamentId { get; set; }

        public Orders Order { get; set; }

        public Guid OrderId { get; set; }
    }
}
