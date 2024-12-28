using GitHubActionsTest.API.Dto;
using GitHubActionsTest.API.Http;
using Moq;
using Moq.Protected;
using System.Net;

namespace GitHubActionsTest.UnitTests
{
    public class AirQualityHttpClientTests
    {
        private readonly Mock<HttpMessageHandler> handlerMock;
        private readonly AirQualityHttpClient classUnderTest;

        public AirQualityHttpClientTests()
        {
            handlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri("https://air-quality-api.open-meteo.com");
            classUnderTest = new AirQualityHttpClient(httpClient);
        }

        [Fact]
        public async Task GetAirQualityAsync_ReturnsResponseContent()
        {
            // Arrange
            var airQualityDto = new AirQualityDto
            {
                Latitude = 44.80513,
                Longitude = 20.45819,
                Current = "us_aqi",
                TimeFormat = "unixtime",
                Domains = "cams_europe"
            };

            var queryString = airQualityDto.ToQueryString();
            var expectedResponse = "response content";

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri!.ToString().Contains($"/v1/air-quality?{queryString}")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponse),
                });

            // Act
            var result = await classUnderTest.GetAirQualityAsync(airQualityDto);

            // Assert
            Assert.Equal(expectedResponse, result);
        }
    }
}
