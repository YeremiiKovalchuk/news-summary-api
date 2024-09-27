namespace NewsSummary.Infrastructure.Models;

public class CacheOptions
{
    public bool IsRedis { get; init; } = false;

    public DateTime SlidingExpiration { get; init; }

    public DateTime AbsoluteExpiration { get; init; }
}
