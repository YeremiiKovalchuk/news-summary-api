using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases;

public class RemoveCityFromDbUseCase : IRemoveCityFromDbUseCase
{
    private readonly ICityRepository _cityRepository;
    public RemoveCityFromDbUseCase(ICityRepository rep, ILogger<GetAllDbEntriesUseCase> logger)
    {
        this._cityRepository = rep;
    }

    public void Execute(string cityName)
    {
        _cityRepository.RemoveCity(cityName);
    }
}
