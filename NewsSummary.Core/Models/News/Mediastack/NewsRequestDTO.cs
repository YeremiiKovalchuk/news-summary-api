namespace NewsSummary.Core.Models.News.Mediastack;

public class NewsRequestDTO
{
    public Pagination Pagination { get; init; } = null!;
    public List<Article> Data { get; init; } = new();
}
