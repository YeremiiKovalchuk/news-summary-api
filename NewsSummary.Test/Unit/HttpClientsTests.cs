using Moq;
using Xunit;

using System.Net;

using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using NewsSummary.Test.Constants;
using MyHashTable.Tests.Extensions;
using NewsSummary.Core.Interfaces.UseCases;
using NewsSummary.Core.Services.UseCases;

namespace MyHashTable.Tests.Unit;

public class HttpClientTests : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;
    public HttpClientTests(WebApplicationFactory<Program> factory)
    {
        this._factory = factory;
    }


    [Fact]
    public async Task WeatherApi_Client_Returns_Correct_Json()
    {
        var testCityName = "Test";
        var mock = new Mock<MyDelegatingHandler>();
        mock.Setup(HttpMethod.Get, $"https://api.openweathermap.org/data/2.5/forecast?lang=en&appid={It.IsAny<string>()}&q={testCityName}", BuildOk(TestConstants.WeatherApiTestResponse));

        var hf = this._factory.CreateDefaultClient(mock.Object);
        var response = await hf.GetAsync($"NewsSummary/GetForecast/?city={testCityName}");


        Assert.Equal(TestConstants.WeatherApiTestResponse, await response.Content.ReadAsStringAsync());
    }

    public async Task Mediastack_Client_Returns_Correct_Json()
    {
        var testCountryName = "Test";
        var mock = new Mock<MyDelegatingHandler>();
        mock.Setup(HttpMethod.Get, $"https://api.mediastack.com/v1/news?access_key={It.IsAny<string>}&countries={testCountryName}", BuildOk(TestConstants.MediastackTestResponse));

        var hf = this._factory.CreateDefaultClient(mock.Object);
        var response = await hf.GetAsync($"NewsSummary/GetNews/?country={testCountryName}");


        Assert.Equal(TestConstants.MediastackTestResponse, await response.Content.ReadAsStringAsync());
    }

    private static HttpResponseMessage BuildOk(string content)
    {
        return new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(content),
        };
    }

}

public class MyDelegatingHandler : DelegatingHandler
{

}