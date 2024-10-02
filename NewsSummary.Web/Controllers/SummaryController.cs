using Microsoft.AspNetCore.Mvc;
using NewsSummary.Core.Interfaces.UseCases;
using NewsSummary.Core.Interfaces.UseCases.Database;
using NewsSummary.Core.Models.Forecast;
using NewsSummary.Core.Models.Forecast.WeatherAPI;
using NewsSummary.Core.Models.News;
using NewsSummary.Core.Models.News.Mediastack;

namespace NewsSummary.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SummaryController : ControllerBase
{

    private readonly IGetForecastUseCase _getForecastUseCase;
    private readonly IGetNewsUseCase _getNewsUseCase;
    private readonly ILogger _logger;

    public SummaryController(IGetForecastUseCase getForecastUseCase, IGetNewsUseCase getNewsUseCase, 
        IGetAllDatabaseEntriesUseCase getAllDatabaseEntriesUseCase,
        IAddCityToDbUseCase addCityToDbUseCase,
        ILogger<SummaryController> logger)
    {
        this._getForecastUseCase = getForecastUseCase;
        this._getNewsUseCase = getNewsUseCase;
        this._logger = logger;
    }

    [HttpGet("GetForecast")]
    [ProducesResponseType(typeof(ForecastResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void),StatusCodes.Status418ImATeapot)]
    public async Task<IActionResult> GetForecast(string city = "Calp", string language = "en")
    {
        var settings = new ForecastRequestSettings()
        {
            City = city,
            Lang = language,
        };

        var output = await this._getForecastUseCase.Execute(settings);
        if (output.Success)
        {
            return this.Ok(output.Value);
        }
        return this.StatusCode(StatusCodes.Status418ImATeapot);   
    }

    [HttpGet("GetNews")]
    [ProducesResponseType(typeof(NewsResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status418ImATeapot)]
    public async Task<IActionResult> GetNews(string country = "ua")
    {
        var settings = new NewsRequestSettings()
        {
            Country = country
        };

        var output = await this._getNewsUseCase.Execute(settings);
        if (output.Success)
        {
            return this.Ok(output.Value);
        }
        return this.StatusCode(StatusCodes.Status418ImATeapot);
    }


}
