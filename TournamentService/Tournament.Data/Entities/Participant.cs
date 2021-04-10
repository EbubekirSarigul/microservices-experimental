using MicroserviceTraining.Framework.Data;
using System;

namespace Tournament.Data.Entities
{
    public class Participant : Entity
    {
        public Guid TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public Guid PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string PlayerSurname { get; set; }

        public string PlayerRating { get; set; }
    }
}
