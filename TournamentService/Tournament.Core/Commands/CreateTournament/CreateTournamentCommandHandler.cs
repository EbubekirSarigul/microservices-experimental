using MediatR;
using MicroserviceTraining.Framework.Constants;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Data.Repositories;
using Entity = Tournament.Data.Entities;

namespace Tournament.Core.Commands.CreateTournament
{
    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, CreateTournamentResult>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public CreateTournamentCommandHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<CreateTournamentResult> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            Entity.Tournament tournament = new Entity.Tournament(request.Name, request.Date, request.EntryPrice, request.Address);

            var tournamentEntity = await _tournamentRepository.AddTournament(tournament);

            await _tournamentRepository.UnitOfWork.SaveEntitiesAsync();

            return new CreateTournamentResult
            {
                TournamentId = tournament.Id,
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success"
            };
        }
    }
}
