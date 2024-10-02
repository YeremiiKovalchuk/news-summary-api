using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using NewsSummary.Core.Interfaces.UseCases;
using NewsSummary.Web;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Core.Models.News.Mediastack;
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Core.Models.News;
using NewsSummary.Core.Models;

namespace NewsSummary.Test.Unit;
public class SummaryControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IGetForecastUseCase> _getForecastUseCaseMock;
    private readonly Mock<IGetNewsUseCase> _getNewsUseCaseMock;

    public SummaryControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _getForecastUseCaseMock = new Mock<IGetForecastUseCase>();
        _getNewsUseCaseMock = new Mock<IGetNewsUseCase>();
    }

    [Fact]
    public async Task GetForecast_ReturnsOk_WithForecastData()
    {
        // Arrange
        var forecastResponse = new ForecastResponseDTO
        {
            CityName = "Calp",
            Forecasts = new List<CommonForecastEntry> { new() { DtTxt = "2023-10-01", Temp = 22.5 } }
        };

        _getForecastUseCaseMock.Setup(x => x.Execute(It.IsAny<ForecastRequestSettings>()))
            .ReturnsAsync(new Result<ForecastResponseDTO> { Success = true, Value = forecastResponse });

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton(_getForecastUseCaseMock.Object);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync("/Summary/GetForecast?city=Calp&language=en");

        // Assert
        response.EnsureSuccessStatusCode(); // Status code 200-299
        var result = await response.Content.ReadFromJsonAsync<ForecastResponseDTO>();
        Assert.NotNull(result);
        Assert.Equal("Calp", result.CityName);
    }

    [Fact]
    public async Task GetForecast_Returns418_WhenForecastFails()
    {
        // Arrange
        _getForecastUseCaseMock.Setup(x => x.Execute(It.IsAny<ForecastRequestSettings>()))
            .ReturnsAsync(new Result<ForecastResponseDTO> { Success = false });

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton(_getForecastUseCaseMock.Object);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync("/Summary/GetForecast?city=Calp&language=en");

        // Assert
        Assert.Equal(418, (int)response.StatusCode);
    }

    [Fact]
    public async Task GetNews_ReturnsOk_WithNewsData()
    {
        // Arrange
        var newsResponse = new NewsResponseDTO
        {
            News = new List<CommonNewsEntry> { new(){ Title = "Breaking News" } }
        };

        _getNewsUseCaseMock.Setup(x => x.Execute(It.IsAny<NewsRequestSettings>()))
            .ReturnsAsync(new Result<NewsResponseDTO> { Success = true, Value = newsResponse });

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton(_getNewsUseCaseMock.Object);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync("/Summary/GetNews?country=ua");

        // Assert
        response.EnsureSuccessStatusCode(); // Status code 200-299
        var result = await response.Content.ReadFromJsonAsync<NewsResponseDTO>();
        Assert.NotNull(result);
        Assert.Single(result.News);
        Assert.Equal("Breaking News", result.News[0].Title);
    }

    [Fact]
    public async Task GetNews_Returns418_WhenNewsFails()
    {
        // Arrange
        _getNewsUseCaseMock.Setup(x => x.Execute(It.IsAny<NewsRequestSettings>()))
            .ReturnsAsync(new Result<NewsResponseDTO> { Success = false });

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton(_getNewsUseCaseMock.Object);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync("/Summary/GetNews?country=ua");

        // Assert
        Assert.Equal(418, (int)response.StatusCode);
    }
}