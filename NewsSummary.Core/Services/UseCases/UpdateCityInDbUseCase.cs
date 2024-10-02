using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases;

public class UpdateCityInDbUseCase: IUpdateCityInDbUseCase
{
    private readonly ICityRepository _cityRepository;
    public UpdateCityInDbUseCase(ICityRepository rep)
    {
        this._cityRepository = rep;
    }

    public void Execute(CityDto cityInfo)
    {
        _cityRepository.UpdateCity(cityInfo);
    }
}
