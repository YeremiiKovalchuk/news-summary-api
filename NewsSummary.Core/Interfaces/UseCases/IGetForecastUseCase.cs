using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases;

public interface IGetForecastUseCase
{
    public Task<Result<ForecastResponseDTO>> Execute(ForecastRequestSettings settings);
}
