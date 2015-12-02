using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace MirrorProject.Models
{
    public class ServiceSingleton
    {
        private static ServiceSingleton instance = new ServiceSingleton();

        private WundergroundData wunderground = null;

        private WeatherReport weatherReport = new WeatherReport();

        private ServiceScheduler scheduler = new ServiceScheduler();

        private List<WeatherIcon> weatherIcons = new List<WeatherIcon>();

        private ServiceSingleton() { CreateWeatherIconList(); }

        public static ServiceSingleton GetInstance()
        {
            return instance;
        }

        public void SetWunderground(WundergroundData data)
        {
            if (data.forecast.simpleforecast.forecastday[0].high.celsius.IsNullOrWhiteSpace())
            {
                return;
            }

            this.wunderground = data;

            this.weatherReport = new WeatherReport()
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

        public WundergroundData GetWunderground()
        {
            return this.wunderground;
        }

        public WeatherReport GetWeatherReport()
        {
            return this.weatherReport;
        }

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

        public struct WeatherIcon
        {
            public string Wunderground;
            public string Css;
        }

        public struct WeatherReport
        {
            public double Temp;
            public string WindDir;
            public double WindSpeed;
            public string Icon;
            public List<WeatherDayForecast> Daily;
            public List<WeatherHourForecast> Hourly;
        }

        public struct WeatherDayForecast
        {
            public Date Date;
            public string TempHigh;
            public string TempLow;
            public int RainChance;
        }

        public struct WeatherHourForecast
        {
            public int Hour;
            public double RainFall;
            public int RainChance;
        }
    }
}