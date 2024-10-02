using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NewsSummary.Core.Models;
using NewsSummary.Infrastructure.Data;
using NewsSummary.Infrastructure.Services;
using Xunit;

namespace NewsSummary.Test.Unit;

public class CityRepositoryTests
{
    private readonly CityRepository _repository;
    private readonly Mock<SummaryDBContext> _mockDbContext;
    private readonly Mock<DbSet<CityDto>> _mockCitySet;
    private readonly Mock<ILogger<CityRepository>> _mockLogger;

    public CityRepositoryTests()
    {
        _mockDbContext = new Mock<SummaryDBContext>();
        _mockCitySet = new Mock<DbSet<CityDto>>();
        _mockLogger = new Mock<ILogger<CityRepository>>();

        _mockDbContext.Setup(m => m.Cities).Returns(_mockCitySet.Object);
        _repository = new CityRepository(_mockDbContext.Object, _mockLogger.Object);
    }

    private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> elements) where T : class
    {
        var queryable = elements.AsQueryable();
        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        return dbSet;
    }

    [Fact]
    public void AddCity_ShouldReturnTrue_WhenCityIsAdded()
    {
        // Arrange
        var city = new CityDto { CityName = "TestCity" };
        _mockCitySet.Setup(m => m.Add(It.IsAny<CityDto>()));
        _mockDbContext.Setup(m => m.SaveChanges()).Returns(1);

        // Act
        var result = _repository.AddCity(city);

        // Assert
        _mockCitySet.Verify(m => m.Add(It.IsAny<CityDto>()), Times.Once);
        _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public void AddCity_ShouldReturnFalse_WhenSaveChangesFails()
    {
        // Arrange
        var city = new CityDto { CityName = "TestCity" };
        _mockCitySet.Setup(m => m.Add(It.IsAny<CityDto>()));
        _mockDbContext.Setup(m => m.SaveChanges()).Returns(0);

        // Act
        var result = _repository.AddCity(city);

        // Assert
        _mockCitySet.Verify(m => m.Add(It.IsAny<CityDto>()), Times.Once);
        _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        Assert.False(result);
    }

    [Fact]
    public void GetAllCities_ShouldReturnListOfCities()
    {
        // Arrange
        var cities = new List<CityDto>
{
    new CityDto { CityName = "City1" },
    new CityDto { CityName = "City2" }
};
        var mockCitySet = CreateMockDbSet(cities);
        _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

        // Act
        var result = _repository.GetAllCities();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("City1", result[0]?.CityName);
        Assert.Equal("City2", result[1]?.CityName);
    }

    [Fact]
    public void TryGetCityByName_ShouldReturnTrue_WhenCityExists()
    {
        // Arrange
        var city = new CityDto { CityName = "City1" };
        var cities = new List<CityDto> { city };
        var mockCitySet = CreateMockDbSet(cities);
        _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

        // Act
        var result = _repository.TryGetCityByName("City1", out var retrievedCity);

        // Assert
        Assert.True(result);
        Assert.NotNull(retrievedCity);
        Assert.Equal("City1", retrievedCity.CityName);
    }

    [Fact]
    public void TryGetCityByName_ShouldReturnFalse_WhenCityDoesNotExist()
    {
        // Arrange
        var cities = new List<CityDto>();
        var mockCitySet = CreateMockDbSet(cities);
        _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

        // Act
        var result = _repository.TryGetCityByName("NonExistentCity", out var retrievedCity);

        // Assert
        Assert.False(result);
        Assert.Null(retrievedCity);
    }

    [Fact]
    public void RemoveCity_ShouldReturnTrue_WhenCityIsRemoved()
    {
        // Arrange
        var city = new CityDto { CityName = "City1" };
        var cities = new List<CityDto> { city };

        // Create mock DbSet
        var mockCitySet = CreateMockDbSet(cities);
        _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

        // Ensure SaveChanges is called and returns 1 (indicating success)
        _mockDbContext.Setup(m => m.SaveChanges()).Returns(1);

        // Act
        var result = _repository.RemoveCity("City1");

        // Assert
        mockCitySet.Verify(m => m.Remove(It.IsAny<CityDto>()), Times.Once);
        _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public void RemoveCity_ShouldReturnFalse_WhenCityDoesNotExist()
    {
        // Arrange
        var cities = new List<CityDto>();
        var mockCitySet = CreateMockDbSet(cities);
        _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

        // Act
        var result = _repository.RemoveCity("NonExistentCity");

        // Assert
        Assert.False(result);
        _mockCitySet.Verify(m => m.Remove(It.IsAny<CityDto>()), Times.Never);
        _mockDbContext.Verify(m => m.SaveChanges(), Times.Never);
    }

    [Fact]
    public void UpdateCity_ShouldReturnTrue_WhenCityIsUpdated()
    {
        // Arrange
        var city = new CityDto { CityName = "City1" };
        _mockCitySet.Setup(m => m.Update(It.IsAny<CityDto>()));
        _mockDbContext.Setup(m => m.SaveChanges()).Returns(1);

        // Act
        var result = _repository.UpdateCity(city);

        // Assert
        _mockCitySet.Verify(m => m.Update(It.IsAny<CityDto>()), Times.Once);
        _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public void UpdateCity_ShouldReturnFalse_WhenSaveChangesFails()
    {
        // Arrange
        var city = new CityDto { CityName = "City1" };
        _mockCitySet.Setup(m => m.Update(It.IsAny<CityDto>()));
        _mockDbContext.Setup(m => m.SaveChanges()).Returns(0);

        // Act
        var result = _repository.UpdateCity(city);

        // Assert
        _mockCitySet.Verify(m => m.Update(It.IsAny<CityDto>()), Times.Once);
        _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        Assert.False(result);
    }

}
