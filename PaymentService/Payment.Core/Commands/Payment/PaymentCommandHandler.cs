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

            if (order == null)
            {
                throw new BusinessException("ORDER_NOT_FOUND", "Order cannot be found.", System.Net.HttpStatusCode.NotFound);
            }

            if (!order.OrderStatus.Equals(OrderStatusEnum.PENDING.ToString()))
            {
                throw new BusinessException("INVALID_ORDER_STATE", "Order state is invalid.", System.Net.HttpStatusCode.BadRequest);
            }

            ///// mock payment

            order.SetStatusCompleted();

            await _paymentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new PaymentResult
            {
                OrderId = order.Id.ToString(),
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success"
            };
        }
    }
}
