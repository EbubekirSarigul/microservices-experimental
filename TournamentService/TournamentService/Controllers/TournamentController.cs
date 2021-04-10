﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tournament.Core.Commands;

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
    }
}
