using Microsoft.Extensions.Caching.Distributed;

namespace NewsSummary.Infrastructure.Constants;

public static class CacheConstants
{
    public static readonly TimeSpan AbsoluteExpirationTime = TimeSpan.FromHours(12);

    public static readonly DistributedCacheEntryOptions CommonCacheOptions = new() { AbsoluteExpirationRelativeToNow = AbsoluteExpirationTime };

    public const string NamePrefix = "SummaryAPI_";
}
