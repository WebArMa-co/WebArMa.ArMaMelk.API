using StackExchange.Redis;
using WebArMa.ArMaMelk.API.Application.Redis;

namespace WebArMa.ArMaMelk.Persistence.Services
{
    public class RedisService(IDatabase database) : IRedisService
    {
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await database.StringSetAsync(key, value, expiry, When.Always);
        }

        public async Task<string?> GetAsync(string key)
        {
            var value = await database.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await database.KeyExistsAsync(key);
        }

        public async Task DeleteAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }
    }
}
