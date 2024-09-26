namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class ForecastRequestSettings
{
    public decimal Latitude { get; init; }

    public decimal Longitude { get; init; }

    public const string Units = "metric";

    public string Lang { get; init; } = "en";

    public string? ApiKey { get; init; } = null!;
}
