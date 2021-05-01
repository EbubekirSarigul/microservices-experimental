using Basket.Core.Models;
using System.Threading.Tasks;

namespace Basket.Core.Repository
{
    public interface IBasketRepository
    {
        Task<PlayerBasket> AddToBasket(string playerId, Tournament tournament);

        Task<PlayerBasket> GetBasket(string playerId);

        Task DeleteBasket(string playerId);
    }
}
