using Nancy.Json;
using Shop.Core.Dto.WeatherDtos.AccuWeatherDtos;
using Shop.Core.ServiceInterface;
using System.Net;

namespace Shop.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "I3z5ZLeMB5VS3kNRMtT6OVTj48ODgAD6";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={accuApiKey}&q={dto.CityName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                //127964

                List<AccuLocationRootDto> accuResult = new JavaScriptSerializer().Deserialize<List<AccuLocationRootDto>>(json);

                dto.CityName = accuResult[0].LocalizedName;
                dto.CityCode = accuResult[0].Key;
            }   

            string urlWeather = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/{dto.CityCode}?apikey={accuApiKey}&metric=true";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(urlWeather);
                AccuWeatherRootDto accuForecastResult = new JavaScriptSerializer().Deserialize<AccuWeatherRootDto>(json);

                // Headline properties
                dto.EffectiveDate = accuForecastResult.Headline.EffectiveDate.ToString("yyyy-MM-dd");
                dto.EffectiveEpochDate = accuForecastResult.Headline.EffectiveEpochDate;
                dto.Severity = accuForecastResult.Headline.Severity;
                dto.Text = accuForecastResult.Headline.Text;
                dto.Category = accuForecastResult.Headline.Category;
                dto.EndDate = accuForecastResult.Headline.EndDate.ToString("yyyy-MM-dd");
                dto.EndEpochDate = accuForecastResult.Headline.EndEpochDate;

                // DailyForecast properties
                var forecast = accuForecastResult.DailyForecasts[0];
                dto.DailyForecastsDate = forecast.Date.ToString("yyyy-MM-dd");
                dto.DailyForecastsEpochDate = forecast.EpochDate;

                // Temperature properties
                dto.TempMinValue = forecast.Temperature.Minimum.Value;
                dto.TempMinUnit = forecast.Temperature.Minimum.Unit;
                dto.TempMinUnitType = forecast.Temperature.Minimum.UnitType;
                dto.TempMaxValue = forecast.Temperature.Maximum.Value;
                dto.TempMaxUnit = forecast.Temperature.Maximum.Unit;
                dto.TempMaxUnitType = forecast.Temperature.Maximum.UnitType;

                // Day properties
                dto.DayIcon = forecast.Day.Icon;
                dto.DayIconPhrase = forecast.Day.IconPhrase;
                dto.DayHasPrecipitation = forecast.Day.HasPrecipitation;
                dto.DayPrecipitationType = forecast.Day.PrecipitationType;
                dto.DayPrecipitationIntensity = forecast.Day.PrecipitationIntensity;

                // Night properties
                dto.NightIcon = forecast.Night.Icon;
                dto.NightIconPhrase = forecast.Night.IconPhrase;
                dto.NightHasPrecipitation = forecast.Night.HasPrecipitation;
                dto.NightPrecipitationType = forecast.Night.PrecipitationType;
                dto.NightPrecipitationIntensity = forecast.Night.PrecipitationIntensity;

                // Links
                dto.MobileLink = forecast.MobileLink;
                dto.Link = forecast.Link;
            }

                return dto;
        }
    }
}
