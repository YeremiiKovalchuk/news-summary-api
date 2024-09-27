using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
namespace NewsSummary.Core.Interfaces;

public interface IForecastClient
{
    public Task<Result<ForecastRequestDTO>> GetForecast(ForecastRequestSettings settings);
}
