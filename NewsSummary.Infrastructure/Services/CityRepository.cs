using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Infrastructure.Data;
using System.Runtime;

namespace NewsSummary.Infrastructure.Services;

public class CityRepository : ICityRepository
{
    private readonly SummaryDBContext _dbContext;
    private readonly ILogger _logger;
    public CityRepository(SummaryDBContext summaryDBContext, ILogger<CityRepository> logger)
    {
        this._dbContext = summaryDBContext;
        this._logger = logger;
    }
    public bool AddCity(CityDto cityInfo)
    {
        this._logger.LogDebug($"Trying to add city with name {cityInfo.CityName} to Db.");
        this._dbContext.Cities.Add(cityInfo);
        this._dbContext.SaveChanges();
        return true;
    }

    public List<CityDto?> GetAllCities()
    {
        this._logger.LogDebug($"Trying to get all cities from Db.");
        return _dbContext.Cities.AsNoTracking().ToList<CityDto?>();
    }

    public bool TryGetCityByName(string cityName, out CityDto? city)
    {
        this._logger.LogDebug($"Trying to get city with name {cityName} from Db");
        city = _dbContext.Cities.AsNoTracking().FirstOrDefault(c => c.CityName == cityName);
        if (city == null)
        {
            return false;
        }
        return true;
    }

    public bool RemoveCity(string cityName)
    {
        this._logger.LogDebug($"Trying to remove city with name {cityName} from Db");
        var deleteCity = new CityDto() { CityName = cityName };
        this._dbContext.Cities.Remove(deleteCity);
        this._dbContext.SaveChanges();
        return true;
    }

    public bool UpdateCity(CityDto cityInfo)
    {
        this._logger.LogDebug($"Trying to update city with name {cityInfo.CityName} in Db");
        this._dbContext.Cities.Update(cityInfo);
        this._dbContext.SaveChanges();
        return true;
    }
}
