namespace Shop.Core.Dto.WeatherDtos.AccuWeatherDtos
{
    public class AccuLocationWeatherResultDto
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
        public string PrimaryPostalCode { get; set; }
        public AccuRegionDto Region { get; set; }
        public AccuCountryDto Country { get; set; }
        public AccuAdministrativeAreaDto AdministrativeArea { get; set; }
        public AccuTimeZoneDto TimeZone { get; set; }
        public AccuGeoPositionDto GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public List<AccuSupplementalAdminAreaDto> SupplementalAdminAreas { get; set; }
        public List<string> DataSets { get; set; }
    }
}
