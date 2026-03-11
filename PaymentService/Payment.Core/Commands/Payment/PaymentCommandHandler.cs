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

        /// <summary>
        /// Handles a payment command by marking the referenced order as completed and producing a payment result.
        /// </summary>
        /// <param name="request">The payment command containing the identifier of the order to process.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="PaymentResult"/> containing the order identifier, response code, and response message.</returns>
        /// <exception cref="BusinessException">Thrown with code "ORDER_NOT_FOUND" and HTTP 404 when the order cannot be found, or with code "INVALID_ORDER_STATE" and HTTP 400 when the order is not in the "PENDING" state.</exception>
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
