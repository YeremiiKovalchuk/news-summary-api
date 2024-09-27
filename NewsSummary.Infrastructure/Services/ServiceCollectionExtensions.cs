using Microsoft.Extensions.DependencyInjection;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Services.MappingProfiles;

namespace NewsSummary.Infrastructure.Services;

public static class ServiceCollectionExtensions
{

    public static void AddCommonCaching(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache(); //inf
        services.AddSingleton<IUniversalCache, UniversalCache>(); //inf
    }

    public static void AddCommonAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ForecastWeatherApiMappingProfile));
    }

}
