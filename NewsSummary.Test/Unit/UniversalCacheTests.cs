using Microsoft.Extensions.Logging;
using Moq;
using NewsSummary.Infrastructure.Constants;
using NewsSummary.Infrastructure.Services;
using StackExchange.Redis;
using System.Text.Json;
using Xunit;

namespace NewsSummary.Test.Unit;

public class UniversalCacheTests
{
    [Fact]
    public async Task Cache_Works_Correctly()
    {
        const string testkey = "69";
        const string testvalue = "Nice";

        var mockDatabase = new Mock<IDatabase>();
  
        mockDatabase.Setup<RedisValue>(_ => _.StringGet(CacheConstants.NamePrefix + testkey, CommandFlags.None)).Returns(JsonSerializer.Serialize(testvalue));

        var mockMultiplexer = new Mock<IConnectionMultiplexer>();
        mockMultiplexer.Setup(_ => _.IsConnected).Returns(false);

        mockMultiplexer
            .Setup(_ => _.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
            .Returns(mockDatabase.Object);

        var cache = new CacheStore(mockMultiplexer.Object.GetDatabase(), new Mock<ILogger<CacheStore>>().Object);

 
        Assert.Equal(testvalue, await cache.GetValueAsync<string>(testkey));
    }
}
