using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases;

public class GetAllDbEntriesUseCase: IGetAllDatabaseEntriesUseCase
{
    private readonly ICityRepository _cityRepository;
    private readonly ILogger _logger;
    public GetAllDbEntriesUseCase(ICityRepository rep, ILogger<GetAllDbEntriesUseCase> logger)
    {
        this._cityRepository = rep;
        this._logger = logger;
    }

    public List<CityDto> Execute()
    {
        return _cityRepository.GetAllCities();
    }

}
