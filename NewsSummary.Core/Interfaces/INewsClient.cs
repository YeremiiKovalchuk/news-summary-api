using NewsSummary.Core.Models;
using NewsSummary.Core.Models.News.Mediastack;

namespace NewsSummary.Core.Interfaces;

public interface INewsClient
{
    public Task<Result<NewsRequestDTO>> GetNews(NewsRequestSettings settings);
}
