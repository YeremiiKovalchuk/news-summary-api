namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class WeatherInfo
{
    public int Id { get; init; }  // Maps to "id"
    public string Main { get; init; } = null!;  // Maps to "main"
    public string Description { get; init; } = null!;  // Maps to "description"
    public string Icon { get; init; } = null!;  // Maps to "icon"
}
