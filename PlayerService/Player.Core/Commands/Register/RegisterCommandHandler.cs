using MediatR;
using MicroserviceTraining.Framework.Constants;
using Player.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Player.Core.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly IPlayerRepository _playerRepository;

        public RegisterCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Data.Entities.Player player = new Data.Entities.Player(request.Name, request.Surname, request.PhoneNumber, request.Rating);
            await _playerRepository.AddPlayer(player);
            await _playerRepository.UnitOfWork.SaveEntitiesAsync();

            return new RegisterResult
            {
                ResponseCode = Constant.ResultCode_Success,
                ResponseMessage = "Success"
            };
        }
    }
}
