using MicroserviceTraining.Framework.Data.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity = Tournament.Data.Entities;

namespace Tournament.Data.Repositories
{
    public interface ITournamentRepository : IRepository
    {
        Task<Entities.Tournament> AddTournament(Entity.Tournament tournament);

        void UpdateTournament(Entities.Tournament tournament);

        Entities.Tournament GetTournament(Guid id);

        Task<ICollection<Entities.Tournament>> GetTournaments();


    }
}
