using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Extensions;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using Polly;
using Polly.Retry;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace NewsSummary.Core.Services.Clients;

public class WeatherApiForecastClient : IForecastClient
{
    private readonly HttpClient _httpClient;
    private readonly IGetApiKeyUseCase _apiKeyUseCase;
    private readonly IGetAllDatabaseEntriesUseCase _getAllCitiesUseCase;
    private readonly ILogger _logger;

    public WeatherApiForecastClient(HttpClient httpClient, IGetApiKeyUseCase getApiKeyUseCase,IGetAllDatabaseEntriesUseCase getAllDatabaseEntriesUseCase,ILogger<WeatherApiForecastClient> logger)
    {
        this._httpClient = httpClient;
        this._apiKeyUseCase = getApiKeyUseCase;
        this._getAllCitiesUseCase = getAllDatabaseEntriesUseCase;
        this._logger = logger;
    }
    
    public async Task<Result<ForecastRequestDTO>> GetForecast(ForecastRequestSettings settings)
    {
        _logger.LogInformation("Requesting weather forecast from WeatherApi.");

        var city = this._getAllCitiesUseCase.Execute().FirstOrDefault(c => c.CityName == settings.City);

        _logger.LogDebug(city == null ? "City is not in the DB, requesting by name." : "City is in DB, requesting by coordinates");

        var requestStringBuilder = new StringBuilder($"https://api.openweathermap.org/data/2.5/forecast?lang=en&appid={this._apiKeyUseCase.Execute("WeatherApi")}&units=metric&");
        requestStringBuilder.Append(city == null? $"q={settings.City}" : $"lat={city.Latitude.ToString().Replace(",",".")}&lon={city.Longitude.ToString().Replace(",", ".")}");
        var requestString = requestStringBuilder.ToString();

        var policy = Policies.WaitAndRetry;
        var policyResponse = await policy.ExecuteAndCaptureAsync( () => _httpClient.GetAsync(new Uri(requestString)));
        var response = policyResponse.Result;

        var forecast = await response.Content.ReadFromJsonAsync<ForecastRequestDTO>(WebConstants.CommonJsonSerializerOptions);

        return new Result<ForecastRequestDTO>()
        {
            Value = forecast,
            Success = response.IsSuccessStatusCode
        };

       // var jsonForecast = WebConstants.TestWeatherApiResponse;

    }
}
