using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IAddCityToDbUseCase
{
    public void Execute(CityDto city);
}
