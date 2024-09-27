using AutoMapper;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.News;
using NewsSummary.Core.Models.News.Mediastack;

namespace NewsSummary.Core.Services.UseCases;

public class GetNewsUseCase: IGetNewsUseCase
{
    private readonly ILogger _logger;
    private readonly INewsClient _newsClient;
    private readonly IMapper _mapper;
    public GetNewsUseCase(INewsClient newsClient, IMapper mapper, ILogger<GetNewsUseCase> logger)
    {
        this._newsClient = newsClient;
        this._logger = logger;
        this._mapper = mapper;
    }

    public async Task<Result<NewsResponseDTO>> Execute(NewsRequestSettings settings)
    {

        var resultUnmapped = await this._newsClient.GetNews(settings);

        var resultMapped = resultUnmapped.Success ? _mapper.Map<NewsResponseDTO>(resultUnmapped.Value) : null;
     
        return new Result<NewsResponseDTO>()
        {
            //Success = response.IsSuccessStatusCode,
            Success = resultUnmapped.Success,
            Value = resultMapped
        };
    }

}
