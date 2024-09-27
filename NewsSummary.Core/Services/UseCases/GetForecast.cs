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
    private readonly IMapper _mapper;
    public GetForecast(IForecastClient forecastClient, IMapper mapper, ILogger<GetForecast> logger)
    {
        this._forecastClient = forecastClient;
        this._logger = logger;
        this._mapper = mapper;
    }

    /// <summary>
    /// Gets 5 day / 3 hour forecast
    /// </summary>
    public async Task<Result<ForecastResponseDTO>> Execute(ForecastRequestSettings settings)
    {

        var resultUnmapped = await this._forecastClient.GetForecast(settings);

        var resultMapped = resultUnmapped.Success ? _mapper.Map<ForecastResponseDTO>(resultUnmapped.Value) : null;
     
        return new Result<ForecastResponseDTO>()
        {
            //Success = response.IsSuccessStatusCode,
            Success = resultUnmapped.Success,
            Value = resultMapped
        };
    }

}
