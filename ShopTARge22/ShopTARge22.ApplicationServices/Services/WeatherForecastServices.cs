using Nancy.Json;
using ShopTARge22.Core.Dto.WeatherDtos;
using ShopTARge22.Core.Dto.WeatherDtos.AccuWeatherDtos;
using ShopTARge22.Core.ServiceInterface;
using System.Net;

namespace ShopTARge22.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {

        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string idOpenWeather = "yourApiKey";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City}&units=metric&appid={idOpenWeather}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                WeatherResponseRootDto weatherResult = new JavaScriptSerializer().Deserialize<WeatherResponseRootDto>(json);

                dto.City = weatherResult.Name;
                dto.Temp = weatherResult.Main.Temp;
                dto.FeelsLike = weatherResult.Main.Feels_like;
                dto.Humidity = weatherResult.Main.Humidity;
                dto.Pressure = weatherResult.Main.Pressure;
                dto.WindSpeed = weatherResult.Wind.Speed;
                dto.Description = weatherResult.Weather[0].Description;
            }

            return dto;
        }


        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={accuApiKey}&q={dto.CityName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                List<AccuLocationRootDto> accuResult = new JavaScriptSerializer().Deserialize<List<AccuLocationRootDto>>(json);

                dto.CityName = accuResult[0].Citys[0].LocalizedName;
                dto.CityCode = accuResult[0].Citys[0].Key;
                dto.Rank = accuResult[0].Citys[0].Rank;
            }

            string urlWeather = $"https://dataservice.accuweather.com/forecasts/v1/daily/1day/{dto.CityCode}?apikey={accuApiKey}&metric=true";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(urlWeather);
                AccuWeatherRootDto weatherRootDto = new JavaScriptSerializer().Deserialize<AccuWeatherRootDto>(json);
                
                dto.EffectiveDate = weatherRootDto.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDto.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDto.Headline.Severity;
                dto.Text = weatherRootDto.Headline.Text;
                dto.Category = weatherRootDto.Headline.Category;
                dto.EndDate = weatherRootDto.Headline.EndDate;
                dto.EndEpochDate = weatherRootDto.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDto.Headline.MobileLink;
                dto.Link = weatherRootDto.Headline.Link;

                dto.DailyForecastsDate = weatherRootDto.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDto.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDto.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDto.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherRootDto.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherRootDto.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherRootDto.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherRootDto.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherRootDto.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDto.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherRootDto.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayPrecipitationType = weatherRootDto.DailyForecasts[0].Day.PrecipitationType;
                dto.DayPrecipitationIntensity = weatherRootDto.DailyForecasts[0].Day.PrecipitationIntensity;

                dto.NightIcon = weatherRootDto.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDto.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherRootDto.DailyForecasts[0].Night.HasPrecipitation;
                dto.NightPrecipitationType = weatherRootDto.DailyForecasts[0].Night.PrecipitationType;
                dto.NightPrecipitationIntensity = weatherRootDto.DailyForecasts[0].Night.PrecipitationIntensity;
            }

                return dto;
        }
    }
}
