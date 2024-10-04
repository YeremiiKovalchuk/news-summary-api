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

    public async Task<bool> AddCityAsync(CityDto cityInfo)
    {
        this._logger.LogDebug($"Trying to add city with name {cityInfo.CityName} to Db.");
        await this._dbContext.Cities.AddAsync(cityInfo);
        if (await this._dbContext.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;   
    }

    public async Task<List<CityDto?>> GetAllCitiesAsync()
    {
        this._logger.LogDebug($"Trying to get all cities from Db.");
        return await _dbContext.Cities.AsNoTracking().ToListAsync<CityDto?>();
    }

    public async Task<CityDto?> GetCityByName(string cityName)
    {
        this._logger.LogDebug($"Trying to get city with name {cityName} from Db");
        return await _dbContext.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.CityName == cityName);
    }

    public async Task<bool> RemoveCity(string cityName)
    {
        this._logger.LogDebug($"Trying to remove city with name {cityName} from Db");
        var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.CityName == cityName);
        if (city == null)
        { 
            return false;
        }

        this._dbContext.Cities.Remove(city);
        await this._dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateCity(CityDto cityInfo)
    {
        this._logger.LogDebug($"Trying to update city with name {cityInfo.CityName} in Db");
        this._dbContext.Cities.Update(cityInfo);
        if (await this._dbContext.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;
    }
}
