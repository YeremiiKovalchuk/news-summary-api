namespace NewsSummary.Core.Interfaces;

public interface IUniversalCache
{
    public Task<TValue?> GetValue<TValue>(string key);
    public Task SetValue<TValue>(string key, TValue value);
}
