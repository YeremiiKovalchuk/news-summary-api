namespace NewsSummary.Core.Models.Forecast;

public class CommonForecastEntry
{
    public string DtTxt { get; init; } = null!;
    public double Temp { get; init; }
    public string Weather { get; init; } = null!;
}
