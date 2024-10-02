using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Web.Controllers;
using Xunit;
using System;
using System.Collections.Generic;
using NewsSummary.Core.Models;

public class DatabaseControllerTests
{
    private readonly DatabaseController _controller;
    private readonly Mock<IGetAllDatabaseEntriesUseCase> _getAllDatabaseEntriesUseCaseMock;
    private readonly Mock<IAddCityToDbUseCase> _addCityToDbUseCaseMock;
    private readonly Mock<IRemoveCityFromDbUseCase> _removeCityFromDbUseCaseMock;
    private readonly Mock<IUpdateCityInDbUseCase> _updateCityInDbUseCaseMock;

    public DatabaseControllerTests()
    {
        _getAllDatabaseEntriesUseCaseMock = new Mock<IGetAllDatabaseEntriesUseCase>();
        _addCityToDbUseCaseMock = new Mock<IAddCityToDbUseCase>();
        _removeCityFromDbUseCaseMock = new Mock<IRemoveCityFromDbUseCase>();
        _updateCityInDbUseCaseMock = new Mock<IUpdateCityInDbUseCase>();

        _controller = new DatabaseController(
            _getAllDatabaseEntriesUseCaseMock.Object,
            _addCityToDbUseCaseMock.Object,
            _removeCityFromDbUseCaseMock.Object,
            _updateCityInDbUseCaseMock.Object);
    }

    [Fact]
    public void GetAllCities_ReturnsOk_WhenEntriesExist()
    {
        // Arrange
        var cities = new List<CityDto> { new CityDto { CityName = "City1" } };
        _getAllDatabaseEntriesUseCaseMock.Setup(x => x.Execute()).Returns(cities);

        // Act
        var result = _controller.GetAllCities();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(cities, okResult.Value);
    }

    [Fact]
    public void GetAllCities_ReturnsNoContent_WhenNoEntriesExist()
    {
        // Arrange
        _getAllDatabaseEntriesUseCaseMock.Setup(x => x.Execute()).Returns((List<CityDto>)null);

        // Act
        var result = _controller.GetAllCities();

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public void AddNewCity_ReturnsOk_WhenCityAddedSuccessfully()
    {
        // Arrange
        var city = new CityDto { CityName = "New City" };

        // Act
        var result = _controller.AddNewCity(city);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void AddNewCity_ReturnsTeapot_WhenExceptionThrown()
    {
        // Arrange
        var city = new CityDto { CityName = "New City" };
        _addCityToDbUseCaseMock.Setup(x => x.Execute(It.IsAny<CityDto>())).Throws(new Exception());

        // Act
        var result = _controller.AddNewCity(city);

        // Assert
        var teapotResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(418, teapotResult.StatusCode);
    }

    [Fact]
    public void DeleteCity_ReturnsOk_WhenCityDeletedSuccessfully()
    {
        // Act
        var result = _controller.DeleteCity("CityName");

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void DeleteCity_ReturnsTeapot_WhenExceptionThrown()
    {
        // Arrange
        _removeCityFromDbUseCaseMock.Setup(x => x.Execute(It.IsAny<string>())).Throws(new Exception());

        // Act
        var result = _controller.DeleteCity("CityName");

        // Assert
        var teapotResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(418, teapotResult.StatusCode);
    }

    [Fact]
    public void UpdateCity_ReturnsOk_WhenCityUpdatedSuccessfully()
    {
        // Arrange
        var city = new CityDto { CityName = "Updated City" };

        // Act
        var result = _controller.UpdateCity(city);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void UpdateCity_ReturnsTeapot_WhenExceptionThrown()
    {
        // Arrange
        var city = new CityDto { CityName = "Updated City" };
        _updateCityInDbUseCaseMock.Setup(x => x.Execute(It.IsAny<CityDto>())).Throws(new Exception());

        // Act
        var result = _controller.UpdateCity(city);

        // Assert
        var teapotResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(418, teapotResult.StatusCode);
    }
}
