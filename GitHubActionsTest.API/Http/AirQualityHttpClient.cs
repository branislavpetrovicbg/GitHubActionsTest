using GitHubActionsTest.API.Dto;

namespace GitHubActionsTest.API.Http
{
    public class AirQualityHttpClient
    {
        private readonly HttpClient httpClient;

        public AirQualityHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetAirQualityAsync(AirQualityDto airQualityDto)
        {
            var queryString = airQualityDto.ToQueryString();
            var response = await httpClient.GetAsync($"/v1/air-quality?{queryString}");
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
