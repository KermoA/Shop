using Shop.Core.Dto.WeatherDtos.AccuWeatherDtos;

namespace Shop.ApplicationServices.Services
{
    public class WeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "I3z5ZLeMB5VS3kNRMtT6OVTj48ODgAD6";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={accuApiKey}&q={dto.CityName}";
        }
    }
}
