using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast;

namespace NewsSummary.Core.Services;

public class ForecastService: IForecastService
{
    private readonly IGetForecastUseCase _forecastGetter;
    private readonly ILogger<ForecastService> _logger;
    
    public ForecastService(IGetForecastUseCase forecastGetter, ILogger<ForecastService> logger) 
    { 
        this._forecastGetter = forecastGetter; 
        this._logger = logger;
    }

    public async Task<Result<ForecastResponseDTO>> GetForecast()
    {
        return await _forecastGetter.Get(new Models.Forecast.WeatherAPI.ForecastRequestSettings());
    }

}
