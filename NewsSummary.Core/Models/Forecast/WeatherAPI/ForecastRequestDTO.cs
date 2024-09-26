using System.Text.Json.Serialization;

namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class ForecastRequestDTO
{
    public string Cod { get; init; } = null!;  // Maps to "cod"
    public int Message { get; init; }  // Maps to "message"
    public int Cnt { get; init; }  // Maps to "cnt"
    public List<ForecastEntry> List { get; init; } = new();  // Maps to "list"
    public CityInfo City { get; init; } = null!;  // Maps to "city"
}
