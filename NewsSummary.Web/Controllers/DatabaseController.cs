using Microsoft.AspNetCore.Mvc;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models;
using NewsSummary.Core.Models.Forecast;

namespace NewsSummary.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly IGetAllDatabaseEntriesUseCase _getAllDatabaseEntriesUseCase;
    private readonly IAddCityToDbUseCase _addCityToDbUseCase;
    private readonly IRemoveCityFromDbUseCase _removeCityFromDbUseCase;
    private readonly IUpdateCityInDbUseCase _updateCityInDbUseCase;

    public DatabaseController(IGetAllDatabaseEntriesUseCase getAllDatabaseEntriesUseCase, IAddCityToDbUseCase addCityToDbUseCase, IRemoveCityFromDbUseCase removeCityFromDbUseCase, IUpdateCityInDbUseCase updateCityInDbUseCase)
    {
        this._getAllDatabaseEntriesUseCase = getAllDatabaseEntriesUseCase;
        this._addCityToDbUseCase = addCityToDbUseCase;
        this._removeCityFromDbUseCase = removeCityFromDbUseCase;
        this._updateCityInDbUseCase = updateCityInDbUseCase;
    }


    [HttpGet("GetAllCities")]
    [ProducesResponseType(typeof(List<CityDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public IActionResult GetAllCities()
    {
        var entries = this._getAllDatabaseEntriesUseCase.Execute();

        return entries == null? this.NoContent(): this.Ok(entries);
    }

    [HttpPost("AddNewCity")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status418ImATeapot)]
    public IActionResult AddNewCity([FromBody] CityDto city)
    {
        try
        {
            this._addCityToDbUseCase.Execute(city);
        }
        catch (Exception e)
        {
            return this.StatusCode(418);
            
        }
        return this.Ok();
    }

    [HttpDelete("DeleteCity")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status418ImATeapot)]
    public IActionResult DeleteCity(string cityName)
    {
        try
        {
            this._removeCityFromDbUseCase.Execute(cityName);
        }
        catch (Exception e)
        {
            return this.StatusCode(418);

        }
        return this.Ok();
    }

    [HttpPut("UpdateCity")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status418ImATeapot)]
    public IActionResult UpdateCity([FromBody] CityDto city)
    {
        try
        {
            this._updateCityInDbUseCase.Execute(city);
        }
        catch (Exception e)
        {
            return this.StatusCode(418);

        }
        return this.Ok();
    }
}
