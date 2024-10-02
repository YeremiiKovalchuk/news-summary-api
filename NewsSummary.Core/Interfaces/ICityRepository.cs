using NewsSummary.Core.Models;

namespace NewsSummary.Core.Interfaces;

public interface ICityRepository
{
    public List<CityDto?> GetAllCities();

    public bool TryGetCityByName(string cityId, out CityDto? city);

    public bool AddCity(CityDto cityInfo);

    public bool RemoveCity(string cityName);

    public bool UpdateCity(CityDto cityInfo);
}
