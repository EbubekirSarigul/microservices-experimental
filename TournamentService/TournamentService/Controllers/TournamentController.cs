using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tournament.Core.Commands.CreateTournament;
using Tournament.Core.Commands.UpdateTournament;
using Tournament.Core.Queries;

namespace TournamentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateTournamentResult>> CreateTournament([FromBody] CreateTournamentCommand createTournamentCommand)
        {
            return await _mediator.Send(createTournamentCommand);
        }

        [HttpGet]
        public async Task<ActionResult<GetTournamentsResult>> GetTournaments([FromQuery] GetTournamentsQuery getTournamentsQuery)
        {
            return await _mediator.Send(getTournamentsQuery);
        }

        [HttpPut]
        public async Task<ActionResult<UpdateTournamentResult>> UpdateTournament([FromBody] UpdateTournamentCommand updateTournamentCommand)
        {
            return await _mediator.Send(updateTournamentCommand);
        }
    }
}
