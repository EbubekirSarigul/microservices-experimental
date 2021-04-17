using AutoMapper;
using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Core.Models;
using Tournament.Data.Repositories;

namespace Tournament.Core.Queries
{
    public class GetTournamentsQueryHandler : IRequestHandler<GetTournamentsQuery, GetTournamentsResult>
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMapper _mapper;

        public GetTournamentsQueryHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }
        public async Task<GetTournamentsResult> Handle(GetTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = await _tournamentRepository.GetTournaments();

            if (!tournaments.Any())
            {
                throw new BusinessException("NO_TOURNAMENTS", "Not tournaments are found.", System.Net.HttpStatusCode.NotFound);
            }

            var tournamentsModel = _mapper.Map<ICollection<TournamentModel>>(tournaments);
            return new GetTournamentsResult
            {
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success",
                Tournaments = tournamentsModel
            };
        }
    }
}
