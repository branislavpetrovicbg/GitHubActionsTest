using GitHubActionsTest.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GitHubActionsTest.UnitTests
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
        private readonly WeatherForecastController controllerUnderTest;

        public WeatherForecastControllerTests()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            controllerUnderTest = new WeatherForecastController(_loggerMock.Object);
        }

        [Fact]
        public void Get_Weathers()
        {
            var result = controllerUnderTest.Get();

            Assert.Equal(5, result.Count());
        }
    }
}
