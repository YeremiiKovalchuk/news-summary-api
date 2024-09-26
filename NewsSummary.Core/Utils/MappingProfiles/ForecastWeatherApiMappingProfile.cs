using AutoMapper;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Core.Models.Forecast;

namespace NewsSummary.Core.Utils.MappingProfiles;

public class ForecastWeatherApiMappingProfile: Profile
{
    public ForecastWeatherApiMappingProfile()
    {
        CreateMap<ForecastRequestDTO, ForecastResponseDTO>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.Forecasts, opt => opt.MapFrom(src => src.List));

        CreateMap<ForecastEntry, CommonForecastEntry>()
            .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Main.Temp))
            .ForMember(dest => dest.Weather, opt => opt.MapFrom(src => src.Weather.FirstOrDefault().Main));
    }
}
