using Shop.Core.Dto.WeatherDtos.OpenWeatherMapDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IOpenWeatherMapServices
    {
        Task<OpenWeatherMapRootDto> OpenWeatherMapResult(OpenWeatherMapRootDto dto);
    }
}
