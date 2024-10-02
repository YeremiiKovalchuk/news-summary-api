using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IGetAllDatabaseEntriesUseCase
{
    public List<CityDto> Execute();
}
