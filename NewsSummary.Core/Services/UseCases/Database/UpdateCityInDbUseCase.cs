using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases.Database;

public class UpdateCityInDbUseCase : IUpdateCityInDbUseCase
{
    private readonly ICityRepository _cityRepository;
    public UpdateCityInDbUseCase(ICityRepository rep)
    {
        _cityRepository = rep;
    }

    public bool Execute(CityDto cityInfo)
    {
        return _cityRepository.UpdateCity(cityInfo);
    }
}
