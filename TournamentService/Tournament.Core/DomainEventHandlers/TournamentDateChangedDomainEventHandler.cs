using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Data.Events;

namespace Tournament.Core.DomainEventHandlers
{
    public class TournamentDateChangedDomainEventHandler : INotificationHandler<TournamentDateChangedDomainEvent>
    {
        public async Task Handle(TournamentDateChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(10);
            var sss = "todo";
        }
    }
}
