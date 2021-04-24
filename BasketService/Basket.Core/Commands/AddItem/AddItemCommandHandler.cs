using Basket.Core.Models;
using Basket.Core.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Core.Commands.AddItem
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, PlayerBasket>
    {
        private readonly IBasketRepository _basketRepository;

        public AddItemCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<PlayerBasket> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.AddToBasket(request.PlayerId, request.Tournament);

            return basket;
        }
    }
}
