using Microsoft.Extensions.DependencyInjection;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Interfaces.UseCases;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Services.Clients;
using NewsSummary.Core.Services.UseCases;
using System.Reflection.Metadata;

namespace NewsSummary.Core.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static void AddCommonHttpClients(this IServiceCollection services)
    {
        services.ConfigureHttpClient<IForecastClient, WeatherApiForecastClient>("WeatherClient");
        services.ConfigureHttpClient<INewsClient, MediastackNewsClient>("NewsClient");
    }


    private static void ConfigureHttpClient<TInterface, TImplementation>(this IServiceCollection services, string name)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddHttpClient(name).AddTypedClient<TInterface, TImplementation>();
    }

    public static void AddCommonUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetForecastUseCase, GetForecastUseCase>();
        services.AddScoped<IGetNewsUseCase, GetNewsUseCase>();
        services.AddScoped<IGetApiKeyUseCase, GetApiKeyUseCase>();

        services.AddScoped<IGetAllDatabaseEntriesUseCase, GetAllDbEntriesUseCase>();
        services.AddScoped<IAddCityToDbUseCase, AddCityToDbUseCase>();
    }
}
