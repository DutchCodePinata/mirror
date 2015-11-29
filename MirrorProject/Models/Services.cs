using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TNX.RssReader;

namespace MirrorProject.Models
{
    public static class Services
    {
        public static async Task WundergroundQuery()
        {
            using (var client = new HttpClient(new HttpClientHandler(), true))
            {
                string apikey = "1859ccf0821879ad";
                string pws = "IZUIDHOL158";
                string query = string.Format("http://api.wunderground.com/api/{0}/conditions/forecast/q/pws:{1}.json", apikey, pws);

                var json = await client.GetStringAsync(query);

                ServiceSingleton.GetInstance().SetWunderground(JsonConvert.DeserializeObject<WundergroundData>(json));
            }
        }

        public static async Task RssQuery()
        {
            // Make configuration property
            string feedString = "http://www.nos.nl/export/nosnieuws-rss.xml";

            RssFeed feed = await RssHelper.ReadFeedAsync(feedString);
            Uri feeduri = new Uri(feedString);
            RssSingleton.GetInstance().SetRssItems(feed.Items);
        }
    }
}