namespace NewsSummary.Core.Extensions;

public static class DoubleExtensions
{
    public static string SetCommonFormat(this double value)
    {
        return value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
    }
}
