using Microsoft.AspNetCore.Mvc;
using NewsSummary.Core.Interfaces;

namespace NewsSummary.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IForecastService _forecastService;
        private readonly ILogger _logger;

        public WeatherForecastController(IForecastService forecastService, ILogger<WeatherForecastController> logger)
        {
            this._forecastService = forecastService;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var output = await this._forecastService.GetForecast();

            return this.Ok(output.Value);
        }
        
    }
}
