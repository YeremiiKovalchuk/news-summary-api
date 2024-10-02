using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NewsSummary.Core.Models;
using NewsSummary.Infrastructure.Data;
using NewsSummary.Infrastructure.Services;
using Xunit;

namespace NewsSummary.Test.Unit
{
    public class CityRepositoryTests
    {
        private readonly Mock<SummaryDBContext> _mockDbContext;
        private readonly Mock<DbSet<CityDto>> _mockCitySet;
        private readonly Mock<ILogger<CityRepository>> _mockLogger;
        private readonly CityRepository _cityRepository;

        public CityRepositoryTests()
        {
            _mockDbContext = new Mock<SummaryDBContext>();
            _mockCitySet = new Mock<DbSet<CityDto>>();
            _mockLogger = new Mock<ILogger<CityRepository>>();

            _mockDbContext.Setup(m => m.Cities).Returns(_mockCitySet.Object);
            _cityRepository = new CityRepository(_mockDbContext.Object, _mockLogger.Object);
        }

        private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> elements)
            where T : class
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
        public void AddCity_ShouldAddCityToDatabase()
        {
            // Arrange
            var cityDto = new CityDto { CityName = "TestCity" };
            _mockCitySet.Setup(m => m.Add(It.IsAny<CityDto>()));

            // Act
            var result = _cityRepository.AddCity(cityDto);

            // Assert
            _mockCitySet.Verify(m => m.Add(It.IsAny<CityDto>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
            Assert.True(result);
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
            var result = _cityRepository.GetAllCities();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("City1", result[0]?.CityName);
            Assert.Equal("City2", result[1]?.CityName);
        }

        [Fact]
        public void TryGetCityByName_ShouldReturnCity_WhenCityExists()
        {
            // Arrange
            var city = new CityDto { CityName = "City1" };
            var cities = new List<CityDto> { city };

            var mockCitySet = CreateMockDbSet(cities);
            _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

            // Act
            var result = _cityRepository.TryGetCityByName("City1", out var retrievedCity);

            // Assert
            Assert.True(result);
            Assert.NotNull(retrievedCity);
            Assert.Equal("City1", retrievedCity?.CityName);
        }

        [Fact]
        public void TryGetCityByName_ShouldReturnFalse_WhenCityDoesNotExist()
        {
            // Arrange
            var cities = new List<CityDto>();

            var mockCitySet = CreateMockDbSet(cities);
            _mockDbContext.Setup(m => m.Cities).Returns(mockCitySet.Object);

            // Act
            var result = _cityRepository.TryGetCityByName("NonExistentCity", out var retrievedCity);

            // Assert
            Assert.False(result);
            Assert.Null(retrievedCity);
        }

        [Fact]
        public void RemoveCity_ShouldRemoveCityFromDatabase()
        {
            // Arrange
            var cityName = "TestCity";
            var cityDto = new CityDto { CityName = cityName };
            _mockCitySet.Setup(m => m.Remove(It.IsAny<CityDto>()));

            // Act
            var result = _cityRepository.RemoveCity(cityName);

            // Assert
            _mockCitySet.Verify(m => m.Remove(It.IsAny<CityDto>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void UpdateCity_ShouldUpdateCityInDatabase()
        {
            // Arrange
            var cityDto = new CityDto { CityName = "TestCity" };
            _mockCitySet.Setup(m => m.Update(It.IsAny<CityDto>()));

            // Act
            var result = _cityRepository.UpdateCity(cityDto);

            // Assert
            _mockCitySet.Verify(m => m.Update(It.IsAny<CityDto>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
            Assert.True(result);
        }
    }
}
