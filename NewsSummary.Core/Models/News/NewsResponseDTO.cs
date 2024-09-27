namespace NewsSummary.Core.Models.News;

public class NewsResponseDTO
{
    public string Country { get; init; } = null!;

    public List<CommonNewsEntry> News { get; init; }
}
