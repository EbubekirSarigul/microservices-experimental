using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Data.Events;

namespace Tournament.Core.DomainEventHandlers
{
    public class TournamentEntryPriceChangedDomainEventHandler : INotificationHandler<TournamentEntryPriceChangedDomainEvent>
    {
        public async Task Handle(TournamentEntryPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(10);
            var sss = "todo";
        }
    }
}
