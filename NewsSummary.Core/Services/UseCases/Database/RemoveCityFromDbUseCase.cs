using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases.Database;

public class RemoveCityFromDbUseCase : IRemoveCityFromDbUseCase
{
    private readonly ICityRepository _cityRepository;
    public RemoveCityFromDbUseCase(ICityRepository rep, ILogger<GetAllDbEntriesUseCase> logger)
    {
        _cityRepository = rep;
    }

    public bool Execute(string cityName)
    {
       return _cityRepository.RemoveCity(cityName);
    }
}
