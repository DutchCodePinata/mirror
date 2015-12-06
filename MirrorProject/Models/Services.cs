using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TNX.RssReader;

namespace MirrorProject.Models
{
    public static class Services
    {
        private static List<WeatherIcon> weatherIcons = new List<WeatherIcon>();
        public static WeatherReport WundergroundQuery()
        {
            using (var client = new HttpClient(new HttpClientHandler(), true))
            {
                string apikey = "1859ccf0821879ad";
                string backupapikey = "5defeac1eb748efd";
                string pws = "IZUIDHOL158";
                string query = string.Format("http://api.wunderground.com/api/{0}/conditions/forecast/hourly/q/pws:{1}.json", backupapikey, pws);

                var json = client.GetStringAsync(query).Result;

                var data = JsonConvert.DeserializeObject<WundergroundData>(json);

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


                var weatherReport = new WeatherReport()
                {
                    Temp = data.current_observation.temp_c,
                    WindDir = data.current_observation.wind_dir,
                    WindSpeed = data.current_observation.wind_kph,
                    Icon = weatherIcons.Where(x => x.Wunderground.Equals(data.current_observation.icon)).Select(y => y.Css).FirstOrDefault(),
                    Daily = new List<WeatherDayForecast>(),
                    Hourly = new List<WeatherHourForecast>()
                };

                for (int i = 0; i < 3; i++)
                {
                    weatherReport.Daily.Add(new WeatherDayForecast()
                    {
                        RainChance = data.forecast.simpleforecast.forecastday[i].pop,
                        Date = data.forecast.simpleforecast.forecastday[i].date,
                        TempHigh = data.forecast.simpleforecast.forecastday[i].high.celsius,
                        TempLow = data.forecast.simpleforecast.forecastday[i].low.celsius
                    });
                }

                foreach (HourlyForecast hf in data.hourly_forecast)
                {
                    weatherReport.Hourly.Add(new WeatherHourForecast()
                    {
                        Hour = int.Parse(hf.FCTTIME.hour),
                        RainFall = double.Parse(hf.qpf.metric),
                        RainChance = int.Parse(hf.pop)
                    });
                }



                return weatherReport;
            }
        }

        public static RssResult RssQuery()
        {
            // Make configuration property
            string[] feeds = { "http://www.rtlnieuws.nl/service/rss/nieuws/index.xml", "http://tweakers.net/feeds/nieuws.xml" };
            int numberOfItems = 5;

            //Retrieve every item in every feed
            List<RssItem> items = new List<RssItem>();
            foreach (string feed in feeds)
            {
                RssFeed rssFeed = RssHelper.ReadFeed(feed);
                items.AddRange(rssFeed.Items);
            }

            //Sort all on publication datetime
            items = items.OrderByDescending(x => x.PublicationUtcTime).Take(numberOfItems).ToList();

            RssResult result = new RssResult();
            result.SetRssItems(items);
            return result;
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