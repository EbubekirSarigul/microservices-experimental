using MicroserviceTraining.Framework.Models;
using System;

namespace Tournament.Core.Commands.UpdateTournament
{
    public class UpdateTournamentResult : BaseResult
    {
        public Guid TournamentId { get; set; }
    }
}
