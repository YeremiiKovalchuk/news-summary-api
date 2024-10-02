namespace NewsSummary.Core.Interfaces.UseCases;

public interface IGetApiKeyUseCase
{
    /// <summary>
    /// Gets key for <paramref name="api"/>
    /// </summary>
    /// <param name="api"></param>
    /// <returns></returns>
    public string Execute(string api);
}
