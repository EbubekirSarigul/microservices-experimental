using MicroserviceTraining.Framework.Data.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Player.Data.Repositories
{
    public interface IPlayerRepository : IRepository
    {
        Task<ICollection<Player.Data.Entities.Player>> GetPlayersByMinimumRating(int minRating);
    }
}
