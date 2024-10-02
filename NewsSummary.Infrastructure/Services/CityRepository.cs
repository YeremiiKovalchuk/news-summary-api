using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Models;
using NewsSummary.Infrastructure.Data;

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
        if (this._dbContext.SaveChanges() > 0)
        {
            return true;
        }
        return false;   
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
        var city = _dbContext.Cities.FirstOrDefault(c => c.CityName == cityName);
        if (city == null)
        { 
            return false;
        }

        this._dbContext.Cities.Remove(city);
        this._dbContext.SaveChanges();
        return true;
    }

    public bool UpdateCity(CityDto cityInfo)
    {
        this._logger.LogDebug($"Trying to update city with name {cityInfo.CityName} in Db");
        this._dbContext.Cities.Update(cityInfo);
        if (this._dbContext.SaveChanges() > 0)
        {
            return true;
        }
        return false;
    }
}
