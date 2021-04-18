using MediatR;
using MicroserviceTraining.Framework.Sms.Abstractions;
using Player.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Player.Core.IntegrationEventHandlers.TournamentDateChanged
{
    public class TournamentDateChangedIntegrationEventHandler : INotificationHandler<TournamentDateChangedIntegrationEvent>
    {
        private readonly ISmsProvider _smsProvider;
        private readonly IPlayerRepository _playerRepository;

        public TournamentDateChangedIntegrationEventHandler(ISmsProvider smsProvider, IPlayerRepository playerRepository)
        {
            _smsProvider = smsProvider;
            _playerRepository = playerRepository;
        }

        public async Task Handle(TournamentDateChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            if (notification.PlayerList != null)
            {
                var playersToInform = await _playerRepository.GetPlayersByIdList(notification.PlayerList);
                foreach (var player in playersToInform)
                {
                    await _smsProvider.SendSms(player.PhoneNumber, notification.ToString());
                }
            }
        }
    }
}
