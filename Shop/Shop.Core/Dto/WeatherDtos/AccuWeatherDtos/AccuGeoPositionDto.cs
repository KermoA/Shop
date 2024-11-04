namespace Shop.Core.Dto.WeatherDtos.AccuWeatherDtos
{
    public class AccuGeoPositionDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public AccuElevationDto Elevation { get; set; }
    }
}
