using MicroserviceTraining.Framework.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Player.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Player.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayerContext _playerContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _playerContext;
            }
        }

        public PlayerRepository(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public async Task<ICollection<Player.Data.Entities.Player>> GetPlayersByMinimumRating(int minRating)
        {
            return await _playerContext.Players.Where(x => x.Rating >= minRating).ToListAsync();
        }

        public async Task<ICollection<Entities.Player>> GetPlayersByIdList(IEnumerable<Guid> playerIds)
        {
            return await _playerContext.Players.Where(x => playerIds.Contains(x.Id)).ToListAsync();
        }
    }
}
