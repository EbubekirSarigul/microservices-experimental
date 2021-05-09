using MediatR;
using Player.Data.Entities;
using Player.Data.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Core.IntegrationEventHandlers
{
    public class CheckoutIntegrationEventHandler : INotificationHandler<CheckoutIntegrationEvent>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CheckoutIntegrationEventHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(CheckoutIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var order = new Orders(notification.TotalPrice, notification.PaymentId, notification.PlayerId);

            notification.Tournaments.ForEach(x => order.AddOrderDetail(Guid.Parse(x.Id)));

            await _paymentRepository.AddOrder(order);

            await _paymentRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
