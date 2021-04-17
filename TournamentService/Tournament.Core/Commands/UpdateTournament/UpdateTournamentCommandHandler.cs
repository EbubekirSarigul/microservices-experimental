using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Data.Repositories;

namespace Tournament.Core.Commands.UpdateTournament
{
    public class UpdateTournamentCommandHandler : IRequestHandler<UpdateTournamentCommand, UpdateTournamentResult>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public UpdateTournamentCommandHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<UpdateTournamentResult> Handle(UpdateTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = _tournamentRepository.GetTournament(Guid.Parse(request.Id));

            if(tournament == null)
            {
                throw new BusinessException("TOURNAMENT_NOT_FOUND", "Tournament is not found.", System.Net.HttpStatusCode.NotFound);
            }

            tournament.SetDate(request.Date);
            tournament.SetPrice(request.EntryPrice);

            _tournamentRepository.UpdateTournament(tournament);

            await _tournamentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new UpdateTournamentResult
            {
                TournamentId = tournament.Id,
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success"
            };
        }
    }
}
