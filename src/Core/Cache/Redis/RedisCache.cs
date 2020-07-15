using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace Core.Cache.Redis
{
    public class RedisCache : ICacheManager
    {
        private readonly IDatabase _redisDb;
        public RedisCache(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<bool> SetAsync<T>(string key, T data, int duration)
        {
            var created = await _redisDb.StringSetAsync(key, JsonSerializer.Serialize(data), System.TimeSpan.FromDays(duration));

            return created;

        }

        public async Task<bool> SetAsync(string key, object data, int duration)
        {
            var created = await _redisDb.StringSetAsync(key, JsonSerializer.Serialize(data), System.TimeSpan.FromDays(duration));

            return created;
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await _redisDb.KeyDeleteAsync(key);
        }

        public async Task<T> GetAsync<T>(string key) where T : class, new()
        {
            RedisValue data = await _redisDb.StringGetAsync(key);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(data);
        }

        public async Task<object> GetAsync(string key)
        {
            RedisValue data = await _redisDb.StringGetAsync(key);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<object>(data);
        }
    }
}