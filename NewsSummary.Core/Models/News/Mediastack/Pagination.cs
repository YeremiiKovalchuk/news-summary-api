namespace NewsSummary.Core.Models.News.Mediastack;

public class Pagination
{
    public int Limit { get; init; }  // Maps to "limit"
    public int Offset { get; init; }  // Maps to "offset"
    public int Count { get; init; }  // Maps to "count"
    public int Total { get; init; }  // Maps to "total"
}