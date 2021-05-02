using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Core.IntegrationEventHandlers
{
    public class CheckoutIntegrationEventHandler : INotificationHandler<CheckoutIntegrationEvent>
    {
        public Task Handle(CheckoutIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
