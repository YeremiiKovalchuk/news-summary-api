namespace NewsSummary.Core.Interfaces;

public interface IUniversalCache
{
    public Task<TValue?> GetValueAsync<TValue>(string key);
    public Task SetValueAsync<TValue>(string key, TValue value);
}
