using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using Player.Data.Enums;
using Player.Data.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Core.Commands.Payment
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, PaymentResult>
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResult> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _paymentRepository.GetOrder(request.PaymentId);

            var orderId = order.Id.ToString();

            if (order == null)
            {
                throw new BusinessException("ORDER_NOT_FOUND", "Order cannot be found.", System.Net.HttpStatusCode.NotFound);
            }

            if (order.OrderStatus != "PENDING")
            {
                throw new BusinessException("INVALID_ORDER_STATE", "Order state is invalid.", System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                order.SetStatusCompleted();
                await _paymentRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception)
            {
            }

            return new PaymentResult
            {
                OrderId = orderId,

                ResponseCode = "00",

                ResponseMessage = "payment done"
            };
        }
    }
}
