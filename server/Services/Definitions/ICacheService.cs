namespace KePass.Server.Services.Definitions;

public interface ICacheService
{
    Task SetAsync<T>(string key, string value, TimeSpan? expiration = null);
    Task<string?> GetAsync(string key);
    Task RemoveAsync(string key);
    Task<bool> ExistsAsync(string key);
}