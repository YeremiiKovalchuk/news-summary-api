using Microsoft.Extensions.Caching.Distributed;
using NewsSummary.Infrastructure.Constants;
using System.Text.Json;

namespace NewsSummary.Infrastructure.Extensions;

public static class DistributedCacheExtensions
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string key, T value)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, jsonData, CacheConstants.CommonCacheOptions);
    }

    public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }
}
