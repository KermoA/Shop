namespace Shop.Models.OpenWeatherMaps
{
    public class OpenWeatherMapsIndexViewModel
    {
        public string Name { get; set; }
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int? SeaLevel { get; set; }
        public int? GrndLevel { get; set; }
        public int Visibility { get; set; }
        public double WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public int Cloudiness { get; set; }
        public int? Sunrise { get; set; }
        public int? Sunset { get; set; }
        public List<WeatherCondition> WeatherConditions { get; set; }
        public string Base { get; set; }
        public long Dt { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public int Cod { get; set; }
    }

        public class WeatherCondition
        {
            public int Id { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }
}

