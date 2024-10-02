using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;

namespace NewsSummary.Core.Services.UseCases;

public class AddCityToDbUseCase : IAddCityToDbUseCase
{
    private readonly ICityRepository _cityRepository;
    public AddCityToDbUseCase(ICityRepository rep)
    {
        this._cityRepository = rep;
    }

    public void Execute(CityDto cityInfo)
    {
        this._cityRepository.AddCity(cityInfo);
    }

}
