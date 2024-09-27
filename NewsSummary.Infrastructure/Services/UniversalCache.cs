using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Extensions;
using NewsSummary.Infrastructure.Models;

namespace NewsSummary.Infrastructure.Services;

public class UniversalCache : IUniversalCache
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger _logger;
    public UniversalCache(IOptions<CacheOptions> options, IDistributedCache distributedCache, ILogger<UniversalCache> logger)
    {
        this._logger = logger;
        this._distributedCache = distributedCache;
    }

    public async Task<TValue?> GetValueAsync<TValue>(string key)
    {
        this._logger.LogInformation($"Getting {key} from cache.");
        return await this._distributedCache.GetRecordAsync<TValue>(key);
    }

    public async Task SetValueAsync<TValue>(string key, TValue value)
    {
        this._logger.LogInformation($"Setting {key} in cache.");
        await this._distributedCache.SetRecordAsync<TValue>(key, value);
    }
}
