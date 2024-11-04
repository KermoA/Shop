namespace Shop.Core.Dto.WeatherDtos.AccuWeatherDtos
{
    public class AccuTimeZoneDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double GmtOffset { get; set; }
        public bool IsDaylightSavingTime { get; set; }
        public DateTime? NextOffsetChange { get; set; }
    }
}
