using Shop.Core.Dto.WeatherDtos.AccuWeatherDtos;

namespace Shop.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto);
    }
}
