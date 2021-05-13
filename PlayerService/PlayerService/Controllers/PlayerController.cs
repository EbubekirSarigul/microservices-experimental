using MediatR;
using Microsoft.AspNetCore.Mvc;
using Player.Core.Commands.Register;
using System.Threading.Tasks;

namespace PlayerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterResult>> CreateTournament([FromBody] RegisterCommand registerCommand)
        {
            return await _mediator.Send(registerCommand);
        }
    }
}
