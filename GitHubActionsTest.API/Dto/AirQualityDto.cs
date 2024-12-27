using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GitHubActionsTest.API.Dto
{
    /// <summary>
    /// Data transfer object for air quality.
    /// e.g. https://air-quality-api.open-meteo.com/v1/air-quality?latitude=44.80513&longitude=20.45819&current=us_aqi&timeformat=unixtime&start_date=2024-12-27&end_date=2024-12-27&domains=cams_europe
    /// </summary>
    public class AirQualityDto
    {
        public AirQualityDto()
        {
            Latitude = 44.80513;
            Longitude = 20.45819;
            Current = "us_aqi";
            TimeFormat = "unixtime";
            Domains = "cams_europe";
        }

        [FromQuery(Name = "latitude")]
        public double Latitude { get; init; }

        [FromQuery(Name = "longitude")]
        public double Longitude { get; init; }

        [FromQuery(Name = "current")]
        public string Current { get; init; }

        [FromQuery(Name = "timeformat")]
        public string TimeFormat { get; init; }

        [FromQuery(Name = "domains")]
        public string Domains { get; init; }

        public string ToQueryString()
        {
            return $"latitude={Latitude.ToString(CultureInfo.InvariantCulture)}&longitude={Longitude.ToString(CultureInfo.InvariantCulture)}&current={Current}&timeformat={TimeFormat}&domains={Domains}";
        }
    }
}
