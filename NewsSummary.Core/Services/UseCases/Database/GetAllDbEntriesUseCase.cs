using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases.Database;

public class GetAllDbEntriesUseCase : IGetAllDatabaseEntriesUseCase
{
    private readonly ICityRepository _cityRepository;
    public GetAllDbEntriesUseCase(ICityRepository rep)
    {
        _cityRepository = rep;
    }

    public List<CityDto> Execute()
    {
        return _cityRepository.GetAllCities();
    }

}
