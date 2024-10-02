using Polly;
using Polly.Retry;

namespace NewsSummary.Core.Constants;

public static class Policies
{
    public static readonly AsyncRetryPolicy WaitAndRetry = Policy.Handle<Exception>().WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(500 * attempt));
}
