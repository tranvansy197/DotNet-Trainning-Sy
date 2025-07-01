namespace App.Api.Service;

public interface IRedisService
{
    Task SetStringAsync(string key, string value);
    Task<string?> GetStringAsync(string key);
    Task<bool?> Delete(string key);
}