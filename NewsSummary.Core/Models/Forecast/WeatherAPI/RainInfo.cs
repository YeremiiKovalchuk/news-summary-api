using System.Text.Json.Serialization;

namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class RainInfo
{
    [JsonPropertyName("3h")]
    public double? Volume { get; init; }  // Maps to "3h" (in JSON, use [JsonPropertyName("3h")] if necessary)
}
