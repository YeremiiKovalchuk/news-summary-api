using Microsoft.Extensions.DependencyInjection;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Models;
using NewsSummary.Infrastructure.Services;
using NewsSummary.Infrastructure.Services.MappingProfiles;
using StackExchange.Redis;

namespace NewsSummary.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddRedis(this IServiceCollection services, string connectionString, RedisRetryPolicy retrySettings)
    {

        services.AddSingleton<IDatabase>(cfg =>
        {
            var configOptions = new ConfigurationOptions()
            {
                EndPoints = 
                { 
                    {
                        connectionString
                    } 
                },

                AbortOnConnectFail = false,
                ConnectRetry = retrySettings.RetryCount,
                ReconnectRetryPolicy = new ExponentialRetry(retrySettings.ExponentialRetryTimeout)
            };
  
            IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configOptions);
            return multiplexer.GetDatabase();
           
        });

        services.AddSingleton<ICacheStore, CacheStore>();
    }

    public static void AddCommonAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ForecastWeatherApiMappingProfile));
    }

}
