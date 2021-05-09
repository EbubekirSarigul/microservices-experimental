using Basket.Core.IntegrationEvents;
using Basket.Core.Repository;
using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using MicroserviceTraining.Framework.IntegrationEvents.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Core.Commands.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, CheckoutResult>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IntegrationEventService _integrationEventService;

        public CheckoutCommandHandler(IBasketRepository basketRepository, IntegrationEventService integrationEventService)
        {
            _basketRepository = basketRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<CheckoutResult> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasket(request.PlayerId);

            if (basket == null || !basket.Tournament.Any())
            {
                throw new BusinessException("NO_BASKET", "Basket cannot be found.", System.Net.HttpStatusCode.BadRequest);
            }

            CheckoutIntegrationEvent integrationEvent = new CheckoutIntegrationEvent(basket.Tournament.Sum(x => x.Price), Guid.Parse(request.PlayerId), basket.Tournament);
            await _integrationEventService.PublishEventAsync(integrationEvent, Constant.EventTopic_CheckoutAccepted);

            return new CheckoutResult
            {
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success",
                PaymentId = integrationEvent.PaymentId.ToString()
            };
        }
    }
}
