using Basket.Core.Models;
using MicroserviceTraining.Framework.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Basket.Core.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<PlayerBasket> AddToBasket(string playerId, Tournament tournament)
        {
            var basket = new PlayerBasket();

            var item = await _cache.GetStringAsync(playerId);

            if (!string.IsNullOrEmpty(item))
            {
                basket = item.Deserialize<PlayerBasket>();
            }

            basket.AddItem(tournament);

            await _cache.SetStringAsync(playerId, basket.ToJson(), new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            return (await _cache.GetStringAsync(playerId)).Deserialize<PlayerBasket>();
        }

        public async Task DeleteBasket(string playerId)
        {
            var basket = await _cache.GetStringAsync(playerId);
            if (!string.IsNullOrEmpty(basket))
            {
                await _cache.RemoveAsync(playerId);
            }
        }

        public async Task<PlayerBasket> GetBasket(string playerId)
        {
            var basket = await _cache.GetStringAsync(playerId);
            if (basket != null)
            {
                return basket.Deserialize<PlayerBasket>();
            }
            else
            {
                return default(PlayerBasket);
            }
        }
    }
}
