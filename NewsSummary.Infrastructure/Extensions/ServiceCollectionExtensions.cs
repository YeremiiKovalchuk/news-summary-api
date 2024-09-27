using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Services;
using NewsSummary.Infrastructure.Services.MappingProfiles;

namespace NewsSummary.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddCommonCaching(this IServiceCollection services, ConfigurationManager manager)
    {
        services.AddStackExchangeRedisCache(options => 
        {
            options.Configuration = manager.GetConnectionString("Redis");
            options.InstanceName = "SummaryAPI_";
        });
        
        services.AddSingleton<IUniversalCache, UniversalCache>(); 
    }

    public static void AddCommonAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ForecastWeatherApiMappingProfile));
    }

}
