using GitHubActionsTest.API.Configuration;
using GitHubActionsTest.API.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace GitHubActionsTest.FunctionalTests
{
    public class AirQualityTestsWithWireMock : WebApplicationFactory<AirQualityController>
    {
        private readonly HttpClient client;
        private readonly WireMockServer mockServer;

        public AirQualityTestsWithWireMock()
        {
            mockServer = WireMockServer.Start();
            client = CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Configure<AppSettings>(options =>
                {
                    options.ExternalApiBaseUrl = mockServer.Urls[0];
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            mockServer.Stop();
        }

        [Fact]
        public async Task GetAirQualityAsync_ReturnsAirQuality()
        {
            // Arrange
            mockServer
                .Given(Request.Create()
                    .WithPath("/v1/air-quality") // Adjust path to match your external API
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(@"{""us_aqi"": 50}"));

            // Act
            var response = await client.GetAsync("/api/airquality");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("us_aqi", responseString);
        }
    }
}
