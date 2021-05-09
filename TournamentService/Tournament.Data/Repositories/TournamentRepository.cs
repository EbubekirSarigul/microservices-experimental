using MicroserviceTraining.Framework.Data.Interface;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournament.Data.Context;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentContext _tournamentContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _tournamentContext;
            }
        }

        public TournamentRepository(TournamentContext tournamentContext)
        {
            _tournamentContext = tournamentContext;
        }

        public async Task<Entities.Tournament> AddTournament(Entities.Tournament tournament)
        {
            var isExists = await _tournamentContext.Tournament.AnyAsync(x => x.Name == tournament.Name);
            if (!isExists)
            {
                var result = await _tournamentContext.AddAsync(tournament);
                return result.Entity;
            }
            else
            {
                throw new BusinessException("TOURNAMENT_ALREADY_EXISTS", "A tournament with the same name is already exists.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public void UpdateTournament(Entities.Tournament tournament)
        {
            _tournamentContext.Update(tournament);

        }

        public async Task<Entities.Tournament> GetTournament(Guid id)
        {
            var result = await _tournamentContext.Tournament.Include(x => x.Participants).SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<ICollection<Entities.Tournament>> GetTournaments()
        {
            var result = await _tournamentContext.Tournament.Include(x=>x.Participants).ToListAsync();
            return result;
        }

        public async Task<List<Entities.Tournament>> GetTournaments(List<Guid> ids)
        {
            var result = await _tournamentContext.Tournament.Include(x => x.Participants).Where(x => ids.Contains(x.Id)).ToListAsync();
            return result;
        }
    }
}
