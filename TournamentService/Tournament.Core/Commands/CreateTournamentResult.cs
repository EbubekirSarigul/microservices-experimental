using MicroserviceTraining.Framework.Models;
using System;

namespace Tournament.Core.Commands
{
    public class CreateTournamentResult : BaseResult
    {
        public Guid TournamentId { get; set; }
    }
}
