using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IUpdateCityInDbUseCase
{
    void Execute(CityDto cityInfo);
}
