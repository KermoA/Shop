using Nancy.Json;
using Shop.Core.Dto.WeatherDtos.OpenWeatherMapDtos;
using Shop.Core.ServiceInterface;
using System.Net;

namespace Shop.ApplicationServices.Services
{
    public class OpenWeatherMapServices : IOpenWeatherMapServices
    {
        public async Task<OpenWeatherMapRootDto> OpenWeatherMapResult(OpenWeatherMapRootDto dto)
        {
            string owmApiKey = "3c1c938c3acbb76dba4fd4ed28306c77";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.Name}&appid={owmApiKey}&units=metric";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                OpenWeatherMapRootDto openWeatherMapResult = new JavaScriptSerializer().Deserialize<OpenWeatherMapRootDto>(json);

                // Map all properties
                dto.Id = openWeatherMapResult.Id;
                dto.Name = openWeatherMapResult.Name;
                dto.Timezone = openWeatherMapResult.Timezone;
                dto.Dt = openWeatherMapResult.Dt;
                dto.Cod = openWeatherMapResult.Cod;
                dto.Clouds = openWeatherMapResult.Clouds;
                dto.Coord = openWeatherMapResult.Coord;
                dto.Main = openWeatherMapResult.Main;
                dto.Visibility = openWeatherMapResult.Visibility;
                dto.Wind = openWeatherMapResult.Wind;
                dto.Weather = openWeatherMapResult.Weather;
                dto.Sys = openWeatherMapResult.Sys;
            }

            return dto;
        }
    }
}
