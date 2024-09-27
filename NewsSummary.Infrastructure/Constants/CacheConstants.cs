using Microsoft.Extensions.Caching.Distributed;

namespace NewsSummary.Infrastructure.Constants;

public static class CacheConstants
{
    public const int AbsoluteExpirationHours = 12;

    public static DistributedCacheEntryOptions CommonCacheOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(AbsoluteExpirationHours) };
}
