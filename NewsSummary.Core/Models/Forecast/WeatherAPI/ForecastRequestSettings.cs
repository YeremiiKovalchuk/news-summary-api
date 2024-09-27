namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class ForecastRequestSettings
{
    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public const string Units = "metric";

    public string Lang { get; init; } = "en";

    public string? ApiKey { get; init; } = null!;
}
