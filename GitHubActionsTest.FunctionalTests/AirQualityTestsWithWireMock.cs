using GitHubActionsTest.API.Configuration;
using GitHubActionsTest.API.Controllers;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace GitHubActionsTest.FunctionalTests
{
    public class AirQualityTestsWithWireMock : IDisposable
    {
        private readonly CustomWebApplicationFactory<AirQualityController> factory;
        private readonly HttpClient client;
        private readonly WireMockServer mockServer;

        public AirQualityTestsWithWireMock()
        {
            mockServer = WireMockServer.Start();

            factory = new CustomWebApplicationFactory<AirQualityController>(services =>
            {
                // Configure your app to use mock server URL instead of real API
                services.Configure<AppSettings>(options =>
                {
                    options.ExternalApiBaseUrl = mockServer.Urls[0];
                });
            });
            client = factory.CreateClient();
        }

        public void Dispose()
        {
            mockServer.Stop();
            factory.Dispose();
            client.Dispose();
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
