using System.Text.Json.Serialization;

namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class ForecastEntry
{

    public long Dt { get; init; }  // Maps to "dt" (UNIX timestamp)
    public MainInfo Main { get; init; } = null!;  // Maps to "main"
    public List<WeatherInfo> Weather { get; init; } = new();  // Maps to "weather"
    public CloudsInfo Clouds { get; init; } = null!;  // Maps to "clouds"
    public WindInfo Wind { get; init; } = null!;  // Maps to "wind"
    public double Visibility { get; init; }  // Maps to "visibility"
    public double Pop { get; init; }  // Maps to "pop"
    public RainInfo? Rain { get; init; }  // Maps to "rain"
    public SysInfo Sys { get; init; } = null!;  // Maps to "sys"
    [JsonPropertyName("dt_txt")]
    public string DtTxt { get; init; } = null!;  // Maps to "dt_txt"

}
