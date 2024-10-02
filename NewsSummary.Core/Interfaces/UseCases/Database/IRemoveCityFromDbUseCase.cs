namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IRemoveCityFromDbUseCase
{
    public bool Execute(string cityName);
}
