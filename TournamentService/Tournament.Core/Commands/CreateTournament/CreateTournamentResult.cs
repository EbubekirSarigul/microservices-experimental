using MicroserviceTraining.Framework.Models;
using System;

namespace Tournament.Core.Commands.CreateTournament
{
    public class CreateTournamentResult : BaseResult
    {
        public Guid TournamentId { get; set; }
    }
}
