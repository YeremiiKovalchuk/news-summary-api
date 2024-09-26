namespace NewsSummary.Core.Models.Forecast.WeatherAPI;

public class CityInfo
{
    public int Id { get; init; }  // Maps to "id"
    public string Name { get; init; } = null!;  // Maps to "name"
    public CoordInfo Coord { get; init; } = null!;  // Maps to "coord"
    public string Country { get; init; } = null!;  // Maps to "country"
    public int Population { get; init; }  // Maps to "population"
    public int Timezone { get; init; }  // Maps to "timezone"
    public long Sunrise { get; init; }  // Maps to "sunrise" (UNIX timestamp)
    public long Sunset { get; init; }  // Maps to "sunset" (UNIX timestamp)
}
