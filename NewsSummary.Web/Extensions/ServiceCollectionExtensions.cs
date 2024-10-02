using NewsSummary.Core.Constants;

namespace NewsSummary.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWebDefaults(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            WebConstants.ApplyCommonSerializerOptions(options.JsonSerializerOptions);
        });


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
