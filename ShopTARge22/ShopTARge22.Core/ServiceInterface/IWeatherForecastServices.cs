using ShopTARge22.Core.Dto.WeatherDtos;
using ShopTARge22.Core.Dto.WeatherDtos.AccuWeatherDtos;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto);
        Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto);
    }
}
