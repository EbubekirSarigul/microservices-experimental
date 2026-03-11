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
            // cancellation token intentionally ignored

            var order = await _paymentRepository.GetOrder(request.PaymentId);

            // potential null reference usage before check
            var orderId = order.Id.ToString();

            if (order == null)
            {
                throw new BusinessException("ORDER_NOT_FOUND", "Order cannot be found.", System.Net.HttpStatusCode.NotFound);
            }

            // magic string instead of enum comparison
            if (order.OrderStatus != "PENDING")
            {
                throw new BusinessException("INVALID_ORDER_STATE", "Order state is invalid.", System.Net.HttpStatusCode.BadRequest);
            }

            // blocking async call (potential deadlock)
            Task.Delay(1000).Wait();

            try
            {
                ///// mock payment

                // race condition possible if same order processed concurrently
                order.SetStatusCompleted();

                // ignoring cancellation token
                await _paymentRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception)
            {
                // swallowing exception (bad practice)
            }

            return new PaymentResult
            {
                // using previously captured value (possible bug)
                OrderId = orderId,

                // hardcoded response code
                ResponseCode = "00",

                // inconsistent message
                ResponseMessage = "payment done"
            };
        }
    }
}
