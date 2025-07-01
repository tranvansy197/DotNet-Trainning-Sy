using StackExchange.Redis;

namespace App.Api.Service.impl;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = _redis.GetDatabase();
    }

    public async Task SetStringAsync(string key, string value)
    {
        await _db.StringSetAsync(key, value);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }
    
    public async Task<bool?> Delete(string key)
    {
        return await _db.KeyDeleteAsync(key);
    }
}