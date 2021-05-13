using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tournament.Data.Repositories;

namespace Tournament.Core.IntegrationEventHandlers.PaymentCompleted
{
    public class PaymentCompletedIntegrationEventHandler : INotificationHandler<PaymentCompletedIntegrationEvent>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public PaymentCompletedIntegrationEventHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task Handle(PaymentCompletedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var tournaments = await _tournamentRepository.GetTournaments(notification.Tournaments);

            tournaments.ForEach(x => x.AddParticipant(notification.PlayerId));

            await _tournamentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
