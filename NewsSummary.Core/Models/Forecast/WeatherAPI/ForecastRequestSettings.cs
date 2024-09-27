namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class ForecastRequestSettings
{
    public string City { get; init; } = null!;

    public const string Units = "metric";

    public string Lang { get; init; } = "en";

    public string? ApiKey { get; init; } = null!;
}
