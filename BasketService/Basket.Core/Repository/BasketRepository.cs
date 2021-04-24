using Basket.Core.Models;
using MicroserviceTraining.Framework.Extensions;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Basket.Core.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<PlayerBasket> AddToBasket(string playerId, Tournament tournament)
        {
            var basket = await GetBasket(playerId);

            if (basket == default(PlayerBasket))
            {
                basket = new PlayerBasket();
            }

            basket.AddItem(tournament);
            await _database.StringSetAsync(playerId, basket.ToJson());

            return await GetBasket(playerId);
        }

        public async Task<PlayerBasket> GetBasket(string playerId)
        {
            var item = await _database.StringGetAsync(playerId);

            if(item.IsNullOrEmpty)
            {
                return default(PlayerBasket);
            }

            return item.ToString().Deserialize<PlayerBasket>();
        }
    }
}
