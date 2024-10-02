using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;
using NewsSummary.Web.Controllers;
using Xunit;
using System.Collections.Generic;

namespace NewsSummary.Test.Unit;

public class DatabaseControllerTests
{
    private readonly DatabaseController _controller;
    private readonly Mock<IGetAllDatabaseEntriesUseCase> _mockGetAllDatabaseEntriesUseCase;
    private readonly Mock<IAddCityToDbUseCase> _mockAddCityToDbUseCase;
    private readonly Mock<IRemoveCityFromDbUseCase> _mockRemoveCityFromDbUseCase;
    private readonly Mock<IUpdateCityInDbUseCase> _mockUpdateCityInDbUseCase;

    public DatabaseControllerTests()
    {
        _mockGetAllDatabaseEntriesUseCase = new Mock<IGetAllDatabaseEntriesUseCase>();
        _mockAddCityToDbUseCase = new Mock<IAddCityToDbUseCase>();
        _mockRemoveCityFromDbUseCase = new Mock<IRemoveCityFromDbUseCase>();
        _mockUpdateCityInDbUseCase = new Mock<IUpdateCityInDbUseCase>();

        _controller = new DatabaseController(
            _mockGetAllDatabaseEntriesUseCase.Object,
            _mockAddCityToDbUseCase.Object,
            _mockRemoveCityFromDbUseCase.Object,
            _mockUpdateCityInDbUseCase.Object
        );
    }

    [Fact]
    public void GetAllCities_ShouldReturnOk_WhenCitiesExist()
    {
        // Arrange
        var cityList = new List<CityDto>
    {
        new CityDto { CityName = "City1" },
        new CityDto { CityName = "City2" }
    };
        _mockGetAllDatabaseEntriesUseCase.Setup(x => x.Execute()).Returns(cityList);

        // Act
        var result = _controller.GetAllCities();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CityDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public void GetAllCities_ShouldReturnNoContent_WhenNoCitiesExist()
    {
        // Arrange
        _mockGetAllDatabaseEntriesUseCase.Setup(x => x.Execute()).Returns((List<CityDto>)null);

        // Act
        var result = _controller.GetAllCities();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void AddNewCity_ShouldReturnOk_WhenCityIsAdded()
    {
        // Arrange
        var cityDto = new CityDto { CityName = "City1" };
        _mockAddCityToDbUseCase.Setup(x => x.Execute(cityDto)).Returns(true);

        // Act
        var result = _controller.AddNewCity(cityDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void AddNewCity_ShouldReturnNoContent_WhenCityIsNotAdded()
    {
        // Arrange
        var cityDto = new CityDto { CityName = "City1" };
        _mockAddCityToDbUseCase.Setup(x => x.Execute(cityDto)).Returns(false);

        // Act
        var result = _controller.AddNewCity(cityDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCity_ShouldReturnOk_WhenCityIsDeleted()
    {
        // Arrange
        var cityName = "City1";
        _mockRemoveCityFromDbUseCase.Setup(x => x.Execute(cityName)).Returns(true);

        // Act
        var result = _controller.DeleteCity(cityName);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void DeleteCity_ShouldReturnNoContent_WhenCityIsNotDeleted()
    {
        // Arrange
        var cityName = "City1";
        _mockRemoveCityFromDbUseCase.Setup(x => x.Execute(cityName)).Returns(false);

        // Act
        var result = _controller.DeleteCity(cityName);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateCity_ShouldReturnOk_WhenCityIsUpdated()
    {
        // Arrange
        var cityDto = new CityDto { CityName = "City1" };
        _mockUpdateCityInDbUseCase.Setup(x => x.Execute(cityDto)).Returns(true);

        // Act
        var result = _controller.UpdateCity(cityDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void UpdateCity_ShouldReturnNoContent_WhenCityIsNotUpdated()
    {
        // Arrange
        var cityDto = new CityDto { CityName = "City1" };
        _mockUpdateCityInDbUseCase.Setup(x => x.Execute(cityDto)).Returns(false);

        // Act
        var result = _controller.UpdateCity(cityDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

}
