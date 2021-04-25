using Basket.Core.Commands.AddItem;
using Basket.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<AddItemResult>> AddItem([FromBody] AddItemCommand addItemCommand)
        {
            return await _mediator.Send(addItemCommand);
        }
    }
}
