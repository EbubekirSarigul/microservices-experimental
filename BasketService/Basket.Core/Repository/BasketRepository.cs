using Basket.Core.Models;
using MicroserviceTraining.Framework.Cache.Abstraction;
using MicroserviceTraining.Framework.Extensions;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Basket.Core.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ICacheProvider _cacheProvider;

        public BasketRepository(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<PlayerBasket> AddToBasket(string playerId, Tournament tournament)
        {
            var basket = await _cacheProvider.GetItem<PlayerBasket>(playerId);

            if (basket == default(PlayerBasket))
            {
                basket = new PlayerBasket();
            }

            basket.AddItem(tournament);
            await _cacheProvider.AddItem(playerId, basket);

            return await _cacheProvider.GetItem<PlayerBasket>(playerId);
        }

        public async Task<PlayerBasket> GetBasket(string playerId)
        {
            return await _cacheProvider.GetItem<PlayerBasket>(playerId);
        }
    }
}
