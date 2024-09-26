namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class WindInfo
{
    public double Speed { get; init; }  // Maps to "speed"
    public int Deg { get; init; }  // Maps to "deg"
    public double Gust { get; init; }  // Maps to "gust"
}
