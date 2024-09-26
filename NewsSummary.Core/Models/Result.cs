namespace NewsSummary.Core.Models;

public class Result<T>
{
    public T? Value { get; init; }
    public bool Success { get; init; }
}
