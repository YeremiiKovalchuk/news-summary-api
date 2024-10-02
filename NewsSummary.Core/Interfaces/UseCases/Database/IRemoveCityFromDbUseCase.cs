namespace NewsSummary.Core.Interfaces.UseCases.Database;

public interface IRemoveCityFromDbUseCase
{
    public void Execute(string cityName);
}
