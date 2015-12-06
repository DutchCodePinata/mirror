using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using TNX.RssReader;

namespace MirrorProject.Models
{
    public static class Services
    {
        public static async Task WundergroundQuery()
        {
            try
            {
                using (var client = new HttpClient(new HttpClientHandler(), true))
                {
                    string apikey = "1859ccf0821879ad";
                    string backupapikey = "5defeac1eb748efd";
                    string pws = "IZUIDHOL158";
                    string query = string.Format("http://api.wunderground.com/api/{0}/conditions/forecast/hourly/q/pws:{1}.json", apikey, pws);

                    var json = await client.GetStringAsync(query);

                    ServiceSingleton.GetInstance().SetWunderground(JsonConvert.DeserializeObject<WundergroundData>(json));
                }
            }
            catch (Exception ex)
            {
                
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
    }
}