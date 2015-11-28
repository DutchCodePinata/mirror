using Newtonsoft.Json;
using QDFeedParser;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

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

        public static void RssQuery()
        {
            using (var client = new HttpClient(new HttpClientHandler(), true))
            {
                // Make configuration property
                string feedString = "http://www.nos.nl/export/nosnieuws-rss.xml";

                Uri feeduri = new Uri(feedString);
                IFeedFactory factory = new HttpFeedFactory();
                IFeed feed = factory.CreateFeed(feeduri);

                RssSingleton.GetInstance().SetRssItems(feed.Items);
            }
        }
    }
}