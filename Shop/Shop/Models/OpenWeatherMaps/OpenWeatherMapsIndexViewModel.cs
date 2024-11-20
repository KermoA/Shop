namespace Shop.Models.OpenWeatherMaps
{
    public class OpenWeatherMapsIndexViewModel
    {
        public int All { get; set; }

        public double Lon { get; set; }
        public double Lat { get; set; }

        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }

        public string Base { get; set; }
        public int Visibility { get; set; }

        public int Dt { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string CityName { get; set; }
        public int Cod { get; set; }

        public int Type { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }

        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public double Speed { get; set; }
        public int Deg { get; set; }
    }
}
