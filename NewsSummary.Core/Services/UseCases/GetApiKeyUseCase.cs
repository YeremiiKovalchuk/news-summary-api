using Microsoft.Extensions.Options;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases;

public class GetApiKeyUseCase: IGetApiKeyUseCase
{
    private readonly ApiKeys _apiKeys;

    public GetApiKeyUseCase(IOptions<ApiKeys> apiKeys)
    {
        _apiKeys = apiKeys.Value;
    }

    public string Execute(string api)
    {
        return api switch
        {
            "WeatherApi" => _apiKeys.WeatherApiKey,
            "Mediastack" => _apiKeys.MediastackKey,
            _ => throw new ArgumentException($"{api} is invalid API")
        };
    }
}
