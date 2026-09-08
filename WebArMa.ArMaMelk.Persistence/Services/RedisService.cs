using StackExchange.Redis;
using WebArMa.ArMaMelk.API.Application.Redis;

namespace WebArMa.ArMaMelk.Persistence.Services
{
    public class RedisService(IConnectionMultiplexer redis) : IRedisService
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry, When.Always);
        }

        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task DeleteAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
