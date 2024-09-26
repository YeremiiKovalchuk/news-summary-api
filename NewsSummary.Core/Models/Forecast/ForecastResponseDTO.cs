namespace NewsSummary.Core.Models.Forecast;

public class ForecastResponseDTO
{
    public string CityName { get; init; } = null!;
    public List<CommonForecastEntry> Forecasts { get; init; } = null!;

    public int Count { get => Forecasts?.Count ?? 0; }

}
