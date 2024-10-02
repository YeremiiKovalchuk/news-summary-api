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

    public DatabaseController(IGetAllDatabaseEntriesUseCase getAllDatabaseEntriesUseCase, IAddCityToDbUseCase addCityToDbUseCase)
    {
        _getAllDatabaseEntriesUseCase = getAllDatabaseEntriesUseCase;
        _addCityToDbUseCase = addCityToDbUseCase;
    }


    [HttpGet("GetAllCities")]
    [ProducesResponseType(typeof(List<CityDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status418ImATeapot)]
    public IActionResult GetAllCities()
    {
        return this.Ok(this._getAllDatabaseEntriesUseCase.Execute());
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
}
