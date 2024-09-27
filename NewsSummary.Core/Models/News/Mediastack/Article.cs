using System.Text.Json.Serialization;

namespace NewsSummary.Core.Models.News.Mediastack;

public class Article
{
    public string Author { get; init; } = null!;  // Maps to "author"
    public string Title { get; init; } = null!;  // Maps to "title"
    public string Description { get; init; } = null!;  // Maps to "description"
    public string Url { get; init; } = null!;  // Maps to "url"
    public string Source { get; init; } = null!;  // Maps to "source"
    public string Image { get; init; } = null!;  // Maps to "image"
    public string Category { get; init; } = null!;  // Maps to "category"
    public string Language { get; init; } = null!;  // Maps to "language"
    public string Country { get; init; } = null!;  // Maps to "country"
    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; init; }  // Maps to "published_at"
}
