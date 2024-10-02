using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Core.Models.News.Mediastack;
using System.Net.Http.Json;
using System.Text.Json;

namespace NewsSummary.Core.Services.Clients;

public class MediastackNewsClient : INewsClient
{
    private readonly HttpClient _httpClient;
    private readonly IGetApiKeyUseCase _getApiKeyUseCase;
    private readonly ILogger _logger;

    public MediastackNewsClient(HttpClient httpClient, IGetApiKeyUseCase getApiKeyUseCase, IGetAllDatabaseEntriesUseCase getCities,ILogger<MediastackNewsClient> logger)
    {
        this._httpClient = httpClient;
        this._getApiKeyUseCase = getApiKeyUseCase;
        this._logger = logger;
    }
    
    public async Task<Result<NewsRequestDTO>> GetNews(NewsRequestSettings settings)
    {
        var requestString = $"https://api.mediastack.com/v1/news?access_key={this._getApiKeyUseCase.Execute("Mediastack")}&countries={settings.Country}";
        _logger.LogInformation("Requesting news Mediastack.");

        var policy = Policies.WaitAndRetry;
        var policyResponse = await policy.ExecuteAndCaptureAsync(() => _httpClient.GetAsync(new Uri(requestString)));
        var response = policyResponse.Result;

        var news = await response.Content.ReadFromJsonAsync<NewsRequestDTO>(WebConstants.CommonJsonSerializerOptions);

        return new Result<NewsRequestDTO>()
        {
            Value = news,
            Success = response.IsSuccessStatusCode
        };
    }
}
