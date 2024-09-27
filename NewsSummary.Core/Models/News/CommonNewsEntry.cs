namespace NewsSummary.Core.Models.News;

public class CommonNewsEntry
{
    public string Source { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Url { get; init; } = null!;
    public DateTime PublicationDate { get; init; }
}
