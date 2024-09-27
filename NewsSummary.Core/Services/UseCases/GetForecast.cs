using AutoMapper;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Core.Models.Forecast.WeatherAPI;

namespace NewsSummary.Core.Services.UseCases;

public class GetForecast: IGetForecastUseCase
{
    private readonly ILogger _logger;
    private readonly IForecastClient _forecastClient;
    private readonly IUniversalCache _cache;
    private readonly IMapper _mapper;
    public GetForecast(IForecastClient forecastClient, IUniversalCache cache, IMapper mapper, ILogger<GetForecast> logger)
    {
        this._forecastClient = forecastClient;
        this._cache = cache;
        this._logger = logger;
        this._mapper = mapper;
    }

    /// <summary>
    /// Gets 5 day / 3 hour forecast
    /// </summary>
    public async Task<Result<ForecastResponseDTO>> Execute(ForecastRequestSettings settings)
    {

        var resultUnmapped = await GetUnmappedResult(settings);

        var resultMapped = resultUnmapped.Success ? _mapper.Map<ForecastResponseDTO>(resultUnmapped.Value) : null;
     
        return new Result<ForecastResponseDTO>()
        {
            //Success = response.IsSuccessStatusCode,
            Success = resultUnmapped.Success,
            Value = resultMapped
        };
    }

    private async Task<Result<ForecastRequestDTO>> GetUnmappedResult(ForecastRequestSettings settings)
    {
        var key = WebConstants.ForecastCacheKey + "_" + settings.City.ToUpperInvariant();
        var resultFromCache = await this._cache.GetValueAsync<Result<ForecastRequestDTO>>(key);
        if (resultFromCache != null)
        {
            return resultFromCache;
        }

        this._logger.LogInformation("Didn`t find forecast entry in the cache.");
        var result = await this._forecastClient.GetForecast(settings);

        if (result.Success)
        {
            await this._cache.SetValueAsync(key, result);
        }

        return result;

    }

}
