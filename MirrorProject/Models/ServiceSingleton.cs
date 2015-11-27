using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace MirrorProject.Models
{
    public class ServiceSingleton
    {
        private static ServiceSingleton instance = new ServiceSingleton();

        private WundergroundData wunderground = null;

        private WeatherReport weatherReport = new WeatherReport();

        private ServiceScheduler scheduler = new ServiceScheduler();

        private List<WeatherIcon> weatherIcons = new List<WeatherIcon>();

//        iconTable: {
//		'01d':'wi-day-sunny',
//		'02d':'wi-day-cloudy',
//		'03d':'wi-cloudy',
//		'04d':'wi-cloudy-windy',
//		'09d':'wi-showers',
//		'10d':'wi-rain',
//		'11d':'wi-thunderstorm',
//		'13d':'wi-snow',
//		'50d':'wi-fog',
//		'01n':'wi-night-clear',
//		'02n':'wi-night-cloudy',
//		'03n':'wi-night-cloudy',
//		'04n':'wi-night-cloudy',
//		'09n':'wi-night-showers',
//		'10n':'wi-night-rain',
//		'11n':'wi-night-thunderstorm',
//		'13n':'wi-night-snow',
//		'50n':'wi-night-alt-cloudy-windy'
//	}
//wunderground namen voor de mapper zijn hier te vinden: <div class="icon-set">
//	<img src="http://icons.wxug.com/i/c/k/chanceflurries.gif" alt="http://icons.wxug.com/i/c/k/chanceflurries.gif">
//	<img src="http://icons.wxug.com/i/c/k/chancerain.gif" alt="http://icons.wxug.com/i/c/k/chancerain.gif">
//	<img src="http://icons.wxug.com/i/c/k/chancesleet.gif" alt="http://icons.wxug.com/i/c/k/chancesleet.gif">
//	<img src="http://icons.wxug.com/i/c/k/chancesnow.gif" alt="http://icons.wxug.com/i/c/k/chancesnow.gif">
//	<img src="http://icons.wxug.com/i/c/k/chancetstorms.gif" alt="http://icons.wxug.com/i/c/k/chancetstorms.gif">
//	<img src="http://icons.wxug.com/i/c/k/clear.gif" alt="http://icons.wxug.com/i/c/k/clear.gif">
//	<img src="http://icons.wxug.com/i/c/k/cloudy.gif" alt="http://icons.wxug.com/i/c/k/clear.gif">
//	<img src="http://icons.wxug.com/i/c/k/flurries.gif" alt="http://icons.wxug.com/i/c/k/flurries.gif">
//	<img src="http://icons.wxug.com/i/c/k/fog.gif" alt="http://icons.wxug.com/i/c/k/fog.gif">
//	<img src="http://icons.wxug.com/i/c/k/hazy.gif" alt="http://icons.wxug.com/i/c/k/hazy.gif">
//	<img src="http://icons.wxug.com/i/c/k/mostlycloudy.gif" alt="http://icons.wxug.com/i/c/k/mostlycloudy.gif">
//	<img src="http://icons.wxug.com/i/c/k/mostlysunny.gif" alt="http://icons.wxug.com/i/c/k/mostlysunny.gif">
//	<img src="http://icons.wxug.com/i/c/k/partlycloudy.gif" alt="http://icons.wxug.com/i/c/k/partlycloudy.gif">
//	<img src="http://icons.wxug.com/i/c/k/partlysunny.gif" alt="http://icons.wxug.com/i/c/k/partlysunny.gif">
//	<img src="http://icons.wxug.com/i/c/k/sleet.gif" alt="http://icons.wxug.com/i/c/k/sleet.gif">
//	<img src="http://icons.wxug.com/i/c/k/rain.gif" alt="http://icons.wxug.com/i/c/k/rain.gif">
//	<img src="http://icons.wxug.com/i/c/k/sleet.gif" alt="http://icons.wxug.com/i/c/k/sleet.gif">
//	<img src="http://icons.wxug.com/i/c/k/snow.gif" alt="http://icons.wxug.com/i/c/k/snow.gif">
//	<img src="http://icons.wxug.com/i/c/k/sunny.gif" alt="http://icons.wxug.com/i/c/k/sunny.gif">
//	<img src="http://icons.wxug.com/i/c/k/tstorms.gif" alt="http://icons.wxug.com/i/c/k/tstorms.gif">
//	<img src="http://icons.wxug.com/i/c/k/cloudy.gif" alt="http://icons.wxug.com/i/c/k/cloudy.gif">
//	<img src="http://icons.wxug.com/i/c/k/partlycloudy.gif" alt="http://icons.wxug.com/i/c/k/partlycloudy.gif">
//</div>


        private ServiceSingleton() { CreateWeatherIconList(); }

        public static ServiceSingleton GetInstance()
        {
            return instance;
        }

        public void SetWunderground(WundergroundData data)
        {
            this.wunderground = data;

            this.weatherReport = new WeatherReport()
            {
                Temp = data.current_observation.temp_c,
                WindDir = data.current_observation.wind_dir,
                WindSpeed = data.current_observation.wind_kph,
                ForecastIcon = weatherIcons.Where(x=>x.wunderground.Equals(data.current_observation.icon)).Select(y=>y.css).FirstOrDefault(),
                ForecastTxtToday = data.forecast.simpleforecast.forecastday[0].conditions,
                ForecastChanceOfRain = data.forecast.simpleforecast.forecastday[0].pop,
                ForecastTempHigh = data.forecast.simpleforecast.forecastday[0].high.celsius,
                ForecastTempLow = data.forecast.simpleforecast.forecastday[0].low.celsius
            };
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
            weatherIcons.Add(new WeatherIcon { css = "wi-day-sunny", wunderground = "sunny" });
            weatherIcons.Add(new WeatherIcon { css = "wi-day-sunny", wunderground = "clear" });
            weatherIcons.Add(new WeatherIcon { css = "wi-day-sunny", wunderground = "mostlysunny" });
            weatherIcons.Add(new WeatherIcon { css = "wi-day-sunny", wunderground = "partlysunny" });

            weatherIcons.Add(new WeatherIcon { css = "wi-cloudy", wunderground = "cloudy" });
            weatherIcons.Add(new WeatherIcon { css = "wi-cloudy", wunderground = "mostlycloudy" });
            weatherIcons.Add(new WeatherIcon { css = "wi-cloudy", wunderground = "partlycloudy" });

            weatherIcons.Add(new WeatherIcon { css = "wi-rain", wunderground = "rain" });
            weatherIcons.Add(new WeatherIcon { css = "wi-rain", wunderground = "chancerain" });

            weatherIcons.Add(new WeatherIcon { css = "wi-sleet", wunderground = "chancesleet" });
            weatherIcons.Add(new WeatherIcon { css = "wi-sleet", wunderground = "sleet" });

            weatherIcons.Add(new WeatherIcon { css = "wi-thunderstorm", wunderground = "chancetstorms" });
            weatherIcons.Add(new WeatherIcon { css = "wi-thunderstorm", wunderground = "tstorms" });

            weatherIcons.Add(new WeatherIcon { css = "wi-snow", wunderground = "chanceflurries" });
            weatherIcons.Add(new WeatherIcon { css = "wi-snow", wunderground = "chancesnow" });
            weatherIcons.Add(new WeatherIcon { css = "wi-snow", wunderground = "flurries" });
            weatherIcons.Add(new WeatherIcon { css = "wi-snow", wunderground = "snow" });

            weatherIcons.Add(new WeatherIcon { css = "wi-fog", wunderground = "fog" });
            weatherIcons.Add(new WeatherIcon { css = "wi-fog", wunderground = "hazy" });
        }

        public struct WeatherIcon
        {
            public string wunderground;
            public string css;
        }

        public struct WeatherReport
        {
            public double Temp;
            public string WindDir;
            public double WindSpeed;
            public string ForecastIcon;
            public string ForecastTxtToday;
            public int ForecastChanceOfRain;
            public string ForecastTempHigh;
            public string ForecastTempLow;
        }
    }
}