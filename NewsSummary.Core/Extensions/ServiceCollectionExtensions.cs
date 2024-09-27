using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Services.Clients;
using NewsSummary.Core.Services.UseCases;

namespace NewsSummary.Core.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static void AddCommonHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient("WeatherClient").AddTypedClient<IForecastClient, WeatherApiForecastClient>(); 
        services.AddHttpClient("NewsClient").AddTypedClient<INewsClient, MediastackNewsClient>();
    }

    public static void AddCommonUseCases(this IServiceCollection services)
    {
        services.AddSingleton<IGetForecastUseCase, GetForecast>();
        services.AddSingleton<IGetNewsUseCase, GetNewsUseCase>();
        services.AddSingleton<IGetApiKeyUseCase, GetApiKeyUseCase>();
    }
}
