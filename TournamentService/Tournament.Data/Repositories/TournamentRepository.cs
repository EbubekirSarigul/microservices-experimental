using MicroserviceTraining.Framework.Data.Interface;
using MicroserviceTraining.Framework.Enum;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using Microsoft.EntityFrameworkCore;
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
                throw new BusinessException(ErrorCodes.TOURNAMENT_ALREADY_EXISTS.Value, "A tournament with the same name is already exists.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<Entities.Tournament> GetTournament(string name)
        {
            var result = await _tournamentContext.Tournament.Where(x => x.Name == name).SingleOrDefaultAsync();
            return result;
        }
    }
}
