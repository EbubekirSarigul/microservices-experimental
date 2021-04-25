using MicroserviceTraining.Framework.Cache.Abstraction;
using MicroserviceTraining.Framework.Extensions;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        public static ConnectionMultiplexer ConnectionMultiplexer;
        static object lockConnectionMultiplexer = new object();

        public RedisCacheProvider(RedisSettings redisSettings)
        {
            if (ConnectionMultiplexer == null)
            {
                lock (lockConnectionMultiplexer)
                {
                    if (ConnectionMultiplexer == null)
                    {
                        var configuration = ConfigurationOptions.Parse(redisSettings.ConnectionString, true);

                        configuration.ResolveDns = true;

                        ConnectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
                    }
                }
            }
        }

        public async Task<T> AddItem<T>(string key, T item)
        {
            var database = ConnectionMultiplexer.GetDatabase();

            await database.StringSetAsync(key, item.ToJson());

            return await GetItem<T>(key);
        }

        public async Task<T> GetItem<T>(string key)
        {
            var database = ConnectionMultiplexer.GetDatabase();
            var item = await database.StringGetAsync(key);

            if (item.IsNullOrEmpty)
            {
                return default(T);
            }

            return item.ToString().Deserialize<T>();
        }
    }
}
