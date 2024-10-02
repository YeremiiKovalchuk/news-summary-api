using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IAddCityToDbUseCase
{
    public bool Execute(CityDto city);
}
