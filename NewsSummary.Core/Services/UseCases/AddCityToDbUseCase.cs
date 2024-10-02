using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases;

public class AddCityToDbUseCase : IAddCityToDbUseCase
{
    private readonly ICityRepository _cityRepository;
    private readonly ILogger _logger;
    public AddCityToDbUseCase(ICityRepository rep, ILogger<AddCityToDbUseCase> logger)
    {
        this._cityRepository = rep;
        this._logger = logger;
    }

    public void Execute(CityDto cityInfo)
    {
        this._cityRepository.AddCity(cityInfo);
    }

}
