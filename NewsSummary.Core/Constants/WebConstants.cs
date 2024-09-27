using System.Text.Json;

namespace NewsSummary.Core.Constants;

public static class WebConstants
{
    public static readonly JsonSerializerOptions CommonJsonSerializerOptions = new() 
    { 
        PropertyNameCaseInsensitive = true, 
        PropertyNamingPolicy = null 
    };

    public static readonly Action<JsonSerializerOptions> ApplyCommonSerializerOptions = options =>
    {
        options.PropertyNameCaseInsensitive = CommonJsonSerializerOptions.PropertyNameCaseInsensitive;
        options.PropertyNamingPolicy = CommonJsonSerializerOptions.PropertyNamingPolicy;
    };
}