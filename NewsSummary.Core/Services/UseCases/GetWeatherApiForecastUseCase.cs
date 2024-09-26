using AutoMapper;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using System.Text.Json;

namespace NewsSummary.Core.Services.UseCases;

public class GetWeatherApiForecastUseCase: IGetForecastUseCase
{
    private readonly ILogger _logger;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    public GetWeatherApiForecastUseCase(HttpClient client, IMapper mapper, ILogger<GetWeatherApiForecastUseCase> logger)
    {
        this._client = client;
        this._logger = logger;
        this._mapper = mapper;
    }

    /// <summary>
    /// Gets 5 day / 3 hour forecast (24*3 = 72 entries)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result<ForecastResponseDTO>> Get(ForecastRequestSettings request)
    {
        var requestString = $"api.openweathermap.org/data/2.5/forecast?lat={request.Latitude}&lon={request.Longitude}&lang={request.Lang}&appid={request.ApiKey}";
        _logger.LogInformation("Requesting weather forecast from external API");
        //var response = await _client.GetAsync(requestString);

        //var jsonForecast = await response.Content.ReadAsStringAsync();
        var jsonForecast = WebConstants.TestWeatherApiResponse;

        var _serializerOptions = new JsonSerializerOptions();
        _serializerOptions.PropertyNameCaseInsensitive = true;
        _serializerOptions.PropertyNamingPolicy = null;
        var resultUnmapped = JsonSerializer.Deserialize<ForecastRequestDTO>(jsonForecast, _serializerOptions);
        var resultMapped = _mapper.Map<ForecastResponseDTO>(resultUnmapped);

        return new Result<ForecastResponseDTO>()
        {
            //Success = response.IsSuccessStatusCode,
            Success = true,
            Value = resultMapped
        };
    }

}
