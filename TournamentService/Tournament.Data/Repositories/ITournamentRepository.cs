using MicroserviceTraining.Framework.Data.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity = Tournament.Data.Entities;

namespace Tournament.Data.Repositories
{
    public interface ITournamentRepository : IRepository
    {
        Task<Entities.Tournament> AddTournament(Entity.Tournament tournament);

        Task<Entities.Tournament> GetTournament(string name);

        Task<ICollection<Entities.Tournament>> GetTournaments();
    }
}
