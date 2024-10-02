using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IUpdateCityInDbUseCase
{
    public bool Execute(CityDto cityInfo);
}
