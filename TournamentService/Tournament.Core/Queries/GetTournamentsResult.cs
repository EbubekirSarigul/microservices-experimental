using MicroserviceTraining.Framework.Models;
using System.Collections.Generic;
using Tournament.Core.Models;

namespace Tournament.Core.Queries
{
    public class GetTournamentsResult : BaseResult
    {
        public ICollection<TournamentModel> Tournaments { get; set; }
    }
}
