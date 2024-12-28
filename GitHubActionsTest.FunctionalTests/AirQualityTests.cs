using GitHubActionsTest.API.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubActionsTest.FunctionalTests
{
    public class AirQualityTests : WebApplicationFactory<AirQualityController>
    {
        private readonly HttpClient client;

        public AirQualityTests()
        {
            client = CreateClient();
        }

        [Fact]
        public async Task GetAirQualityAsync_ReturnsAirQuality()
        {
            var response = await client.GetAsync("/api/airquality");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("us_aqi", responseString);
        }
    }
}
