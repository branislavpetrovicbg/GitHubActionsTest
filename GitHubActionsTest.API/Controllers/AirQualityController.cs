using GitHubActionsTest.API.Dto;
using GitHubActionsTest.API.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitHubActionsTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirQualityController : ControllerBase
    {
        private readonly AirQualityHttpClient airQualityHttpClient;
        private readonly ILogger<AirQualityController> logger;

        public AirQualityController(
            AirQualityHttpClient airQualityHttpClient,
            ILogger<AirQualityController> logger)
        {
            this.airQualityHttpClient = airQualityHttpClient;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var airQualityDto = new AirQualityDto();
            var airQuality = await airQualityHttpClient.GetAirQualityAsync(airQualityDto);

            logger.LogInformation(new EventId(2507986), "Air quality: {airQuality}", airQuality);

            return Ok(airQuality);
        }
    }
}
