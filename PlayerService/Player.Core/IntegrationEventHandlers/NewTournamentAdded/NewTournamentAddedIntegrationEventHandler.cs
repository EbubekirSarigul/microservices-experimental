using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.Sms.Abstractions;
using Player.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Player.Core.IntegrationEventHandlers.NewTournamentAdded
{
    public class NewTournamentAddedIntegrationEventHandler : INotificationHandler<NewTournamentAddedIntegrationEvent>
    {
        private readonly ISmsProvider _smsProvider;
        private readonly IPlayerRepository _playerRepository;

        public NewTournamentAddedIntegrationEventHandler(ISmsProvider smsProvider, IPlayerRepository playerRepository)
        {
            _smsProvider = smsProvider;
            _playerRepository = playerRepository;
        }

        public async Task Handle(NewTournamentAddedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var playersToInform = await _playerRepository.GetPlayersByMinimumRating(Constant.MinRatingForTournamentInforming);

            foreach (var player in playersToInform)
            {
                await _smsProvider.SendSms(player.PhoneNumber, notification.ToString());
            }
        }
    }
}
