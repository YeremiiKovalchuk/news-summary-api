
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces;

public interface IForecastService
{
    public Task<Result<ForecastResponseDTO>> GetForecast();
}
