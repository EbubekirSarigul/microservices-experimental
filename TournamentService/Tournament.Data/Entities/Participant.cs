using MicroserviceTraining.Framework.Data;
using System;

namespace Tournament.Data.Entities
{
    public class Participant : Entity
    {
        public Participant(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public Guid PlayerId { get; set; }
    }
}
