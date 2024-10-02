namespace NewsSummary.Core.Interfaces;

public interface ICacheStore
{
    public Task<TValue?> GetValueAsync<TValue>(string key);
    public Task SetValueAsync<TValue>(string key, TValue value);
}
