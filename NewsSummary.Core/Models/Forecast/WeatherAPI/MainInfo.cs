using System.Text.Json.Serialization;

namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class MainInfo
{
    public double Temp { get; init; }  // Maps to "temp"

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }  // Maps to "feels_like"
    [JsonPropertyName("temp_min")]
    public double TempMin { get; init; }  // Maps to "temp_min"
    [JsonPropertyName("temp_max")]
    public double TempMax { get; init; }  // Maps to "temp_max"
    public int Pressure { get; init; }  // Maps to "pressure"
    [JsonPropertyName("sea_level")]
    public int SeaLevel { get; init; }  // Maps to "sea_level"
    [JsonPropertyName("grnd_level")]
    public int GrndLevel { get; init; }  // Maps to "grnd_level"
    public int Humidity { get; init; }  // Maps to "humidity"
    [JsonPropertyName("temp_kf")]
    public double TempKf { get; init; }  // Maps to "temp_kf"
}
