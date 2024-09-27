using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Extensions;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using System.Net.Http.Json;
using System.Text.Json;

namespace NewsSummary.Core.Services.Clients;

public class WeatherApiForecastClient : IForecastClient
{
    private readonly HttpClient _httpClient;
    private readonly IGetApiKeyUseCase _apiKeyUseCase;
    private readonly ILogger _logger;

    public WeatherApiForecastClient(HttpClient httpClient, IGetApiKeyUseCase getApiKeyUseCase,ILogger<WeatherApiForecastClient> logger)
    {
        this._httpClient = httpClient;
        this._apiKeyUseCase = getApiKeyUseCase;
        this._logger = logger;
    }
    
    public async Task<Result<ForecastRequestDTO>> GetForecast(ForecastRequestSettings settings)
    {
        var requestString = $"https://api.openweathermap.org/data/2.5/forecast?&q={settings.City}&lang={settings.Lang}&units=metric&appid={this._apiKeyUseCase.Execute("WeatherApi")}";
        _logger.LogInformation("Requesting weather forecast from WeatherApi.");
       
        var response = await _httpClient.GetAsync(new Uri(requestString));

        var forecast = await response.Content.ReadFromJsonAsync<ForecastRequestDTO>(WebConstants.CommonJsonSerializerOptions);

        return new Result<ForecastRequestDTO>()
        {
            Value = forecast,
            Success = response.IsSuccessStatusCode
        };

       // var jsonForecast = WebConstants.TestWeatherApiResponse;

    }
}
