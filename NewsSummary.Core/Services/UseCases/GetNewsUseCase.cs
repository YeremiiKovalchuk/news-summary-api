using AutoMapper;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Core.Models.News;
using NewsSummary.Core.Models.News.Mediastack;

namespace NewsSummary.Core.Services.UseCases;

public class GetNewsUseCase: IGetNewsUseCase
{
    private readonly ILogger _logger;
    private readonly IUniversalCache _cache;
    private readonly INewsClient _newsClient;
    private readonly IMapper _mapper;
    public GetNewsUseCase(INewsClient newsClient, IUniversalCache cache, IMapper mapper, ILogger<GetNewsUseCase> logger)
    {
        this._newsClient = newsClient;
        this._cache = cache;
        this._logger = logger;
        this._mapper = mapper;
    }

    public async Task<Result<NewsResponseDTO>> Execute(NewsRequestSettings settings)
    {

        var resultUnmapped = await GetUnmappedResult(settings);

        var resultMapped = resultUnmapped.Success ? _mapper.Map<NewsResponseDTO>(resultUnmapped.Value) : null;
     
        return new Result<NewsResponseDTO>()
        {
            //Success = response.IsSuccessStatusCode,
            Success = resultUnmapped.Success,
            Value = resultMapped
        };
    }

    private async Task<Result<NewsRequestDTO>> GetUnmappedResult(NewsRequestSettings settings)
    {
        var key = WebConstants.NewsCacheKey + "_" + settings.Country.ToUpperInvariant();
        var resultFromCache = await this._cache.GetValueAsync<Result<NewsRequestDTO>>(key);
        if (resultFromCache != null)
        {
            return resultFromCache;
        }

        this._logger.LogInformation("Didn`t find news entry in the cache.");
        var result = await this._newsClient.GetNews(settings);

        if (result.Success)
        {
            await this._cache.SetValueAsync(key, result);
        }

        return result;

    }

}
