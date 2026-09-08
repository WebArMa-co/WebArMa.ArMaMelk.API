namespace WebArMa.ArMaMelk.API.Application.Redis
{
    public interface IRedisService
    {
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task DeleteAsync(string key);
    }
}
