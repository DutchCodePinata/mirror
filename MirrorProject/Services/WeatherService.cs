using System.Linq;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using MirrorProject.Models.ViewModels;
using MirrorProject.Models.Wunderground;

namespace MirrorProject.Models
{
    public class WeatherService
    {
        private static WeatherService instance = new WeatherService();
        private WundergroundData wunderground = null;
        private WeatherReport weatherReport = new WeatherReport();
        private SchedulerService scheduler = new SchedulerService();
        private List<WeatherIcon> weatherIcons = new List<WeatherIcon>();

        private WeatherService() { CreateWeatherIconList(); }

        private void CreateWeatherIconList()
        {
            weatherIcons.Add(new WeatherIcon { Css = "wi-day-sunny", Wunderground = "sunny" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-day-sunny", Wunderground = "clear" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-day-sunny", Wunderground = "mostlysunny" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-day-sunny", Wunderground = "partlysunny" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-cloudy", Wunderground = "cloudy" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-cloudy", Wunderground = "mostlycloudy" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-cloudy", Wunderground = "partlycloudy" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-rain", Wunderground = "rain" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-rain", Wunderground = "chancerain" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-sleet", Wunderground = "chancesleet" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-sleet", Wunderground = "sleet" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-thunderstorm", Wunderground = "chancetstorms" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-thunderstorm", Wunderground = "tstorms" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-snow", Wunderground = "chanceflurries" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-snow", Wunderground = "chancesnow" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-snow", Wunderground = "flurries" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-snow", Wunderground = "snow" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-fog", Wunderground = "fog" });
            weatherIcons.Add(new WeatherIcon { Css = "wi-fog", Wunderground = "hazy" });
        }

        public static WeatherService getInstance()
        {
            return instance;
        }

        public void setWundergroundData(WundergroundData data)
        {
            if (data.forecast.simpleforecast.forecastday[0].high.celsius.IsNullOrWhiteSpace())
            {
                return;
            }

            wunderground = data;

            weatherReport = new WeatherReport()
            {
                Temp = data.current_observation.temp_c,
                WindDir = data.current_observation.wind_dir,
                WindSpeed = data.current_observation.wind_kph,
                Icon = weatherIcons.Where(x=>x.Wunderground.Equals(data.current_observation.icon)).Select(y=>y.Css).FirstOrDefault(),
                Daily = new List<WeatherDayForecast>(),
                Hourly = new List<WeatherHourForecast>()
            };

            for(int i = 0 ; i < 3; i++)
            {
                weatherReport.Daily.Add(new WeatherDayForecast() { 
                    RainChance = data.forecast.simpleforecast.forecastday[i].pop,
                    Date = data.forecast.simpleforecast.forecastday[i].date,
                    TempHigh = data.forecast.simpleforecast.forecastday[i].high.celsius,
                    TempLow = data.forecast.simpleforecast.forecastday[i].low.celsius
                });
            }

            foreach(HourlyForecast hf in data.hourly_forecast)
            {
                weatherReport.Hourly.Add(new WeatherHourForecast()
                {
                    Hour = int.Parse(hf.FCTTIME.hour),
                    RainFall = double.Parse(hf.qpf.metric),
                    RainChance = int.Parse(hf.pop)
                });
            }
        }

        public WundergroundData getWundergroundData()
        {
            return wunderground;
        }

        public WeatherReport getWeatherReport()
        {
            return weatherReport;
        }

        public List<WeatherIcon> getWeatherIcons()
        {
            return weatherIcons;
        }
    }
}