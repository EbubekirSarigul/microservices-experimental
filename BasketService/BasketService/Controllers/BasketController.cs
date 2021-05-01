using Basket.Core.Commands.AddItem;
using Basket.Core.Models;
using Basket.Core.Repository;
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
        private readonly IBasketRepository _basketRepository;

        public BasketController(IMediator mediator, IBasketRepository basketRepository)
        {
            _mediator = mediator;
            _basketRepository = basketRepository;
        }

        [HttpPost]
        public async Task<ActionResult<AddItemResult>> AddItem([FromBody] AddItemCommand addItemCommand)
        {
            return await _mediator.Send(addItemCommand);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket([FromQuery] string playerId)
        {
            await _basketRepository.DeleteBasket(playerId);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<PlayerBasket>> GetBasket([FromQuery] string playerId)
        {
            var basket = await _basketRepository.GetBasket(playerId);

            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }
    }
}
