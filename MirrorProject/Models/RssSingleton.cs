using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Microsoft.Ajax.Utilities;
using TNX.RssReader;

namespace MirrorProject.Models
{
    public class RssSingleton
    {
        private static RssSingleton instance = new RssSingleton();

        private ServiceScheduler scheduler = new ServiceScheduler();

        private List<RssFeedItem> rssItems = new List<RssFeedItem>();

        public static RssSingleton GetInstance()
        {
            return instance;
        }

        public List<RssFeedItem> GetRssItems()
        {
            return rssItems;
        }

        public void SetRssItems(IEnumerable<RssItem> items)
        {
            rssItems.Clear();
            items.ForEach(x => rssItems.Add(new RssFeedItem(x.Title, x.PublicationUtcTime.ToLocalTime().ToString("HH:mm"))));
        }

        public class RssFeedItem
        {
            public RssFeedItem(string title, string publicationTime)
            {
                this.title = title;
                this.publicationTime = publicationTime;
            }

            public string title { get; set; }
            public string publicationTime { get; set; }
        }
    }
}