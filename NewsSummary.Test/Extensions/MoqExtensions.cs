using Moq.Protected;
using Moq;
using System;
using MyHashTable.Tests.Unit;
using System.Diagnostics;
namespace MyHashTable.Tests.Extensions;

public static class Extensions
{
    public static void Setup(this Mock<MyDelegatingHandler> handler, HttpMethod httpMethod, string path, HttpResponseMessage result)
    {
        handler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(s => s.Method == httpMethod && s.RequestUri != null),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(result);
    }
}
