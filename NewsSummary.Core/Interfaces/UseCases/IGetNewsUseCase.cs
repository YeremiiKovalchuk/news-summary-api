using NewsSummary.Core.Models.News.Mediastack;
using NewsSummary.Core.Models.News;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases;

public interface IGetNewsUseCase
{
    public Task<Result<NewsResponseDTO>> Execute(NewsRequestSettings settings);
}
