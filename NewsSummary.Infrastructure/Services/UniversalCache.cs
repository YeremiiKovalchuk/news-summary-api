using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsSummary.Core.Interfaces;
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

    public Task<TValue?> GetValue<TValue>(string key)
    {
        throw new NotImplementedException();
    }

    public Task SetValue<TValue>(string key, TValue value)
    {
        throw new NotImplementedException();
    }
}
