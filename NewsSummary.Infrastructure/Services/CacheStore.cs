using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Constants;
using StackExchange.Redis;
using System.Text.Json;

namespace NewsSummary.Infrastructure.Services;

public class CacheStore : ICacheStore
{
    private readonly IDatabase _database;
    private readonly ILogger _logger;
    public CacheStore(IDatabase database, ILogger<CacheStore> logger)
    {
        this._logger = logger;
        this._database = database;
    }

    public async Task<TValue?> GetValueAsync<TValue>(string key)
    {
        key = CacheConstants.NamePrefix + key;
        this._logger.LogInformation($"Getting {key} from cache.");

        var jsonData = await Task.Run(() => this._database.StringGet(key));

        if (jsonData == RedisValue.Null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<TValue>(jsonData!);
    }

    public async Task SetValueAsync<TValue>(string key, TValue value)
    {
        key = CacheConstants.NamePrefix + key;
        this._logger.LogInformation($"Setting {key} in cache.");

        var jsonData = JsonSerializer.Serialize(value);

        _database.StringSet(key, jsonData);
        await _database.StringGetSetExpiryAsync(key, CacheConstants.AbsoluteExpirationTime);
    }
}
