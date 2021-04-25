using Basket.Core.Repository;
using MediatR;
using MicroserviceTraining.Framework.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Core.Commands.AddItem
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, AddItemResult>
    {
        private readonly IBasketRepository _basketRepository;

        public AddItemCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<AddItemResult> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.AddToBasket(request.PlayerId, request.Tournament);

            return new AddItemResult
            {
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success",
                Basket = basket
            };
        }
    }
}
