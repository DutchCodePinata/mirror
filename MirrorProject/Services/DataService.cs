using MirrorProject.Models.ViewModels;
using MirrorProject.Models.Wunderground;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TNX.RssReader;

namespace MirrorProject.Models
{
    public static class DataService
    {
        public static WeatherReport getWunderground()
        {
            using (var client = new HttpClient(new HttpClientHandler(), true))
            {
                string apikey = "1859ccf0821879ad";
                string backupapikey = "5defeac1eb748efd";
                string pws = "IZUIDHOL158";
                string query = string.Format("http://api.wunderground.com/api/{0}/conditions/forecast/hourly/q/pws:{1}.json", backupapikey, pws);

                var json = client.GetStringAsync(query).Result;

                var data = JsonConvert.DeserializeObject<WundergroundData>(json);

                var weatherReport = new WeatherReport()
                {
                    Temp = data.current_observation.temp_c,
                    WindDir = data.current_observation.wind_dir,
                    WindSpeed = data.current_observation.wind_kph,
                    Icon = WeatherService.getInstance().getWeatherIcons().Where(x => x.Wunderground.Equals(data.current_observation.icon)).Select(y => y.Css).FirstOrDefault(),
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

        public static RssResult getRss()
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
            result.setRssItems(items);
            return result;
        }
    }
}